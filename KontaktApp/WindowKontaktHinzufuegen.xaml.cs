using KontaktLib;
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

namespace KontaktApp
{
    /// <summary>
    /// Interaction logic for WindowKontaktHinzufuegen.xaml
    /// </summary>
    public partial class WindowKontaktHinzufuegen : Window
    {
        public Kontakt Kontakt { get; set; }

        public WindowKontaktHinzufuegen()
        {
            InitializeComponent();
        }

        public WindowKontaktHinzufuegen(Kontakt kontakt)
        {
            InitializeComponent();

            this.Kontakt = kontakt;
            TextBoxName.Text = Kontakt.Name;
            DatePickerGeburtstag.SelectedDate = Kontakt.Geburtsdatum;
            ComboBoxGeschlecht.SelectedIndex = Kontakt.Geschlecht - 1;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBoxName.Text.Trim();
            DateTime? geburtstag = DatePickerGeburtstag.SelectedDate;
            // Unser Geschlecht ist als Zahl von 1-3 definiert
            // SelectedIndex liefert einen Index zwischen 0-2 => +1 rechnen
            int geschlecht = ComboBoxGeschlecht.SelectedIndex + 1;

            //if (TextBoxName.Text.Trim() == "")
            if (string.IsNullOrEmpty(name) ||
                geburtstag == null ||
                geschlecht < 1)
            {
                MessageBox.Show("Bitte alles ausfüllen.");
                return;
            }

            if (geburtstag > DateTime.Now)
            {
                MessageBox.Show("Der Gerurtstag darf nicht in der Zukunft liegen.");
                return;
            }


            Kontakt = new Kontakt(name, geburtstag.Value, geschlecht);
            // Richtigen Status setzen + Fenster schließen
            DialogResult = true;
        }
    }
}
