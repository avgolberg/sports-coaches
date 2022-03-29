using Sports_Coaches.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddTraining.xaml
    /// </summary>
    public partial class AddTraining : Window
    {
        private Context db;
        private List<Training> training;
        public List<Training> selectedTraining;
        public AddTraining(List<Training> selectedTraining)
        {
            InitializeComponent();
            db = new Context();
            training = db.Training.ToList();
            this.selectedTraining = selectedTraining;
            if (selectedTraining.Count > 0) SetSelected();
            else AddFields();
        }

        private void AddFields(Training training = null)
        {
            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness(10);

            TextBox nameTB = new TextBox();
            HintAssist.SetHint(nameTB, "Назва тренування");
            nameTB.FontSize = 14;
            nameTB.Margin = new Thickness(10);
            nameTB.Padding = new Thickness(0, 10, 10 , 10);
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

            trainingSP.Children.Add(sp);
        }

        private void SetSelected()
        {
            foreach (Training training in selectedTraining)
            {
                AddFields(training);
            }
        }

        private void AddSelectedTraining()
        {
            selectedTraining.Clear();
            foreach (StackPanel sp in trainingSP.Children)
            {
                TextBox nameTB = sp.Children[0] as TextBox;
                TextBox timeTB = sp.Children[1] as TextBox;
                TextBox priceTB = sp.Children[2] as TextBox;
                
                if (selectedTraining.Where(t => t.Name.Equals(nameTB.Text) && t.Time.Equals(timeTB.Text) && t.Price.Equals(int.Parse(priceTB.Text))).Any()) continue;
                else if (training.Where(t => t.Name.Equals(nameTB.Text) && t.Time.Equals(timeTB.Text) && t.Price.Equals(int.Parse(priceTB.Text))).Any())
                {
                    selectedTraining.Add(training.Where(t => t.Name.Equals(nameTB.Text) && t.Time.Equals(timeTB.Text) && t.Price.Equals(int.Parse(priceTB.Text))).FirstOrDefault());
                }
                else
                {
                    if (nameTB.Text.Length != 0 && timeTB.Text.Length != 0 && priceTB.Text.Length != 0)
                    {
                        Training training = new Training() { Name = nameTB.Text, Price= int.Parse(priceTB.Text), Time=timeTB.Text };
                        selectedTraining.Add(training);
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
