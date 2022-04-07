using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddCourses.xaml
    /// </summary>
    public partial class AddCourses : Window
    {
        private Context db;
        private List<Course> courses;
        public List<Course> selectedCourses;

        public AddCourses(List<Course> selectedCourses)
        {
            InitializeComponent();
            db = new Context();
            courses = db.Courses.ToList();
            this.selectedCourses = selectedCourses;
            if (selectedCourses.Count > 0) SetSelected();
            else AddComboBox();
        }

        private void AddComboBox(string text = "")
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Margin = new Thickness(10);
            comboBox.FontSize = 14;
            comboBox.Text = text;
            comboBox.IsEditable = true;
            comboBox.ItemsSource = courses;
            comboBox.DisplayMemberPath = "Name";
            MaterialDesignThemes.Wpf.HintAssist.SetHint(comboBox, "Название курса");

            coursesSP.Children.Add(comboBox);
        }


        private void SetSelected()
        {
            foreach (Course course in selectedCourses)
            {
                AddComboBox(course.Name);
            }
        }

        private void AddSelectedCourses()
        {
            selectedCourses.Clear();
            foreach (ComboBox cb in coursesSP.Children)
            {
                if (selectedCourses.Where(c => c.Name.Equals(cb.Text)).Any()) continue;
                else if (courses.Where(c => c.Name.Equals(cb.Text)).Any())
                {
                    selectedCourses.Add(courses.Where(c => c.Name.Equals(cb.Text)).FirstOrDefault());
                }
                else
                {
                    if (cb.Text.Length != 0)
                    {
                        Course course = new Course() { Name = cb.Text };
                        selectedCourses.Add(course);
                        db.Courses.Add(course);
                    }
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedCourses();

            db.SaveChanges();
            this.DialogResult = true;
            Close();
        }

        private void addComboBox_Click(object sender, RoutedEventArgs e)
        {
            AddComboBox();
        }
    }
}
