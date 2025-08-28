using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VererbungBikeCLI
{
    // Abstrakte Klassen können nicht instanziert werden (new Bike() gehnt nicht)
    // Sie dienen rein als Elternklasse für die Vererbung => Sie geben das Schema vor
    public abstract class AbstractBike
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public AbstractBike() { }
        public AbstractBike(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }

        // Abstakte Methoden haben keine Implementierung
        // Sie müssen von den Kindklassen implementiert werden
        // => Zwang zur Implmentierung!
        public abstract double GetDiscount();

        public override string ToString()
        {
            return $"{Name}, {Price} Euro, Discount: {GetDiscount()} Euro";
        }
    }

    // Kindklassen
    public class EBike : AbstractBike
    {
        public EBike() { }
        public EBike(string name, double price) : base(name, price)
        {
        }

        public double GetBatteryStatus()
        {
            Random rand = new Random();
            return rand.Next() * 100;
        }

        public override double GetDiscount()
        {
            return Price * 0.9; // 10% Rabatt
        }
    }

    public class MountainBike : AbstractBike
    {
        public string Color { get; set; } = "Red";

        public MountainBike(string name, double price, string color) {
            Name = name;
            Price = price;
            Color = color;
        }

        // virtual bedeutet, dass diese Methode in einer Kind-Klasse
        // überschrieben werden darf (override)
        // ohne virtual, darf die Methode nicht überschrieben werden
        public virtual int GetGearCount()
        {
            return 30;
        }

        public override double GetDiscount()
        {
            return Price * 0.8; // 20% Rabatt
        }
    }

    public class GravelBike : MountainBike
    {
        public GravelBike(string name, double price, string color) : base(name, price, color)
        {
        }

        public override int GetGearCount()
        {
            return 15;
        }
    }

}
