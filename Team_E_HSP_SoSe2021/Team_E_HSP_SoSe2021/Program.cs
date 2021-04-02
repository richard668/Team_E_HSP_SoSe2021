using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = " Schraubenberechnung Team E";

            Console.WriteLine("\n\n\n Folgendes wird in diesem Programm ausgewertet..");
            Console.WriteLine("\n\n Länge:");
            Console.WriteLine(" Durchmessrer:");
            Console.WriteLine(" Schraubenkopf:");
            Console.WriteLine(" Material:");
            Console.WriteLine(" Gewinde:");
            Console.WriteLine("\n\n\n Zum Fortfahren ENTER drücken..");
            Console.ReadKey();

            string auswahl;
            do
            {
                double laengenausgabe = Laenge(0);
                Console.Clear();
                Console.Clear();
                string schraubenkopfausgabe = Schraubenkopf(0);
                Console.Clear();
                string materialausgabe = Material(0);
                Console.Clear();
                string gewindeausgabe = Gewinde(0);
                Console.Clear();

                Console.WriteLine("\n\n\n Folgende Eingabeparameter werden übermittelt..");
                Console.WriteLine("\n\n Länge: " + laengenausgabe + " mm");
                Console.WriteLine(" Durchmessrer: " + durchmesserausgabe + " mm");
                Console.WriteLine(" Schraubenkopf: " + schraubenkopfausgabe);
                Console.WriteLine(" Material: " + materialausgabe);
                Console.WriteLine(" Gewinde: " + gewindeausgabe);

                Console.WriteLine("\n\n Berechnete Größen: ");

                //Hier kommen die Methoden für die Berechnungen hin..

                Console.ReadKey();

                Console.WriteLine("\n\n Neu berechnen? (j/n)");
                auswahl = Console.ReadLine();
            }
            while (auswahl == "j");

        }

        static public double Laenge(int a)
        {
            Console.Clear();
            double eingabeLaenge;

            Console.WriteLine("\n Länge eingeben: \n");
            eingabeLaenge = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("\n Gewählte Länge: " + eingabeLaenge + " mm");
            Console.ReadKey();

            return eingabeLaenge;

        }

        static public double Durchmesser(int b)
        {
            Console.Clear();
            double eingabeDurchmesser;

            Console.WriteLine("\n Durchmesser eingeben: \n");
            eingabeDurchmesser = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("\n Gewählter Durchmesser: " + eingabeDurchmesser + " mm");
            Console.ReadKey();

            return eingabeDurchmesser;
        }

        static public string Schraubenkopf(int c)
        {
            Console.Clear();
            int schraubenauswahl;
            string schraubenart = "0";

            Console.WriteLine("\n Schraubenkopf auswählen: ");
            Console.WriteLine("\n <1> Sechskantschraube");
            Console.WriteLine(" <2> Zylinderschraube mit Innensechskant");
            Console.WriteLine(" <3> Senkkopf mit Innensechskant");

            schraubenauswahl = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (schraubenauswahl)
            {
                case 1:
                    schraubenart = "Sechskantschraube";
                    break;
                case 2:
                    schraubenart = "Zylinderschraube mit Innensechskant";
                    break;
                case 3:
                    schraubenart = "Senkkopf mit Innensechskant";
                    break;
                default:
                    schraubenart = "-/-";
                    break;
            }

            Console.WriteLine("\n Gewählter Kopf: " + schraubenart);
            Console.ReadKey();

            return schraubenart;
        }

        static public string Material(int d)
        {
            int materialauswahl;
            string materialart = "0";

            Console.WriteLine("\n Material auswählen: ");
            Console.WriteLine("\n <1> Material 1");
            Console.WriteLine(" <2> Material 2");

            materialauswahl = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (materialauswahl)
            {
                case 1:
                    materialart = "Material 1";
                    break;
                case 2:
                    materialart = "Material 2";
                    break;
                default:
                    materialart = "-/-";
                    break;
            }

            Console.WriteLine("\n Gewähltes Material: " + materialart);
            Console.ReadKey();

            return materialart;
        }

        static public string Gewinde(int e)
        {
            int gewindeauswahl;
            string gewindeart = "0";

            Console.WriteLine("\n Gewinde auswählen: ");
            Console.WriteLine("\n <1> Metrisches Regelgewinde");
            Console.WriteLine(" <2> Metrisches Feingewinde");
            Console.WriteLine(" <3> Trapezgewinde");
            Console.WriteLine(" <4> Rohrgewinde");

            gewindeauswahl = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (gewindeauswahl)
            {
                case 1:
                    gewindeart = "Metrisches Regelgewinde";
                    break;
                case 2:
                    gewindeart = "Metrisches Feingewinde";
                    break;
                case 3:
                    gewindeart = "Trapezgewinde";
                    break;
                case 4:
                    gewindeart = "Rohrgewinde";
                    break;
                default:
                    gewindeart = "-/-";
                    break;
            }

            Console.WriteLine("\n Gewähltes Gewinde: " + gewindeart);
            Console.ReadKey();

            return gewindeart;
        }

    }

}