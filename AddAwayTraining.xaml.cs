using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddAwayTraining.xaml
    /// </summary>
    public partial class AddAwayTraining : Window
    {
        private Context db;
        private List<AwayTraining> awayTraining;
        public List<AwayTraining> selectedAwayTraining;
        public AddAwayTraining(List<AwayTraining> selectedAwayTraining)
        {
            InitializeComponent();
            db = new Context();
            awayTraining = db.AwayTraining.ToList();
            this.selectedAwayTraining = selectedAwayTraining;
            if (selectedAwayTraining.Count > 0) SetSelected();
            else AddFields();
        }

        private void AddFields(AwayTraining training = null)
        {
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(10);

            TextBox nameTB = new TextBox();
            HintAssist.SetHint(nameTB, "Назва тренування");
            nameTB.FontSize = 14;
            nameTB.Margin = new Thickness(10);
            nameTB.Padding = new Thickness(0, 10, 10, 10);
            nameTB.TextWrapping = TextWrapping.Wrap;
            if (training != null)
                nameTB.Text = training.Name;
            else nameTB.Text = "";

            TextBox timeTB = new TextBox();
            HintAssist.SetHint(timeTB, "Час тренування (наприклад, 1 год 20 хв)");
            timeTB.FontSize = 14;
            timeTB.Margin = new Thickness(10);
            timeTB.Padding = new Thickness(0, 10, 10, 10);
            timeTB.TextWrapping = TextWrapping.Wrap;
            if (training != null)
                timeTB.Text = training.Time;
            else timeTB.Text = "";

            TextBox priceTB = new TextBox();
            HintAssist.SetHint(priceTB, "Ціна");
            priceTB.FontSize = 14;
            priceTB.Margin = new Thickness(10);
            priceTB.Padding = new Thickness(0, 10, 10, 10);
            priceTB.TextWrapping = TextWrapping.Wrap;
            if (training != null)
                priceTB.Text = training.Price.ToString();
            else priceTB.Text = "";

            sp.Children.Add(nameTB);
            sp.Children.Add(timeTB);
            sp.Children.Add(priceTB);

            awayTrainingSP.Children.Add(sp);
        }

        private void SetSelected()
        {
            foreach (AwayTraining training in selectedAwayTraining)
            {
                AddFields(training);
            }
        }

        private void AddSelectedTraining()
        {
            selectedAwayTraining.Clear();
            foreach (StackPanel sp in awayTrainingSP.Children)
            {
                TextBox nameTB = sp.Children[0] as TextBox;
                TextBox timeTB = sp.Children[1] as TextBox;
                TextBox priceTB = sp.Children[2] as TextBox;

                if (selectedAwayTraining.Where(t => t.Name.Equals(nameTB.Text) && t.Time.Equals(timeTB.Text) && t.Price.Equals(int.Parse(priceTB.Text))).Any()) continue;
                else if (awayTraining.Where(t => t.Name.Equals(nameTB.Text) && t.Time.Equals(timeTB.Text) && t.Price.Equals(int.Parse(priceTB.Text))).Any())
                {
                    selectedAwayTraining.Add(awayTraining.Where(t => t.Name.Equals(nameTB.Text) && t.Time.Equals(timeTB.Text) && t.Price.Equals(int.Parse(priceTB.Text))).FirstOrDefault());
                }
                else
                {
                    if (nameTB.Text.Length != 0 && timeTB.Text.Length != 0 && priceTB.Text.Length != 0)
                    {
                        AwayTraining training = new AwayTraining() { Name = nameTB.Text, Price = int.Parse(priceTB.Text), Time = timeTB.Text };
                        selectedAwayTraining.Add(training);
                    }
                }
            }
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            AddSelectedTraining();

            this.DialogResult = true;
            Close();
        }

        private void addFields_Click(object sender, RoutedEventArgs e)
        {
            AddFields();
        }
    }
}
