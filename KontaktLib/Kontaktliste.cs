using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KontaktLib
{
    public class Kontaktliste
    {
        //public Kontakt[] kontakte = new Kontakt[10];

        public List<Kontakt> Kontakte { get; private set; } = new List<Kontakt>();

        public IReadOnlyList<Kontakt> KontakteReadOnly => Kontakte.AsReadOnly();


        public void Add(Kontakt kontakt)
        {
            Kontakte.Add(kontakt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Remove(int index)
        {
            // Abfangen, falls ein falscher Index übergeben wird
            if (index < 0 || index >= Kontakte.Count)
            {
                return;
            }

            Kontakte.RemoveAt(index);
        }


        public int[] StatistikGeschlecht()
        {
            int[] anzahlGeschlecht = new int[3];

            foreach (Kontakt kontakt in Kontakte)
            {
                // Variante 1: mit Prüfung des Geschlechts switch oder if
                
                // entsprechendes Geschlecht hochzählen für
                // die Statistik
                switch (kontakt.Geschlecht)
                {
                    // weiblich + 1
                    case 1: anzahlGeschlecht[0]++; break;
                    // männlich + 1
                    case 2: anzahlGeschlecht[1]++; break;
                    // divers + 1
                    case 3: anzahlGeschlecht[2]++; break;
                }

                // Variante 2: Geschlecht in einen Index umwandeln und direkt
                // ins array schreiben
                //anzahlGeschlecht[kontakt.Geschlecht - 1]++;
            }

            return anzahlGeschlecht;
        }

        public void DrawStatistikGeschlecht(Canvas canvas)
        {
            // Canvas löschen
            canvas.Children.Clear();

            // Geschlechterstatistik berechnen: [5, 10, 9] (w, m, d)
            int[] anzahlGeschlecht = StatistikGeschlecht();
            Brush[] farben = new Brush[3] {
                Brushes.Pink, Brushes.LightBlue, Brushes.LightGreen
            };

            double offsetX = 10;
            double offsetY = 10;
            double breite = 80;

            double hoeheMax = canvas.ActualHeight - 2 * offsetY;

            double anzahlMax = anzahlGeschlecht.Max();

            double posX = offsetX;
            for (int i = 0; i < anzahlGeschlecht.Length; i++)
            {
                double hoeheBalken = anzahlGeschlecht[i] / anzahlMax * hoeheMax;
                double posY = canvas.ActualHeight - offsetY - hoeheBalken;

                // Rechteck erzeugen
                Rectangle balken = new Rectangle()
                {
                    Width = breite,
                    Height = hoeheBalken,
                    Fill = farben[i]
                };

                // Rechteck platzieren
                Canvas.SetTop(balken, posY);
                Canvas.SetLeft(balken, posX);

                // Elment in den Canvas einfügen
                canvas.Children.Add(balken);

                // x-Position anpassen
                posX += breite + 5; // Breite + kleiner offset, damit balken nicht aufeinanderpicken
            }


        }

        public double StatistikAlterMittelwert()
        {
            // Divison durch 0 vermeiden!
            if (Kontakte.Count == 0)
                return 0;

            double summe = 0;

            foreach (Kontakt kontakt in Kontakte)
            {
                summe += kontakt.Alter;
            }

            return summe / Kontakte.Count;
        }

        public void UpdateListView(ListView listView)
        {
            // Löschen der bisherigen Einträge in der ListView
            listView.Items.Clear();

            // Alle Kontakte in die ListView einfügen
            foreach (Kontakt kontakt in Kontakte)
            {
                listView.Items.Add(kontakt);
            }
        }

        // Serialisierung/Deserialisierung

        public void Speichern(string pfad)
        {
            // StreamWriter erstellen mit dem wir in eine Datei schreiben könnnen
            // using ... Kontextmanager, der sich um das Schließen der Datei kümmert
            //           dies passiert auch wenn eine Exception geworften wird
            using (StreamWriter writer = new StreamWriter(pfad))
            {
                // Über alle Kontakte in der Liste iterieren
                foreach (Kontakt kontakt in Kontakte)
                {
                    // Serialisierten Kontakt in eine neue Zeile im File schreiben
                    writer.WriteLine(kontakt.Serialize());
                }
            }
        }

        public void Laden(string pfad)
        {
            // löschen der bisherigen Kontakte
            Kontakte.Clear();

            // Beim laden mit StreamReader ist die while loop einfacher,
            // da wir vorab nicht wissen wie viele Zeilen die Datei hat => einfach
            // so lange lesen bis das Ende der Datei erreicht ist.
            using (StreamReader reader = new StreamReader(pfad))
            {
                while (reader.EndOfStream == false)
                {
                    // Nächste Zeile vom Reader lesen lassen
                    string line = reader.ReadLine();

                    try
                    {
                        // Deserialisieren mit statischer Methode
                        // Zugriff auf statische Methode IMMER per Klassenname => kein Objekt erstellen
                        Kontakt neuerKontakt = Kontakt.Deserialize(line);
                        // Kontakt wieder in die Liste einfügen
                        Kontakte.Add(neuerKontakt);
                    }
                    catch (Exception ex)
                    {
                        // Typischerweise Fehler loggen, da wir das nicht kennen
                        // einfach still den Fehler übergehen
                    }
                }
            }
        }
    }
}
