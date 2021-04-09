﻿using System;
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
            Console.WriteLine(" Steigung:");
            Console.WriteLine(" Festigkeiten");
            Console.WriteLine(" Durchmesser der Durchgangsbohrung:");
            Console.WriteLine(" Senkungen");


          Console.WriteLine("\n\n\n Zum Fortfahren ENTER drücken..");
            Console.ReadKey();

            string auswahl;
            do
            {
                double laengenausgabe = Laenge(0);
                Console.Clear();
                double durchmesserausgabe = Durchmesser(0);
                Console.Clear();
                (string schraubenkopfausgabe, int Schraubenkopfnummer) = Schraubenkopf();
                Console.Clear();
                (string materialklasse, double zugfestigkkeit, double streckgrenze) = Material();
                Console.Clear();
                (string gewindeausgabe, int Gewindenummer) = Gewinde();
                Console.Clear();

                //Steigung muss in der Konsole vom Bediener eingegeben werden wenn nicht metrisches Regelgewinde ausgewählt wurde
                double Steigung = 0;

                if (Gewindenummer == 1)
                {Steigung = 0; }
                

                Console.WriteLine("\n\n\n Folgende Eingabeparameter werden übermittelt..");
                Console.WriteLine("\n\n Länge: " + laengenausgabe + " mm");
                Console.WriteLine(" Durchmesser: " + durchmesserausgabe + " mm");
                Console.WriteLine(" Schraubenkopf: " + schraubenkopfausgabe);
                Console.WriteLine(" Material: " + materialklasse);
                Console.WriteLine(" Gewinde: " + gewindeausgabe);

                Console.WriteLine("\n\n Berechnete Größen: \n");

                //Hier kommen die Methoden für die Berechnungen hin..
                Console.WriteLine("Rm = " + zugfestigkkeit + " MPa");
                Console.WriteLine("Re = " + streckgrenze + " MPa\n");

                //Steigung des metrischen Regelgewindes
                if (Gewindenummer == 1)
                { Console.WriteLine("Steigung des metrischen Regelgewindes:  " + BerechnungSteigung(Tabellen(), durchmesserausgabe) + " mm"); }

                //Durchgangsbohrung
                Console.WriteLine("Durchmesser der Durchgangsbohrung:  " + BerechnungDurchgangsbohrung(Tabellen() ,durchmesserausgabe)+" mm");

                //Senkdurchmesser Zylinderschraube
                if (Schraubenkopfnummer == 2)
                { Console.WriteLine("Durchmesser der Senkung für Zylinderkopf:  " + BerechnungSenkdurchmesser(Tabellen(), durchmesserausgabe) + " mm"); }

                //Senktiefe Zylinderschraube
                if (Schraubenkopfnummer == 2)
                { Console.WriteLine("Tiefe der Senkung für Zylinderkopf:  " + BerechnungSenktiefe(Tabellen(), durchmesserausgabe) + " mm"); }

                //Durchmesser Kegelsenkung
                if (Schraubenkopfnummer == 3)
                { Console.WriteLine("Durchmesser der Senkung für Senkschrauben:  " + BerechnungDurchmesserKegelsenkung(Tabellen(), durchmesserausgabe) + " mm"); }

                //Kernlochdurchmesser
                if (Gewindenummer == 1 |Gewindenummer == 2 | Gewindenummer == 3)
                { Console.WriteLine("Durchmesser der Kernlochbohrung:  " + BerechnungKernlochbohrung(durchmesserausgabe, Steigung, Tabellen()) + " mm"); }

                //Maximale Belastung der Schraube
                if (Gewindenummer == 1 | Gewindenummer ==2 )
                { Console.WriteLine("Maximal zulässige Belastung:  " + string.Format("{0:0.00}", (BerechnungMaxBelastung(durchmesserausgabe, Steigung, Tabellen(), streckgrenze))) + "N"); }


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

        static public (string, int) Schraubenkopf()
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

            return (schraubenart, schraubenauswahl);
        }

        public static  (string, double, double) Material()
        {
            int materialauswahl;
            string materialart = "0";
            double materialzugfestigkeit=0;
            double materialstreckgrenze = 0;


            Console.WriteLine("\n Material auswählen: ");
            Console.WriteLine("\n <1> Stahl 8.8");
            Console.WriteLine(" <2> Stahl 10.9");
            Console.WriteLine(" <3> Stahl 12.9");
            Console.WriteLine(" <4> Nichtrostender Stahl A4-50");


            materialauswahl = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (materialauswahl)
            {
                case 1:
                    materialart = "Stahl 8.8";
                    materialzugfestigkeit = 800;
                    materialstreckgrenze = 640;

                    break;
                case 2:
                    materialart = "Stahl 10.9";
                    materialzugfestigkeit = 1000;
                    materialstreckgrenze = 900;
                    break;
                case 3:
                    materialart = "Stahl 12.9";
                    materialzugfestigkeit = 1200;
                    materialstreckgrenze = 1080;
                    break;
                case 4:
                    materialart = "Nichtrostender Stahl A4-50";
                    materialzugfestigkeit = 500;
                    materialstreckgrenze = 210;
                    break;
                default:
                    materialart = "-/-";
                    break;
            }

            
            Console.WriteLine("\n Gewähltes Material: " + materialart+" mit \n\n Rm = "+materialzugfestigkeit+" MPa und \n Re = "+materialstreckgrenze+" MPa");
            Console.ReadKey();

            return (materialart, materialzugfestigkeit, materialstreckgrenze);
            
        }

        static public (string, int) Gewinde()
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

            return (gewindeart, gewindeauswahl);
        }

    static public double BerechnungDurchgangsbohrung ( double[,]Tabelle ,double durchmesserausgabe) // 
        {

            //Duchgangbohrung Durchmesser
            double Durchgangsbohrung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj=0; jj<=8; jj++ ) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                if(durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Durchgangsbohrung = Tabelle[jj, 1]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                } 
            }
            return Durchgangsbohrung;
            
              // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }

        static public double BerechnungSteigung(double[,] Tabelle, double durchmesserausgabe) // 
        {

            //Duchgangbohrung Durchmesser
            double Steigung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                }
            }
            return Steigung;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }

        static public double BerechnungSenkdurchmesser(double[,] Tabelle, double durchmesserausgabe) // 
        {

            //Duchgangbohrung Durchmesser
            double Senkdurchmesser = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Senkdurchmesser = Tabelle[jj, 2]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben
                }
            }
            return Senkdurchmesser;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }

        static public double BerechnungSenktiefe(double[,] Tabelle, double durchmesserausgabe) // 
        {

            //Duchgangbohrung Durchmesser
            double Senktiefe = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    Senktiefe = Tabelle[jj, 3]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben
                }
            }
            return Senktiefe;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }

        static public double BerechnungDurchmesserKegelsenkung(double[,] Tabelle, double durchmesserausgabe) // 
        {

            //Duchgangbohrung Durchmesser
            double DurchmesserKegelsenkung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                    DurchmesserKegelsenkung = Tabelle[jj, 4]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben
                }
            }
            return DurchmesserKegelsenkung;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }


        static public double BerechnungKernlochbohrung(double durchmesserausgabe, double Steigung, double[,] Tabelle)
        {
            int jj = 0;
            int M = 0;
            if(Steigung == 0)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben
                    }
                }
            }

            double Kerndurchmesser = durchmesserausgabe - Steigung;

            return Kerndurchmesser;
        }


        static public double BerechnungMaxBelastung(double durchmesserausgabe, double Steigung, double[,] Tabelle, double Streckgrenze)
        {
            int jj = 0;
            int M = 0;
            if (Steigung == 0)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben
                    }
                }
            }
            
            double MaxBelastung = (Math.Pow( ( ( (durchmesserausgabe - 0.6495* Steigung)+(durchmesserausgabe -1.2269* Steigung) )/2 ), 2) ) * Math.PI * 0.25 * Streckgrenze;

            return MaxBelastung;
        }


        static public double[,] Tabellen () // Funktion die ein Array zurückgeben soll
        {
            // die Werte können nicht mit Formeln errechnet werden, sondern sind auf diese Tabellenwerte genormt
            // deswegen wollten wir die als Tabelle hinterlegen um sie bei den Berechnungen bzw. Ausgaben zu verwenden

            double[,] Tabelle = new double[9, 6];

            //Gewinde Nenndurchmesser
            Tabelle[0, 0] = 3;
            Tabelle[1, 0] = 4;
            Tabelle[2, 0] = 5;
            Tabelle[3, 0] = 6;
            Tabelle[4, 0] = 8;
            Tabelle[5, 0] = 10;
            Tabelle[6, 0] = 12;
            Tabelle[7, 0] = 16;
            Tabelle[8, 0] = 20;

            //Durchgangsloch Durchmesser
            Tabelle[0, 1] = 3.4;
            Tabelle[1, 1] = 4.5;
            Tabelle[2, 1] = 5.5;
            Tabelle[3, 1] = 6.6;
            Tabelle[4, 1] = 9;
            Tabelle[5, 1] = 11;
            Tabelle[6, 1] = 13.5;
            Tabelle[7, 1] = 17.5;
            Tabelle[8, 1] = 22;

            //ISO 4762 Senkdurchmesser Zylinderkopfschraube
            Tabelle[0, 2] = 6.5;
            Tabelle[1, 2] = 8;
            Tabelle[2, 2] = 10;
            Tabelle[3, 2] = 11;
            Tabelle[4, 2] = 15;
            Tabelle[5, 2] = 18;
            Tabelle[6, 2] = 20;
            Tabelle[7, 2] = 26;
            Tabelle[8, 2] = 33;

            //ISO 4762 Senktiefe Zylinderkopfschraube
            Tabelle[0, 3] = 3.4;
            Tabelle[1, 3] = 4.4;
            Tabelle[2, 3] = 5.4;
            Tabelle[3, 3] = 6.4;
            Tabelle[4, 3] = 8.6;
            Tabelle[5, 3] = 10.6;
            Tabelle[6, 3] = 12.6;
            Tabelle[7, 3] = 16.6;
            Tabelle[8, 3] = 20.6;

            // Durchmesser Kegelsenkung
            Tabelle[0, 4] = 6.9;
            Tabelle[1, 4] = 9.2;
            Tabelle[2, 4] = 11.5;
            Tabelle[3, 4] = 13.7;
            Tabelle[4, 4] = 18.3;
            Tabelle[5, 4] = 22.7;
            Tabelle[6, 4] = 27.2;
            Tabelle[7, 4] = 34;
            Tabelle[8, 4] = 40.7;

            //Steigung metrisches Regelgewinde
            Tabelle[0, 5] = 0.5;
            Tabelle[1, 5] = 0.7;
            Tabelle[2, 5] = 0.8;
            Tabelle[3, 5] = 1;
            Tabelle[4, 5] = 1.25;
            Tabelle[5, 5] = 1.5;
            Tabelle[6, 5] = 1.75;
            Tabelle[7, 5] = 2;
            Tabelle[8, 5] = 2.5;

            return Tabelle;
        }

    }

}