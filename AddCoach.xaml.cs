﻿using Microsoft.Win32;
using Sports_Coaches.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UnidecodeSharpFork;
using System.Data.Entity;
using System.IO;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddCoach.xaml
    /// </summary>
    public partial class AddCoach : Window
    {
        private Context db;
        private OpenFileDialog openFileDialog;
        private List<Language> selectedLanguage = new List<Language>();
        private List<Diploma> selectedDimplomas = new List<Diploma>();
        private List<Phone> addedPhones = new List<Phone>();
        private List<Course> selectedCourses = new List<Course>();
        private List<Certificate> selectedCertificates = new List<Certificate>();
        private List<Schedule> sheduleResult = new List<Schedule>();
        private List<Training> selectedTraining = new List<Training>();
        private List<AwayTraining> selectedAwayTraining = new List<AwayTraining>();
        private List<Achievement> addedAchievement = new List<Achievement>();
        private List<WorkPlace> addedWorkplaces = new List<WorkPlace>();
        private Coach coachToEdit;
        private bool addSelectedLanguages = false;
        private bool isEditForm = false;
        public AddCoach(int? coachId = null)
        {
            InitializeComponent();
            db = new Context();

            AddSports();
            AddLanguages();
            AddCities();
            AddRanks();

            if (coachId != null)
            {
                coachToEdit = db.Coaches.Include("Sport").Include("City").Include("WorkPlaces").Include("Training").Include("Phone").Include("Languages").Include("City").Include("Diplomas").Include("Courses").Include("Certificates").Include("Certificates").Include("Rank").Include("Training").Include("AwayTraining").Include("Schedule").Include("Achievements").Where(c => c.Id.Equals((int)coachId)).First(); ;
                isEditForm = true;
                delBTN.Visibility = Visibility.Visible;
                EditCoach(coachToEdit);
            }
        }

        private void EditCoach(Coach coach)
        {
            nameTB.Text = coach.FullName;
            if (coach.Gender == 0) (genderSP.Children[0] as RadioButton).IsChecked = true;
            else (genderSP.Children[1] as RadioButton).IsChecked = true;
            birthdateDP.SelectedDate = coach.DateOfBirth;

            if (coach.PhotoUrl != null)
                coachImage.Source = new BitmapImage(new Uri(coach.PhotoUrl, UriKind.Absolute));

            if (coach.Email != null)
                emailTB.Text = coach.Email;

            if (coach.Phone != null && coach.Phone.Count > 0)
            {
                phoneTB.Text = coach.Phone.Last().Number;
                coach.Phone.Remove(coach.Phone.Last());
                addedPhones = coach.Phone.ToList();
                AddSelectedPhones();
            }

            selectedLanguage = coach.Languages.ToList();
            AddSelectedLanguages();

            foreach (Sport sport in sportCB.Items)
            {
                if (sport.Id.Equals(coach.Sport.Id))
                {
                    sportCB.SelectedItem = sport;
                }
            }

            if (coach.Rank != null)
            {
                foreach (Rank rank in rankCB.Items)
                {
                    if (rank.Id.Equals(coach.Rank.Id))
                    {
                        rankCB.SelectedItem = rank;
                    }
                }
            }

            foreach (City city in cityCB.Items)
            {
                if (city.Id.Equals(coach.City.Id))
                {
                    cityCB.SelectedItem = city;
                }
            }

            experienceTB.Text = coach.Experience.ToString();

            selectedDimplomas = coach.Diplomas.ToList();
            AddSelectedDiplomas();

            selectedCourses = coach.Courses.ToList();
            AddSelectedCourses();

            selectedCertificates = coach.Certificates.ToList();
            AddSelectedCertificates();

            addedAchievement = coach.Achievements.ToList();
            AddSelectedAchievements();

            addedWorkplaces = db.Coaches.Include(c => c.WorkPlaces.Select(w => w.City)).Where(c => c.Id.Equals(coach.Id)).First().WorkPlaces.ToList();
            AddSelectedWorkPlaces();

            selectedTraining = coach.Training.ToList();
            AddSelectedTraining();

            selectedAwayTraining = coach.AwayTraining.ToList();
            AddSelectedAwayTraining();

            sheduleResult = coach.Schedule.ToList();
            AddSelectedSchedule();

            addBTN.Content = "Сохранить";

        }

        private void AddLanguages()
        {
            languagesLB.ItemsSource = db.Languages.OrderBy(l => l.Name).ToList();
            languagesLB.DisplayMemberPath = "Name";
        }

        private void AddCities()
        {
            cityCB.ItemsSource = db.Cities.OrderBy(c => c.Name).ToList();
            cityCB.DisplayMemberPath = "Name";
        }

        private void AddSports()
        {
            sportCB.ItemsSource = db.Sports.OrderBy(s => s.Name).ToList();
            sportCB.DisplayMemberPath = "Name";
        }

        private void AddRanks()
        {
            rankCB.ItemsSource = db.Ranks.OrderBy(r => r.Name).ToList();
            rankCB.DisplayMemberPath = "Name";
        }

        private void AddSelectedDiplomas()
        {
            diplomasLB.ItemsSource = selectedDimplomas.ToList();
            diplomasLB.DisplayMemberPath = "Name";
            diplomasLB.SelectAll();
        }

        private void AddSelectedCourses()
        {
            coursesLB.ItemsSource = selectedCourses.ToList();
            coursesLB.DisplayMemberPath = "Name";
            coursesLB.SelectAll();
        }

        private void AddSelectedCertificates()
        {
            certificatesLB.ItemsSource = selectedCertificates.ToList();
            certificatesLB.DisplayMemberPath = "Name";
            certificatesLB.SelectAll();
        }

        private void AddSelectedSchedule()
        {
            scheduleLB.Items.Clear();
            foreach (var schedule in sheduleResult.OrderBy(s => s.Day).ThenBy(s => s.StartHour))
            {
                scheduleLB.Items.Add(schedule.Day.ToString() + ":" + schedule.StartHour + ":00");
            }
            scheduleLB.SelectAll();
        }

        private void AddSelectedTraining()
        {
            trainingLB.Items.Clear();
            foreach (Training training in selectedTraining)
            {
                trainingLB.Items.Add(training.Name + " - " + training.Time + " | " + training.Price + " у.е.");
            }
            trainingLB.SelectAll();
        }

        private void AddSelectedAwayTraining()
        {
            awayTrainingLB.Items.Clear();
            foreach (AwayTraining training in selectedAwayTraining)
            {
                awayTrainingLB.Items.Add(training.Name + " - " + training.Time + " | " + training.Price + " у.е.");
            }
            awayTrainingLB.SelectAll();
        }

        private void AddSelectedWorkPlaces()
        {
            workplaceLB.Items.Clear();
            foreach (WorkPlace workPlace in addedWorkplaces)
            {
                workplaceLB.Items.Add(workPlace.Name);
            }
            workplaceLB.SelectAll();
        }

        private void AddSelectedAchievements()
        {
            achievementsLB.Items.Clear();
            foreach (Achievement achievement in addedAchievement)
            {
                achievementsLB.Items.Add(achievement.Name + " - " + achievement.Place + " | " + achievement.Status);
            }
            achievementsLB.SelectAll();
        }

        private void AddSelectedPhones()
        {
            phonesLB.ItemsSource = addedPhones.ToList();
            phonesLB.DisplayMemberPath = "Number";
            phonesLB.SelectAll();
        }

        private void AddSelectedLanguages()
        {
            addSelectedLanguages = true;
            languagesLB.SelectedItems.Clear();
            foreach (Language language in selectedLanguage)
            {
                languagesLB.SelectedItems.Add(db.Languages.Where(l => l.Id.Equals(language.Id)).First());
            }
            addSelectedLanguages = false;
        }

        private void coachImage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    coachImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    string filename = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void languagesBTN_Click(object sender, RoutedEventArgs e)
        {
            AddLanguage addLanguageForm = new AddLanguage();
            addLanguageForm.ShowDialog();
            if (addLanguageForm.DialogResult == true)
            {
                AddLanguages();
                languagesLB.SelectedItems.Add(db.Languages.ToList().Last());
            }
        }

        private void languagesLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!addSelectedLanguages)
                selectedLanguage.Add(e.AddedItems[0] as Language);
        }

        private void sportBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSport addSportForm = new AddSport();
            addSportForm.ShowDialog();
            if (addSportForm.DialogResult == true)
            {
                AddSports();
                sportCB.SelectedItem = db.Sports.ToList().Last();
            }
        }

        private void cityBTN_Click(object sender, RoutedEventArgs e)
        {
            AddCity addCityForm = new AddCity();
            addCityForm.ShowDialog();
            if (addCityForm.DialogResult == true)
            {
                AddCities();
                cityCB.SelectedItem = db.Cities.ToList().Last();
            }
        }

        private void dimplomasBTN_Click(object sender, RoutedEventArgs e)
        {
            AddDiplomas addDiplomasForm = new AddDiplomas(selectedDimplomas);
            addDiplomasForm.ShowDialog();
            if (addDiplomasForm.DialogResult == true)
            {
                this.selectedDimplomas = addDiplomasForm.selectedDimplomas;
                AddSelectedDiplomas();
            }
        }

        private void phonesBTN_Click(object sender, RoutedEventArgs e)
        {
            AddPhones addPhonesForm = new AddPhones(addedPhones);
            addPhonesForm.ShowDialog();
            if (addPhonesForm.DialogResult == true)
            {
                this.addedPhones = addPhonesForm.addedPhones;
                AddSelectedPhones();
            }
        }
        private void coursesBTN_Click(object sender, RoutedEventArgs e)
        {
            AddCourses addCoursesForm = new AddCourses(selectedCourses);
            addCoursesForm.ShowDialog();
            if (addCoursesForm.DialogResult == true)
            {
                this.selectedCourses = addCoursesForm.selectedCourses;
                AddSelectedCourses();
            }
        }

        private void certificatesBTN_Click(object sender, RoutedEventArgs e)
        {
            AddCertificates addCertificatesForm = new AddCertificates(selectedCertificates);
            addCertificatesForm.ShowDialog();
            if (addCertificatesForm.DialogResult == true)
            {
                this.selectedCertificates = addCertificatesForm.selectedCertificates;
                AddSelectedCertificates();
            }
        }
        private void scheduleBTN_Click(object sender, RoutedEventArgs e)
        {
            ScheduleForm scheduleForm = new ScheduleForm(sheduleResult);
            scheduleForm.ShowDialog();
            if (scheduleForm.DialogResult == true)
            {
                this.sheduleResult = scheduleForm.sheduleResult;
                AddSelectedSchedule();
            }
        }

        private void trainingBTN_Click(object sender, RoutedEventArgs e)
        {
            AddTraining addTrainingForm = new AddTraining(selectedTraining);
            addTrainingForm.ShowDialog();
            if (addTrainingForm.DialogResult == true)
            {
                this.selectedTraining = addTrainingForm.selectedTraining;
                AddSelectedTraining();
            }
        }

        private void awayTrainingBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAwayTraining addAwayTrainingForm = new AddAwayTraining(selectedAwayTraining);
            addAwayTrainingForm.ShowDialog();
            if (addAwayTrainingForm.DialogResult == true)
            {
                this.selectedAwayTraining = addAwayTrainingForm.selectedAwayTraining;
                AddSelectedAwayTraining();
            }
        }

        private void achievementsBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAchievements addAchievementsForm = new AddAchievements(addedAchievement);
            addAchievementsForm.ShowDialog();
            if (addAchievementsForm.DialogResult == true)
            {
                this.addedAchievement = addAchievementsForm.addedAchievement;
                AddSelectedAchievements();
            }
        }

        private void workplaceBTN_Click(object sender, RoutedEventArgs e)
        {
            City selectedCity = null;
            foreach (City city in cityCB.Items)
            {
                if (city.Name.Equals(cityCB.Text))
                {
                    cityCB.SelectedItem = city;
                }
            }
            AddWorkPlace addWorkPlacesForm;
            if (selectedCity!=null)
                addWorkPlacesForm = new AddWorkPlace(addedWorkplaces, selectedCity);
            else addWorkPlacesForm = new AddWorkPlace(addedWorkplaces);
            addWorkPlacesForm.ShowDialog();
            if (addWorkPlacesForm.DialogResult == true)
            {
                this.addedWorkplaces = addWorkPlacesForm.addedWorkplaces;
                AddSelectedWorkPlaces();
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            string fullName = nameTB.Text;

            Gender gender;
            if ((bool)(genderSP.Children[0] as RadioButton).IsChecked) gender = Gender.Мужчина;
            else gender = Gender.Женщина;

            DateTime? birthday = birthdateDP.SelectedDate;

            string imageUrl;
            if (openFileDialog != null)
            {
                string filename = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                string coachImage = fullName.Split(' ')[1].Unidecode() + filename.Substring(filename.LastIndexOf('.'));
                File.Copy(openFileDialog.FileName, "..//..//Images//" + coachImage, true);
                imageUrl = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "Images\\" + coachImage);
            }
            else if (!isEditForm)
            {
                imageUrl = System.IO.Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "Images\\no-image.png");
            }
            else imageUrl = coachToEdit.PhotoUrl;

            string email = emailTB.Text;

            Phone phone = new Phone { Number = phoneTB.Text };
            addedPhones.Add(phone);

            Sport sport = sportCB.SelectedItem as Sport;

            Rank rank = rankCB.SelectedItem as Rank;

            int experience;
            if (int.TryParse(experienceTB.Text, out experience))
            {
                experience = int.Parse(experienceTB.Text);
            }
            else experience = 0;

            City city = cityCB.SelectedItem as City;


            if (fullName.Length > 0 && birthday != null && phone.Number.Length > 0 && selectedLanguage.Count > 0 && sport != null && experience != 0 && city.Name.Length > 0 && selectedTraining.Count > 0)
            {
                if (!isEditForm)
                {
                    Coach coach = new Coach() { FullName = fullName, Gender = gender, DateOfBirth = (DateTime)birthday, PhotoUrl = imageUrl, Email = email, Phone = addedPhones, Languages = selectedLanguage, Sport = sport, Rank = rank, Experience = experience, Diplomas = selectedDimplomas, Courses = selectedCourses, Certificates = selectedCertificates, Achievements = addedAchievement, City = city, WorkPlaces = addedWorkplaces, Training = selectedTraining, AwayTraining = selectedAwayTraining, Schedule = sheduleResult };
                    db.Coaches.Add(coach);
                }
                else
                {
                    Coach coach = coachToEdit;
                    coach.FullName = fullName;
                    coach.Gender = gender;
                    coach.DateOfBirth = (DateTime)birthday;
                    coach.PhotoUrl = imageUrl;
                    coach.Email = email;
                    coach.Phone = addedPhones;
                    coach.Courses = selectedCourses;
                    coach.Sport = sport;
                    coach.Rank = rank;
                    coach.Experience = experience;
                    coach.Diplomas = selectedDimplomas;
                    coach.Courses = selectedCourses;
                    coach.Certificates = selectedCertificates;
                    coach.Achievements = addedAchievement;
                    coach.City = city;
                    coach.WorkPlaces = addedWorkplaces;
                    coach.Training = selectedTraining;
                    coach.AwayTraining = selectedAwayTraining;
                    coach.Schedule = sheduleResult;
                }

                db.SaveChanges();
                this.DialogResult = true;
                Close();
            }
            else
                {
                Xceed.Wpf.Toolkit.MessageBox.Show("Заповніть обов'язкові поля", "Виникла помилка");
            }
        }

        private void delBTN_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show("Ви дійсно вневнені, що хочете видалити запис про " + coachToEdit.FullName + "?", "Видалення запису тренера", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Coach coach = db.Coaches.Include("Sport").Include("City").Include("WorkPlaces").Include("Training").Include("Phone").Include("Languages").Include("City").Include("Diplomas").Include("Courses").Include("Certificates").Include("Certificates").Include("Rank").Include("Training").Include("AwayTraining").Include("Schedule").Include("Achievements").Where(c => c.Id.Equals(coachToEdit.Id)).First();

                if (coach != null)
                {
                    db.Coaches.Remove(coach);
                    db.SaveChanges();
                    this.DialogResult = true;
                    Close();
                }
            }
        }
    }
}
