using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
