
namespace KontaktLib
{
    public class Kontakt
    {
        /*
         * Verwendung von Snippets für die properties
         * 
         * - prop ... Auto Property
         * - propfull ... Voll ausimplementiertes Property 
         *                mit backing Variable
         * - propg ... reines Getter Property
         * 
         * Mit Tabulatortaste zwischen den änderbaren
         * Elementen weiterspringen. Enter wenn fertig.
         * 
         * Andere Snippets:
         * - for
         * - foreach
         * - switch
         * - if
         * - ...
         * 
         */

        // Auto Property (automatisch implemnetiert)
        public string Name { get; set; } = "Unbekannt";

        public DateTime Geburtsdatum { get; set; }

        // Backing field ... Die Variable die den Wert
        // für das entsprechende Propert (hier Geschlecht)
        // enthält.
        private int _geschlecht = 0;
        public int Geschlecht
        {
            get
            {
                // Wichtig: Hier die private Variable
                // (Backing Field) verwenden!
                return _geschlecht;
            } 
            set
            {
                // Wert prüfen, falls falsch -> Exception
                /*if (value > 0 && value <= 3)
                {
                    // value ... Wert der gesetzt werden soll
                    // quasi der Übergabeparameter des Setters
                    _geschlecht = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Geschlecht muss 1, 2 oder 3 sein.");
                }*/

                if (value < 1 || value > 3)
                {
                    throw new ArgumentException(
                        "Geschlecht muss 1, 2 oder 3 sein.");
                }

                _geschlecht = value;
            }
        }

        // Alternative Implementierung des Geschlechts
        // per Character statt Integer
        /*private char _geschlechtChar;

        public char GeschlechtChar
        {
            get { return _geschlechtChar; }
            set 
            { 
                if (_geschlechtChar != 'm' &&
                    _geschlechtChar != 'w' &&
                    _geschlechtChar != 'd')
                {
                    throw new ArgumentException("...");
                }
                
                _geschlechtChar = value; 
            }
        }*/

        public int Alter
        {
            get
            {
                /* Schlechte Implementierung da ungenau
                // Alter berechnen
                TimeSpan differenz = DateTime.Now - Geburtsdatum;
                // Ungenaue Berechnung, da Schaljahre nicht
                // richtig berücksichtigt werden.
                int alter = differenz.Days / 365;
                */

                // DateTime.Now ... aktuelles Datum und Uhrzeit
                // Wir gehen davon aus, dass der Geburtstag bereits war
                DateTime now = DateTime.Now;
                int alter = now.Year - Geburtsdatum.Year;

                // Falls der Geburtstag noch nicht war,
                // müssen wir 1 Jahr abziehen
                if (now.Month < Geburtsdatum.Month)
                {
                    // Definitiv noch nicht Geb. gehabt
                    alter--;
                }
                else if (now.Month == Geburtsdatum.Month &&
                         now.Day < Geburtsdatum.Day)
                {
                    // Tag des aktuellen Monats lieg noch vor dem
                    // Geburtsag => -1
                    alter--;
                }

                // Alternative
                /*
                bool istGeburtstagsmonatInZukunft = now.Month < Geburtsdatum.Month;
                bool istGeburtstagImMonatInZukunft = now.Month == Geburtsdatum.Month 
                                                        && now.Day < Geburtsdatum.Day;

                if (istGeburtstagImMonatInZukunft || 
                    istGeburtstagsmonatInZukunft)
                {
                    alter--;
                }*/

                return alter;
            }
        }




    }

}
