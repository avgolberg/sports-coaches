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
        private List<Language> selectedLanguage;
        public List<Diploma> selectedDimplomas = new List<Diploma>();
        public AddCoach()
        {
            InitializeComponent();
            db = new Context();
            coach = new Coach();
            coach.Id = db.Coaches.Max(c => c.Id) + 1;

            selectedLanguage = new List<Language>();

            AddSports();
            AddLanguages();
            AddCities();
            AddRanks();
        }

        private void AddLanguages()
        {
            languagesLB.Items.Clear();
            foreach (Language language in db.Languages.OrderBy(l => l.Name))
            {
                languagesLB.Items.Add(language.Name);
            }
        }

        private void AddCities()
        {
            cityCB.Items.Clear();
            foreach (City city in db.Cities.OrderBy(c => c.Name))
            {
                cityCB.Items.Add(city.Name);
            }
        }

        private void AddSports()
        {
            sportCB.Items.Clear();
            foreach (Sport sport in db.Sports.OrderBy(s => s.Name))
            {
                sportCB.Items.Add(sport.Name);
            }
        }

        private void AddRanks()
        {
            foreach (Rank rank in db.Ranks.OrderBy(r => r.Name))
            {
                rankCB.Items.Add(rank.Name);
            }

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
                SelectLanguages(db.Languages.ToList().Last());
            }
        }

        private void SelectLanguages(Language newLanguage)
        {
            foreach (Language language in selectedLanguage)
            {
                languagesLB.SelectedItems.Add(language.Name);
            }
            languagesLB.SelectedItems.Add(newLanguage.Name);
        }

        private void languagesLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (string language in languagesLB.SelectedItems)
            {
                selectedLanguage.Add(db.Languages.Where(l => l.Name.Equals(language)).FirstOrDefault());
            }
        }

        private void sportBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSport addSportForm = new AddSport();
            addSportForm.ShowDialog();
            if (addSportForm.DialogResult == true)
            {
                AddSports();
                sportCB.Text = "";
                sportCB.SelectedItem = db.Sports.ToList().Last().Name;
            }
        }

        private void cityBTN_Click(object sender, RoutedEventArgs e)
        {
            AddCity addCityForm = new AddCity();
            addCityForm.ShowDialog();
            if (addCityForm.DialogResult == true)
            {
                AddCities();
                cityCB.Text = "";
                cityCB.SelectedItem = db.Cities.ToList().Last().Name;
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

        private void coursesBTN_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            //if (openFileDialog != null)
            //{
            //    string filename = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
            //    string sportImage = sportTB.Text.ToLower() + filename.Substring(filename.LastIndexOf('.'));
            //    System.IO.File.Copy(openFileDialog.FileName, "..//..//Images//" + sportImage);
            //    sport.ImageUrl = "Images/" + sportImage;
            //}
            //else
            //{
            //    sport.ImageUrl = "Images/no-image.png";
            //}
        }
    }
}
