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
            Console.WriteLine(" Durchmesser:");
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
                double durchmesserausgabe = Durchmesser(0);
                Console.Clear();
                string schraubenkopfausgabe = Schraubenkopf(0);
                Console.Clear();
                string materialausgabe = Material(0);
                Console.Clear();
                string gewindeausgabe = Gewinde(0);
                Console.Clear();

                Console.WriteLine("\n\n\n Folgende Eingabeparameter werden übermittelt..");
                Console.WriteLine("\n\n Länge: " + laengenausgabe + " mm");
                Console.WriteLine(" Durchmesser: " + durchmesserausgabe + " mm");
                Console.WriteLine(" Schraubenkopf: " + schraubenkopfausgabe);
                Console.WriteLine(" Material: " + materialausgabe);
                Console.WriteLine(" Gewinde: " + gewindeausgabe);

                Console.WriteLine("\n\n Berechnete Größen: ");

                //Hier kommen die Methoden für die Berechnungen hin..

                Tabellen();

                Berechnungen();

                Console.WriteLine("Anhand der eingegebenen Parameter konnten folgende zusätzliche Daten ermittelt werden");

                //Ausgabe der errechneten Daten
                

                Console.ReadKey();

                Console.WriteLine("\n\n Neu berechnen? (j/n)");
                auswahl = Console.ReadLine();
            }
            while (auswahl == "j");

        }
        //Hauptprogramm Ende







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

    static public void Berechnungen (double durchmesserausgabe)
        {

            //Duchgangbohrung Durchmesser
            double Durchgangsbohrung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // String der in der Tabelle steht in einen int umwandeln
            for (jj=0; jj<8; jj++ ) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32 Tabelle[jj, 0]; //umwandeln der Strings in der Tabelle in int
                if(durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Durchgangsbohrung = Tabellen[jj, 1]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben

                }
                else
                {

                }
                
            }
              // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
           



            double maxBelastung = 0;
            double Preis = 0;
            string Norm = "norm";
            double Masse = 0;
            double Mindesteinschraubtiefe = 0;
            double Senktiefe = 0;
            double Senkdurchmesser = 0;

        }
        
    static public void Tabellen ()
        {
            // die Werte können nicht mit Formeln errechnet werden, sondern sind auf diese Tabellenwerte genormt
            // deswegen wollten wir die als Tabelle hinterlegen um sie bei den Berechnungen bzw. Ausgaben zu verwenden

            string[,] Tabelle = new string[9, 5];

            //Gewinde Nenndurchmesser
            Tabelle[0, 0] = "3";
            Tabelle[1, 0] = "4";
            Tabelle[2, 0] = "5";
            Tabelle[3, 0] = "6";
            Tabelle[4, 0] = "8";
            Tabelle[5, 0] = "10";
            Tabelle[6, 0] = "12";
            Tabelle[7, 0] = "16";
            Tabelle[8, 0] = "20";

            //Durchgangsloch Durchmesser
            Tabelle[0, 1] = "3,4";
            Tabelle[1, 1] = "4,5";
            Tabelle[2, 1] = "5,5";
            Tabelle[3, 1] = "6,6";
            Tabelle[4, 1] = "9";
            Tabelle[5, 1] = "11";
            Tabelle[6, 1] = "13,5";
            Tabelle[7, 1] = "17,5";
            Tabelle[8, 1] = "22";

            //ISO 4762 Senkdurchmesser Zylinderkopfschraube
            Tabelle[0, 2] = "6,5";
            Tabelle[1, 2] = "8";
            Tabelle[2, 2] = "10";
            Tabelle[3, 2] = "11";
            Tabelle[4, 2] = "15";
            Tabelle[5, 2] = "18";
            Tabelle[6, 2] = "20";
            Tabelle[7, 2] = "26";
            Tabelle[8, 2] = "33";

            //ISO 4762 Senktiefe Zylinderkopfschraube
            Tabelle[0, 3] = "3,4";
            Tabelle[1, 3] = "4,4";
            Tabelle[2, 3] = "5,4";
            Tabelle[3, 3] = "6,4";
            Tabelle[4, 3] = "8,6";
            Tabelle[5, 3] = "10,6";
            Tabelle[6, 3] = "12,6";
            Tabelle[7, 3] = "16,6";
            Tabelle[8, 3] = "20,6";

            // Durchmesser Kegelsenkung
            Tabelle[0, 4] = "6,9";
            Tabelle[1, 4] = "9,2";
            Tabelle[2, 4] = "11,5";
            Tabelle[3, 4] = "13,7";
            Tabelle[4, 4] = "18,3";
            Tabelle[5, 4] = "22,7";
            Tabelle[6, 4] = "27,2";
            Tabelle[7, 4] = "34";
            Tabelle[8, 4] = "40,7";
        }

    }

}