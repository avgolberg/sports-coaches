using Microsoft.Win32;
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

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddCoach.xaml
    /// </summary>
    public partial class AddCoach : Window
    {
        private Context db;
        private OpenFileDialog openFileDialog;
        private Coach coach;
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
        public AddCoach()
        {
            InitializeComponent();
            db = new Context();
            coach = new Coach();
            coach.Id = db.Coaches.Max(c => c.Id) + 1;

            AddSports();
            AddLanguages();
            AddCities();
            AddRanks();
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
                //System.IO.File.Copy(openFileDialog.FileName, "..//..//Images//coach_" + coach.Id + "_" + filename.Substring(filename.LastIndexOf('.')));

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
                diplomasLB.Items.Clear();
                foreach (Diploma diploma in addDiplomasForm.selectedDimplomas)
                {
                    diplomasLB.Items.Add(diploma.Name);
                }
                diplomasLB.SelectAll();
            }
        }

        private void phonesBTN_Click(object sender, RoutedEventArgs e)
        {
            AddPhones addPhonesForm = new AddPhones(addedPhones);
            addPhonesForm.ShowDialog();
            if (addPhonesForm.DialogResult == true)
            {
                this.addedPhones = addPhonesForm.addedPhones;
                phonesLB.Items.Clear();
                foreach (Phone phone in addPhonesForm.addedPhones)
                {
                    phonesLB.Items.Add(phone.Number);
                }
                phonesLB.SelectAll();
            }
        }
        private void coursesBTN_Click(object sender, RoutedEventArgs e)
        {
            AddCourses addCoursesForm = new AddCourses(selectedCourses);
            addCoursesForm.ShowDialog();
            if (addCoursesForm.DialogResult == true)
            {
                this.selectedCourses = addCoursesForm.selectedCourses;
                coursesLB.Items.Clear();
                foreach (Course course in addCoursesForm.selectedCourses)
                {
                    coursesLB.Items.Add(course.Name);
                }
                coursesLB.SelectAll();
            }
        }

        private void certificatesBTN_Click(object sender, RoutedEventArgs e)
        {
            AddCertificates addCertificatesForm = new AddCertificates(selectedCertificates);
            addCertificatesForm.ShowDialog();
            if (addCertificatesForm.DialogResult == true)
            {
                this.selectedCertificates = addCertificatesForm.selectedCertificates;
                certificatesLB.Items.Clear();
                foreach (Certificate certificate in addCertificatesForm.selectedCertificates)
                {
                    certificatesLB.Items.Add(certificate.Name);
                }
                certificatesLB.SelectAll();
            }
        }
        private void scheduleBTN_Click(object sender, RoutedEventArgs e)
        {
            ScheduleForm scheduleForm = new ScheduleForm(sheduleResult);
            scheduleForm.ShowDialog();
            if (scheduleForm.DialogResult == true)
            {
                this.sheduleResult = scheduleForm.sheduleResult;
                scheduleLB.Items.Clear();
                foreach (var schedule in sheduleResult.OrderBy(s => s.Day).ThenBy(s => s.StartHour))
                {
                    scheduleLB.Items.Add(schedule.Day.ToString() + ":" + schedule.StartHour + ":00");
                }
                scheduleLB.SelectAll();
            }
        }

        private void trainingBTN_Click(object sender, RoutedEventArgs e)
        {
            AddTraining addTrainingForm = new AddTraining(selectedTraining);
            addTrainingForm.ShowDialog();
            if (addTrainingForm.DialogResult == true)
            {
                this.selectedTraining = addTrainingForm.selectedTraining;
                trainingLB.Items.Clear();
                foreach (Training training in selectedTraining)
                {
                    trainingLB.Items.Add(training.Name + " - " + training.Time + " | " + training.Price + " грн.");
                }
                trainingLB.SelectAll();
            }
        }

        private void awayTrainingBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAwayTraining addAwayTrainingForm = new AddAwayTraining(selectedAwayTraining);
            addAwayTrainingForm.ShowDialog();
            if (addAwayTrainingForm.DialogResult == true)
            {
                this.selectedAwayTraining = addAwayTrainingForm.selectedAwayTraining;
                awayTrainingLB.Items.Clear();
                foreach (AwayTraining training in selectedAwayTraining)
                {
                    awayTrainingLB.Items.Add(training.Name + " - " + training.Time + " | " + training.Price + " грн.");
                }
                awayTrainingLB.SelectAll();
            }
        }

        private void achievementsBTN_Click(object sender, RoutedEventArgs e)
        {
            AddAchievements addAchievementsForm = new AddAchievements(addedAchievement);
            addAchievementsForm.ShowDialog();
            if (addAchievementsForm.DialogResult == true)
            {
                this.addedAchievement = addAchievementsForm.addedAchievement;
                achievementsLB.Items.Clear();
                foreach (Achievement achievement in addedAchievement)
                {
                    achievementsLB.Items.Add(achievement.Name + " - " + achievement.Place + " | " + achievement.Status);
                }
                achievementsLB.SelectAll();
            }
        }

        private void workplaceBTN_Click(object sender, RoutedEventArgs e)
        {
            AddWorkPlace addWorkPlacesForm = new AddWorkPlace(addedWorkplaces, cityCB.Text);
            addWorkPlacesForm.ShowDialog();
            if (addWorkPlacesForm.DialogResult == true)
            {
                this.addedWorkplaces = addWorkPlacesForm.addedWorkplaces;
                workplaceLB.Items.Clear();
                foreach (WorkPlace workPlace in addedWorkplaces)
                {
                    workplaceLB.Items.Add(workPlace.Name);
                }
                workplaceLB.SelectAll();
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            string fullName = nameTB.Text;

            Gender gender;
            if ((bool)(genderSP.Children[0] as RadioButton).IsChecked) gender = Gender.Чоловік;
            else gender = Gender.Жінка;

            DateTime? birthday = birthdateDP.SelectedDate;

            string imageUrl;
            if (openFileDialog != null)
            {
                string filename = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                string coachImage = fullName.Split(' ')[1].Unidecode() + filename.Substring(filename.LastIndexOf('.'));
                System.IO.File.Copy(openFileDialog.FileName, "..//..//Images//" + coachImage);
                imageUrl = "Images/" + coachImage;
            }
            else
            {
                imageUrl = "Images/no-image.png";
            }

            string email = emailTB.Text;

            Phone phone = new Phone { Number = phoneTB.Text };

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
                addedPhones.Add(phone);

                Coach coach = new Coach() { FullName=fullName, Gender=gender, DateOfBirth=(DateTime)birthday, PhotoUrl=imageUrl, Email=email, Phone= addedPhones, Languages=selectedLanguage, Sport=sport, Rank=rank, Experience=experience, Diplomas=selectedDimplomas, Courses= selectedCourses, Certificates=selectedCertificates, Achievements=addedAchievement, City=city, WorkPlaces=addedWorkplaces, Training= selectedTraining, AwayTraining=selectedAwayTraining, Schedule=sheduleResult };
                db.Coaches.Add(coach);
                db.SaveChanges();
                this.DialogResult = true;
                Close();
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Заповніть обов'язкові поля", "Виникла помилка");
            }
        }
    }
}
