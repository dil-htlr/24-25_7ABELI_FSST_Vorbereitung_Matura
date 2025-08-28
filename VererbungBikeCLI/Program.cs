namespace VererbungBikeCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //AbstractBike abstractBike = new AbstractBike();
            MountainBike mountainBike = new MountainBike("M", 1000, "blue");
            EBike eBike = new EBike("eBike", 2000);
            GravelBike gravel = new GravelBike("gravel", 2400, "red");

            gravel.GetGearCount();

            Console.WriteLine(mountainBike);
            Console.WriteLine(eBike);

            // Man kann beispielsweise eine Liste der Elternklasse anlegen (muss nicht abstrakt sein)
            // Dadurch können in dieser Liste alle Objekte eingefügt werden, die entweder
            // der Elternklasse entsprechen, oder eine vererbte Version davon sind
            List<AbstractBike> bikes = new List<AbstractBike>();
            bikes.Add(eBike);
            bikes.Add(gravel);
            bikes.Add(mountainBike);

            foreach (AbstractBike bike in bikes)
            {
                // Wichtig: Wir können hier nur auf die Eigenschaften (Properties) und
                // Methoden zugreifen, die in AbstraktBike definiert sind, da wir ja
                // in der Liste diesen Datentypen angegeben haben.
                Console.WriteLine($"{bike.Name}, {bike.GetDiscount()} Euro");

                // Möchte man dennoch auf eine vererbte Eigenschaft zugreifen muss man eine
                // Typumwandlung machen
                if (bike is GravelBike)
                {
                    // Typumwandlung mittel "as",
                    // oder typecast: GravelBike gravelBike = (GravelBike)bike;
                    GravelBike gravelBike = bike as GravelBike;
                    // Nach der Typumwandlung können wir auch auf die spezialisierten
                    // Methoden der Unterklasse zugreifen
                    gravelBike.GetGearCount();
                }
            }

        }
    }
}
