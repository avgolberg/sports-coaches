using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddWorkPlace.xaml
    /// </summary>
    public partial class AddWorkPlace : Window
    {
        private Context db;
        private List<WorkPlace> workplaces;
        public List<WorkPlace> addedWorkplaces;
        private City selectedCity;
        public AddWorkPlace(List<WorkPlace> addedWorkplaces, City selectedCity = null)
        {
            InitializeComponent();
            db = new Context();
            workplaces = db.WorkPlaces.ToList();
            this.addedWorkplaces = addedWorkplaces;
            if(selectedCity != null)
                this.selectedCity = selectedCity;
            if (addedWorkplaces.Count > 0) SetSelected();
            else AddFields();
        }
        private void AddFields(WorkPlace workplace = null)
        {
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(10);

            ComboBox nameCB = new ComboBox();
            nameCB.Margin = new Thickness(10);
            nameCB.Padding = new Thickness(0, 10, 10, 10);
            nameCB.FontSize = 14;
            if (workplace != null)
                nameCB.Text = workplace.Name;
            else nameCB.Text = "";
            nameCB.IsEditable = true;
            if(selectedCity != null)
                nameCB.ItemsSource = db.WorkPlaces.Where(w => w.City.Id.Equals(selectedCity.Id)).ToList();
            else nameCB.ItemsSource = db.WorkPlaces.ToList();
            nameCB.DisplayMemberPath = "Name";
            HintAssist.SetHint(nameCB, "Место работы");
            nameCB.SelectionChanged += new SelectionChangedEventHandler(NameChanged);

            TextBox addressTB = new TextBox();
            HintAssist.SetHint(addressTB, "Адрес");
            addressTB.FontSize = 14;
            addressTB.Margin = new Thickness(10);
            addressTB.Padding = new Thickness(0, 10, 10, 10);
            addressTB.TextWrapping = TextWrapping.Wrap;
            if (workplace != null)
                addressTB.Text = workplace.Address;
            else addressTB.Text = "";

            ComboBox cityCB = new ComboBox();
            cityCB.Margin = new Thickness(10);
            cityCB.FontSize = 14;
            if (workplace != null)
                cityCB.SelectedItem = workplace.City;
            else cityCB.Text = "";
            cityCB.IsEditable = true;
            cityCB.ItemsSource = db.Cities.ToList();
            cityCB.DisplayMemberPath = "Name";
            HintAssist.SetHint(cityCB, "Город");

            sp.Children.Add(nameCB);
            sp.Children.Add(addressTB);
            sp.Children.Add(cityCB);

            workplacesSP.Children.Add(sp);
        }

        private void NameChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox nameCB = sender as ComboBox;
            WorkPlace workPlace = nameCB.SelectedItem as WorkPlace;

            StackPanel parentSP = nameCB.Parent as StackPanel;

            TextBox addressTB = parentSP.Children[1] as TextBox;
            ComboBox cityCB = parentSP.Children[2] as ComboBox;

            if (workPlace != null)
            {
                if (workPlace.Address != null && workPlace.Address.Length > 0)
                    addressTB.Text = workPlace.Address;
                if (workPlace.City != null && workPlace.City.Name.Length > 0)
                    cityCB.SelectedItem = workPlace.City;
            }
            else
            {
                addressTB.Text = "";
                cityCB.Text = "";
            }
        }

        private void SetSelected()
        {
            foreach (WorkPlace workplace in addedWorkplaces)
            {
                AddFields(workplace);
            }
        }

        private void AddSelectedAchievement()
        {
            addedWorkplaces.Clear();
            foreach (StackPanel sp in workplacesSP.Children)
            {
                ComboBox nameTB = sp.Children[0] as ComboBox;
                TextBox addressTB = sp.Children[1] as TextBox;
                ComboBox cityCB = sp.Children[2] as ComboBox;
                City city = cityCB.SelectedItem as City;

                if (addedWorkplaces.Where(a => a.Name.Equals(nameTB.Text) && a.Address.Equals(addressTB.Text) && a.City.Id.Equals(city.Id)).Any())
                {
                    WorkPlace workPlace = addedWorkplaces.Where(a => a.Name.Equals(nameTB.Text) && a.Address.Equals(addressTB.Text) && a.City.Id.Equals(city.Id)).FirstOrDefault();
                    db.WorkPlaces.Attach(workPlace);
                    addedWorkplaces.Add(workPlace);
                }
                else
                {
                    if (nameTB.Text.Length != 0 && addressTB.Text.Length != 0 && cityCB.Text.Length != 0)
                    {
                        if (!db.Cities.Where(c => c.Name.Equals(cityCB.Text)).Any())
                        {
                            City newCity = new City() { Name = cityCB.Text };
                            db.Cities.Add(newCity);
                        }
                        WorkPlace workplace = new WorkPlace() { Name = nameTB.Text, Address = addressTB.Text, City = db.Cities.Where(c => c.Name.Equals(cityCB.Text)).FirstOrDefault() };
                        addedWorkplaces.Add(workplace);
                        if(!workplaces.Where(a => a.Name.Equals(nameTB.Text) && a.Address.Equals(addressTB.Text) && a.City.Equals(db.Cities.Where(c => c.Name.Equals(cityCB.Text)).FirstOrDefault())).Any())
                            db.WorkPlaces.Add(workplace);
                    }
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedAchievement();

            db.SaveChanges();
            this.DialogResult = true;
            Close();
        }

        private void addFields_Click(object sender, RoutedEventArgs e)
        {
            AddFields();
        }
    }
}
