using System;
using System.Collections.Generic;
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
            if (index < 0 || index >= Kontakte.Count) { 
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
    }
}
