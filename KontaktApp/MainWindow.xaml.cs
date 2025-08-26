using KontaktLib;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KontaktApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Collection Klasse die mehrere Kontakte für uns
        // verwaltet
        Kontaktliste kontaktliste = new Kontaktliste();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            // Objekt des Fensters erstellen
            WindowKontaktHinzufuegen window = new WindowKontaktHinzufuegen();

            // Fenster als Dialog öffnen => Hauptfenster kann nicht
            // verwendet werden
            if (window.ShowDialog() == true)
            {
                // Wenn OK gedrückt wurde, holen wir uns den
                // Kontakt über das öffentliche Property "Kontakt"
                // ab.
                // Dieses Property haben WIR hinzugefügt.
                Kontakt kontakt = window.Kontakt;

                kontaktliste.Add(kontakt);
                // UpdateListView
                kontaktliste.UpdateListView(ListViewKontakte);
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewKontakte.SelectedIndex < 0)
            {
                return;
            }

            kontaktliste.Remove(ListViewKontakte.SelectedIndex);
            kontaktliste.UpdateListView(ListViewKontakte);
        }

        private void ButtonReferenz_Click(object sender, RoutedEventArgs e)
        {
            Random rand =new Random();
            int randIndex = rand.Next(kontaktliste.Kontakte.Count);
            kontaktliste.Kontakte[randIndex] = new Kontakt("Ricky", DateTime.Now, 3);
            
            // Kontakt kann nicht verändert werden da es eine ReadOnly Liste ist.
            //kontaktliste.KontakteReadOnly[randIndex] = new Kontakt("Ricky", DateTime.Now, 3);

            Kontakt kontakt = kontaktliste.KontakteReadOnly[randIndex];

            kontaktliste.UpdateListView(ListViewKontakte);
        }
    }
}