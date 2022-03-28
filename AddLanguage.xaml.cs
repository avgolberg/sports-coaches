using Sports_Coaches.Models;
using System.Windows;

namespace Sports_Coaches
{
    /// <summary>
    /// Логика взаимодействия для AddLanguage.xaml
    /// </summary>
    public partial class AddLanguage : Window
    {
        private Context db;
        public AddLanguage()
        {
            InitializeComponent();
            db = new Context();
        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            if (languageTB.Text.Length!=0)
            {
                Language language = new Language() { Name = languageTB.Text };
                db.Languages.Add(language);
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
