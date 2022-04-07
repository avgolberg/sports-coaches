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
using System.Data.Entity;
using System.IO;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddCoach.xaml
    /// </summary>
    public partial class ViewCoach : Window
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
        private Coach coach;
        private bool addSelectedLanguages = false;
        private bool isEditForm = false;
        public ViewCoach(int? coachId = null)
        {
            InitializeComponent();
            db = new Context();

            if (coachId != null)
            {
                coach = db.Coaches.Include("Sport").Include("City").Include("WorkPlaces").Include("Training").Include("Phone").Include("Languages").Include("City").Include("Diplomas").Include("Courses").Include("Certificates").Include("Certificates").Include("Rank").Include("Training").Include("AwayTraining").Include("Schedule").Include("Achievements").Where(c => c.Id.Equals((int)coachId)).First();
                isEditForm = true;
                AddCoachInfo(coach);
            }
        }

        private void AddCoachInfo(Coach coach)
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
                addedPhones = coach.Phone.ToList();
                AddSelectedPhones();
            }

            selectedLanguage = coach.Languages.ToList();
            AddSelectedLanguages();

            sportTB.Text = coach.Sport.Name;

            if (coach.Rank != null)
                rankTB.Text = coach.Rank.Name;

            cityTB.Text = coach.City.Name;

            experienceTB.Text = coach.Experience.ToString();

            selectedDimplomas = coach.Diplomas.ToList();
            if(selectedDimplomas.Count>0)
                AddSelectedDiplomas();

            selectedCourses = coach.Courses.ToList();
            if (selectedCourses.Count > 0)
                AddSelectedCourses();
            
            selectedCertificates = coach.Certificates.ToList();
            if (selectedCertificates.Count > 0)
                AddSelectedCertificates();

            addedAchievement = coach.Achievements.ToList();
            if (addedAchievement.Count > 0)
                AddSelectedAchievements();

            addedWorkplaces = db.Coaches.Include(c => c.WorkPlaces.Select(w => w.City)).Where(c => c.Id.Equals(coach.Id)).First().WorkPlaces.ToList();
            if (addedWorkplaces.Count > 0)
                AddSelectedWorkPlaces();

            selectedTraining = coach.Training.ToList();
            if (selectedTraining.Count > 0)
                AddSelectedTraining();

            selectedAwayTraining = coach.AwayTraining.ToList();
            if (selectedAwayTraining.Count > 0)
                AddSelectedAwayTraining();

            sheduleResult = coach.Schedule.ToList();
            if (sheduleResult.Count > 0)
                AddSelectedSchedule();
        }
       

        private void AddSelectedDiplomas()
        {
            foreach (Diploma diploma in selectedDimplomas)
            {
                diplomasSP.Children.Add(new TextBox { Text = diploma.Name, IsReadOnly = true, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10) });
            }
        }

        private void AddSelectedCourses()
        {
            foreach (Course course in selectedCourses)
            {
                coursesSP.Children.Add(new TextBox { Text = course.Name, IsReadOnly = true, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10) });
            }
        }

        private void AddSelectedCertificates()
        {
            foreach (Certificate certificate in selectedCertificates)
            {
                certificatesSP.Children.Add(new TextBox { Text = certificate.Name, IsReadOnly = true, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10) });
            }
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
            foreach (Training training in selectedTraining)
            {
                trainingSP.Children.Add(new TextBox { Text = training.Name + " - " + training.Time + " | " + training.Price + " у.е.", IsReadOnly = true, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10) });
            }
        }

        private void AddSelectedAwayTraining()
        {
            foreach (AwayTraining training in selectedAwayTraining)
            {
                awayTrainingSP.Children.Add(new TextBox { Text = training.Name + " - " + training.Time + " | " + training.Price + " у.е.", IsReadOnly = true, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10) });
            }
        }

        private void AddSelectedWorkPlaces()
        {
            foreach (WorkPlace workplace in addedWorkplaces)
            {
                workplaceSP.Children.Add(new TextBox { Text = workplace.Name, IsReadOnly = true, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10) });
            }
        }

        private void AddSelectedAchievements()
        {
            foreach (Achievement achievement in addedAchievement)
            {
                achievementsSP.Children.Add(new TextBox { Text = achievement.Name, IsReadOnly = true, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(10) });
            }
        }

        private void AddSelectedPhones()
        {
            foreach (Phone phone in addedPhones)
            {
                phonesSP.Children.Add(new TextBox { Text = phone.Number, IsReadOnly = true, TextWrapping=TextWrapping.Wrap, Margin=new Thickness(10) });
            }
        }

        private void AddSelectedLanguages()
        {
            languagesLB.DisplayMemberPath = "Name";
            foreach (Language language in selectedLanguage)
            {
                languagesLB.Items.Add(db.Languages.Where(l => l.Id.Equals(language.Id)).First());
            }
            languagesLB.SelectAll();
        }
    }
}
