using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddAchievements.xaml
    /// </summary>
    public partial class AddAchievements : Window
    {
        private Context db;
        private List<Achievement> achievements;
        public List<Achievement> addedAchievement;
        public AddAchievements(List<Achievement> addedAchievement)
        {
            InitializeComponent();
            db = new Context();
            achievements = db.Achievements.ToList();
            this.addedAchievement = addedAchievement;
            if (addedAchievement.Count > 0) SetSelected();
            else AddFields();
        }

        private void AddFields(Achievement achievement = null)
        {
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(10);

            TextBox nameTB = new TextBox();
            HintAssist.SetHint(nameTB, "Досягнення");
            nameTB.FontSize = 14;
            nameTB.Margin = new Thickness(10);
            nameTB.Padding = new Thickness(0, 10, 10, 10);
            nameTB.TextWrapping = TextWrapping.Wrap;
            if (achievement != null)
                nameTB.Text = achievement.Name;
            else nameTB.Text = "";

            TextBox placeTB = new TextBox();
            HintAssist.SetHint(placeTB, "Місце");
            placeTB.FontSize = 14;
            placeTB.Margin = new Thickness(10);
            placeTB.Padding = new Thickness(0, 10, 10, 10);
            placeTB.TextWrapping = TextWrapping.Wrap;
            if (achievement != null)
                placeTB.Text = achievement.Place;
            else placeTB.Text = "";

            TextBox statusTB = new TextBox();
            HintAssist.SetHint(statusTB, "Статус досягнення");
            statusTB.FontSize = 14;
            statusTB.Margin = new Thickness(10);
            statusTB.Padding = new Thickness(0, 10, 10, 10);
            statusTB.TextWrapping = TextWrapping.Wrap;
            if (achievement != null)
                statusTB.Text = achievement.Status;
            else statusTB.Text = "";

            sp.Children.Add(nameTB);
            sp.Children.Add(placeTB);
            sp.Children.Add(statusTB);

            achievementsSP.Children.Add(sp);
        }

        private void SetSelected()
        {
            foreach (Achievement achievement in addedAchievement)
            {
                AddFields(achievement);
            }
        }

        private void AddSelectedAchievement()
        {
            addedAchievement.Clear();
            foreach (StackPanel sp in achievementsSP.Children)
            {
                TextBox nameTB = sp.Children[0] as TextBox;
                TextBox placeTB = sp.Children[1] as TextBox;
                TextBox statusTB = sp.Children[2] as TextBox;

                if (addedAchievement.Where(a => a.Name.Equals(nameTB.Text) && a.Place.Equals(placeTB.Text) && a.Status.Equals(statusTB.Text)).Any()) continue;
                else if (achievements.Where(a => a.Name.Equals(nameTB.Text) && a.Place.Equals(placeTB.Text) && a.Status.Equals(statusTB.Text)).Any())
                {
                    addedAchievement.Add(achievements.Where(a => a.Name.Equals(nameTB.Text) && a.Place.Equals(placeTB.Text) && a.Status.Equals(statusTB.Text)).FirstOrDefault());
                }
                else
                {
                    if (nameTB.Text.Length != 0 && placeTB.Text.Length != 0 && statusTB.Text.Length != 0)
                    {
                        Achievement achievement = new Achievement() { Name = nameTB.Text, Place = placeTB.Text, Status = statusTB.Text };
                        addedAchievement.Add(achievement);
                    }
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedAchievement();

            this.DialogResult = true;
            Close();
        }

        private void addFields_Click(object sender, RoutedEventArgs e)
        {
            AddFields();
        }
    }
}
