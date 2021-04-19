using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{

    class Program
    {

        static void Main()
        {
            Console.Title = " Schraubenberechnung Team E";

            Console.WriteLine("\n\n\n Folgendes wird in diesem Programm ausgewertet..");
            Console.WriteLine("\n\n Gewinde:");
            Console.WriteLine(" Durchmesser:");
            Console.WriteLine(" Schraubenkopf:");
            Console.WriteLine(" Material:");
            Console.WriteLine(" Länge:");
            Console.WriteLine(" Steigung:");
            Console.WriteLine(" Festigkeiten:");
            Console.WriteLine(" Durchmesser der Durchgangsbohrung:");
            Console.WriteLine(" Senkungen:");


            Console.WriteLine("\n\n\n Zum Fortfahren ENTER drücken..");
            Console.ReadKey();

            string auswahl;
            do
            {
                double dichte = 0.00785;

                Console.WriteLine("\nGewinde auswählen..\n");

                Console.Clear();
                (string gewindeart, int gewindeauswahl, double flankenwinkel) = Gewinde();
                Console.Clear();
                double durchmessereingabe = Durchmesser(gewindeauswahl, Tabellen(), WitworthTabelle());
                Console.Clear();
                double steigung = Steigungseingabe(gewindeauswahl);
                Console.Clear();
                double laengenausgabegewinde = GewindeLaenge();
                Console.Clear();
                double laengenausgabeschaft = SchaftLaenge();
                Console.Clear();
                (string schraubenkopfausgabe, int Schraubenkopfnummer) = Schraubenkopf();
                Console.Clear();
                (string materialklasse, double zugfestigkkeit, double streckgrenze) = Material();
                Console.Clear();



                Console.WriteLine("\n\n\n Folgende Eingabeparameter werden übermittelt..");
                Console.WriteLine(" \n\n Gewindeart: " + gewindeart);
                Console.WriteLine(" Durchmesser: " + durchmessereingabe+" mm");
                Console.WriteLine(" Gewindelänge: " + laengenausgabegewinde + " mm");
                Console.WriteLine(" Schaftlänge: " + laengenausgabeschaft + " mm");
                double gesamtlänge = laengenausgabeschaft + laengenausgabegewinde;
                Console.WriteLine("Gesamtlänge: "+gesamtlänge+" mm");
                Console.WriteLine(" Schraubenkopf: " + schraubenkopfausgabe);
                Console.WriteLine(" Material: " + materialklasse);



                Console.WriteLine("\n\n Berechnete Größen: \n");

                //Hier kommen die Methoden für die Berechnungen:

                //Zugfestigkeit und Streckgrenze ausgeben
                Console.WriteLine("Rm = " + zugfestigkkeit + " MPa");
                Console.WriteLine("Re = " + streckgrenze + " MPa\n");
                Console.WriteLine("\nFlankenwinkel: " + flankenwinkel + "°\n");
                
                //Ausgabe metrisches Regelgewinde
                if (gewindeauswahl == 1)
                {
                    double preisMX = 0.08; // z.B. 0,08 €/g
                    Console.WriteLine("Steigung des metrischen Regelgewindes:  " + BerechnungSteigung(Tabellen(), durchmessereingabe) + " mm");

                    double gewindevolumenMX = Math.Round(GewindevolumenMX(durchmessereingabe, laengenausgabegewinde), 2);
                    Console.WriteLine("Gewindevolumen: " + gewindevolumenMX + " mm^3");

                    double schaftvolumenMX = Math.Round(SchaftvolumenMX(durchmessereingabe, laengenausgabeschaft), 2);
                    Console.WriteLine("Schaftvolumen: " + schaftvolumenMX + " mm^3");

                    double gewindemasseMX = Math.Round(dichte * gewindevolumenMX, 2);
                    Console.WriteLine("Gewindemasse: " + gewindemasseMX + " g");

                    double schaftmasseMX = Math.Round(dichte * schaftvolumenMX, 2);
                    Console.WriteLine("Schaftmasse: " + schaftmasseMX + " g");

                    double gewindepreisMX = Math.Round(preisMX * gewindemasseMX, 2);
                    double schaftpreisMX = Math.Round(preisMX * schaftmasseMX, 2);

                    double gesamtpreisMX = Math.Round(gewindepreisMX + schaftpreisMX,2);

                    Console.WriteLine("Preis: " + gesamtpreisMX + " Euro");
                }
                
                //Ausgabe metrisches Feingewinde
                if (gewindeauswahl == 2)
                {
                    double preisMF = 0.12; // z.B. 0,12€/g
                    Console.WriteLine("Ausgewählte Steigung: " + steigung + " mm");

                    double gewindevolumenMF = Math.Round(GewindevolumenMF(durchmessereingabe, laengenausgabegewinde, steigung), 2);
                    Console.WriteLine("Gewindevolumen: " + gewindevolumenMF + " mm^3");

                    double schaftvolumenMF = Math.Round(SchaftvolumenMF(durchmessereingabe, laengenausgabeschaft), 2);
                    Console.WriteLine("Schaftvolumen: " + schaftvolumenMF + " mm^3");

                    double gewindemasseMF = Math.Round(dichte * gewindevolumenMF, 2);
                    Console.WriteLine("Gewindemasse: " + gewindemasseMF + " g");

                    double schaftmasseMF = Math.Round(dichte * schaftvolumenMF, 2);
                    Console.WriteLine("Schaftmasse: " + schaftmasseMF + " g");

                    double gewindepreisMF = Math.Round(preisMF * gewindemasseMF, 2);
                    double schaftpreisMF = Math.Round(preisMF * schaftmasseMF, 2);

                    double gesamtpreisMF = Math.Round(gewindepreisMF + schaftpreisMF, 2);

                    Console.WriteLine("Preis: " + gesamtpreisMF + " Euro");
                }

                //Ausgabe Witworth-Gewinde
                if (gewindeauswahl == 3)
                {
                    double preisWW = 0.1; // z.B. 0,1€/g
                    (double Gangzahl, double WitworthSteigung) =   BerechnungWitworthSteigung(WitworthTabelle(), durchmessereingabe);
                    Console.WriteLine("Gangzahl des Whitworth-Gewindes:  " + Gangzahl) ;
                    Console.WriteLine("Steigung des Whitworth-Gewindes:  " + string.Format("{0:0.00}",WitworthSteigung) + " mm");

                    double gewindevolumenWW = Math.Round(GewindevolumenWW(durchmessereingabe, laengenausgabegewinde, WitworthSteigung), 2);
                    Console.WriteLine("Gewindevolumen: "+ gewindevolumenWW+" mm^3");

                    double schaftvolumenWW = Math.Round(SchaftvolumenWW(durchmessereingabe, laengenausgabeschaft), 2);
                    Console.WriteLine("Schaftvolumen: " + schaftvolumenWW + " mm^3");

                    double gewindemasseWW = Math.Round(dichte * gewindevolumenWW, 2);
                    Console.WriteLine("Gewindemasse: "+gewindemasseWW+" g");

                    double schaftmasseWW = Math.Round(dichte * schaftvolumenWW, 2);
                    Console.WriteLine("Schaftmasse: " + schaftmasseWW + " g");

                    double gewindepreisWW = Math.Round(preisWW * gewindemasseWW,2);
                    double schaftpreisWW = Math.Round(preisWW * schaftmasseWW, 2);

                    double gesamtpreisWW = Math.Round(gewindepreisWW + schaftpreisWW, 2);

                    Console.WriteLine("Preis: " + gesamtpreisWW + " Euro");
                }
                
                //Durchgangsbohrung
                if (gewindeauswahl != 3)
                { Console.WriteLine("Durchmesser der Durchgangsbohrung:  " + BerechnungDurchgangsbohrung(Tabellen(), durchmessereingabe) + " mm"); }

                //Senkdurchmesser Zylinderschraube
                if (Schraubenkopfnummer == 2)
                { Console.WriteLine("Durchmesser der Senkung für Zylinderkopf:  " + BerechnungSenkdurchmesser(Tabellen(), durchmessereingabe) + " mm"); }

                //Senktiefe Zylinderschraube
                if (Schraubenkopfnummer == 2)
                { Console.WriteLine("Tiefe der Senkung für Zylinderkopf:  " + BerechnungSenktiefe(Tabellen(), durchmessereingabe) + " mm"); }

                //Durchmesser Kegelsenkung
                if (Schraubenkopfnummer == 3 & gewindeauswahl != 3)
                { Console.WriteLine("Durchmesser der Senkung für Senkschrauben:  " + BerechnungDurchmesserKegelsenkung(Tabellen(), durchmessereingabe) + " mm"); }

                //Kernlochdurchmesser             
                 Console.WriteLine("Durchmesser der Kernlochbohrung:  " + BerechnungKernlochbohrung(durchmessereingabe, steigung, Tabellen(), gewindeauswahl, WitworthTabelle()) + " mm"); 

                //Maximale Belastung der Schraube für metrische Gewinde
               
                Console.WriteLine("Maximal zulässige Belastung:  " + string.Format("{0:0.00}", (BerechnungMaxBelastung(durchmessereingabe, steigung, Tabellen(), streckgrenze, gewindeauswahl, WitworthTabelle())/1000 ) )+ " kN"  );
                
                Console.ReadKey();

                Console.WriteLine("\n\n Neu berechnen? (j/n)");
                auswahl = Console.ReadLine();
            }
            while (auswahl == "j");

        }
        //Hauptprogramm Ende






        static public double GewindevolumenMX(double durchmessereingabe, double laengenausgabegewinde)
        {  
            double flankendurchmessre = durchmessereingabe - (0.6495 * (BerechnungSteigung(Tabellen(), durchmessereingabe)));
            double gewindevolumenMX = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
            
            return gewindevolumenMX;
        }

        static public double GewindevolumenMF(double durchmessereingabe, double laengenausgabegewinde, double steigung)
        {            
            double flankendurchmessre = durchmessereingabe - (0.6495 * steigung);
            double gewindevolumenMF = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
         
            return gewindevolumenMF;
        }

        static public double GewindevolumenWW(double durchmessereingabe, double laengenausgabegewinde, double WitworthSteigung)
        {            
            double flankendurchmessre = durchmessereingabe - (0.64 * WitworthSteigung);
            double gewindevolumenWW = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
            
            return gewindevolumenWW;
        }


        static public double SchaftvolumenMX(double durchmessereingabe, double laengenausgabeschaft)
        {
           // double flankendurchmessre = durchmessereingabe - (0.6495 * (BerechnungSteigung(Tabellen(), durchmessereingabe)));
            double schaftvolumenMX = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenMX;
        }

        static public double SchaftvolumenMF(double durchmessereingabe, double laengenausgabeschaft)
        {
            //double flankendurchmessre = durchmessereingabe - (0.6495 * steigung);
            double schaftvolumenMF = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenMF;
        }

        static public double SchaftvolumenWW(double durchmessereingabe, double laengenausgabeschaft)
        {
            //double flankendurchmessre = durchmessereingabe - (0.64 * WitworthSteigung);
            double schaftvolumenWW = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenWW;
        }


        //Eingabe der Gewindelänge
        static public double GewindeLaenge()
        {
            Console.Clear();
            double eingabeLaengegewinde;

            Console.WriteLine("\n Gewindelänge eingeben: \n");
            eingabeLaengegewinde = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("\n Gewählte Gewindelänge: " + eingabeLaengegewinde + " mm");
            Console.ReadKey();

            return eingabeLaengegewinde;
        }

        //Eingabe der Schaftlänge
        static public double SchaftLaenge()
        {
            Console.Clear();
            double eingabeLaengeSchaft;

            Console.WriteLine("\n Schaftlänge eingeben: \n");
            eingabeLaengeSchaft = Convert.ToDouble(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("\n Gewählte Schaftlänge: " + eingabeLaengeSchaft + " mm");
            Console.ReadKey();

            return eingabeLaengeSchaft;

        }

        //Eingabe des Durchmessers unabhängig von der Gewindeart
        public static double Durchmesser(int gewindeauswahl, double[,] Tabelle, string[,] Witworth)
        {
            Console.Clear();

            Console.WriteLine("\n Gewinde Durchmesser auswählen: \n");
            if (gewindeauswahl == 1 | gewindeauswahl == 2)
            {
                Console.WriteLine("\n <1> M3");
                Console.WriteLine(" <2> M4");
                Console.WriteLine(" <3> M5");
                Console.WriteLine(" <4> M6");
                Console.WriteLine(" <5> M8");
                Console.WriteLine(" <6> M10");
                Console.WriteLine(" <7> M12");
                Console.WriteLine(" <8> M16");
                Console.WriteLine(" <9> M20");
            }
            else
            {
                Console.WriteLine("\n <1> 1/4''");
                Console.WriteLine(" <2> 3/8''");
                Console.WriteLine(" <3> 1/2''");
                Console.WriteLine(" <4> 3/4''");
                Console.WriteLine(" <5> 1''");
                Console.WriteLine(" <6> 1 1/4''");
                Console.WriteLine(" <7> 1 1/2''");
                Console.WriteLine(" <8> 2''");
            }

            int de = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            double gewählterDurchmesser = 0;

            if (gewindeauswahl == 1 | gewindeauswahl == 2)
            {
                double ausgabe = Tabelle[de - 1, 0];
                gewählterDurchmesser = ausgabe;
                Console.WriteLine("\n Gewählter Durchmesser:  M" + ausgabe);
                Console.ReadKey();
            }
            else
            {
                string ausgabe = Witworth[de - 1, 0];
                gewählterDurchmesser = Double.Parse (Witworth[de - 1, 1]);
                Console.WriteLine("\n Gewählter Durchmesser: " + ausgabe + "''");
                Console.ReadKey();
            }


            return gewählterDurchmesser;
        }

        //Eingabe der Steigung abhängig von der Gewindeart
        public static double Steigungseingabe(int gewindeauswahl)
        {
            
            double steigung = 0;
            switch (gewindeauswahl)
            {
                case 1:
                    steigung = 0;
                    break;
                case 2:
                    Console.WriteLine("\n Steigung eingeben: \n");
                    steigung = Convert.ToDouble(Console.ReadLine());
                    break;
                case 3:
                    steigung = 0;
                    break;
                default:
                    steigung = 0;
                    break;
            }
            Console.Clear();
            if (gewindeauswahl == 2)
            {
                Console.WriteLine("\n Gewählte Steigung: " + steigung + " mm");
                Console.ReadKey();
            }

            return steigung;
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

        public static (string, double, double) Material()
        {
            int materialauswahl;
            string materialart = "0";
            double materialzugfestigkeit = 0;
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


            Console.WriteLine("\n Gewähltes Material: " + materialart + " mit \n\n Rm = " + materialzugfestigkeit + " MPa und \n Re = " + materialstreckgrenze + " MPa");
            Console.ReadKey();

            return (materialart, materialzugfestigkeit, materialstreckgrenze);

        }

        static public (string, int,double) Gewinde()
        {
            double flankenwinkel=0;
            int gewindeauswahl;
            string gewindeart = "0";

            Console.WriteLine("\n Gewinde auswählen: ");
            Console.WriteLine("\n <1> Metrisches Regelgewinde");
            Console.WriteLine(" <2> Metrisches Feingewinde");
            Console.WriteLine(" <3> Witworth Gewinde");

            gewindeauswahl = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (gewindeauswahl)
            {
                case 1:
                    gewindeart = "Metrisches Regelgewinde";
                    flankenwinkel = 60;
                    break;
                case 2:
                    gewindeart = "Metrisches Feingewinde";
                    flankenwinkel = 60;
                    break;
                case 3:
                    gewindeart = "Witworth Gewinde";
                    flankenwinkel = 55;                      
                    break;
                default:
                    gewindeart = "-/-";
                    break;
            }

            Console.WriteLine("\n Gewähltes Gewinde: " + gewindeart);
            Console.WriteLine("\n Flankenwinkel: " + flankenwinkel+"°");
            Console.ReadKey();

            return (gewindeart, gewindeauswahl, flankenwinkel);
        }




        static public double BerechnungDurchgangsbohrung(double[,] Tabelle, double durchmesserausgabe) // 
        {

            //Duchgangbohrung Durchmesser
            double Durchgangsbohrung = 0; // Wert der am Ende ausgegeben werden soll
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
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
                    Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird übergeben     
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
                    Senkdurchmesser = Tabelle[jj, 2]; // Wert aus der Tabelle wird übergeben
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
                    Senktiefe = Tabelle[jj, 3]; // Wert aus der Tabelle wird übergeben
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
                    DurchmesserKegelsenkung = Tabelle[jj, 4]; // Wert aus der Tabelle wird übergeben
                }
            }
            return DurchmesserKegelsenkung;

            // Wenn keine Übereinstimmung gefunden wurde sollte noch eine Meldung ausgegeben werden  
        }


        static public double BerechnungKernlochbohrung(double durchmesserausgabe, double Steigung, double[,] Tabelle, int Gewindeauswahl, string[,]Witworth)
        {
            int jj = 0;
            double M = 0;
            double Kerndurchmesser = 0;

            //Für metrische Gewinde
            if (Gewindeauswahl == 1| Gewindeauswahl == 2)
              {
                if (Steigung == 0)
                {
                    for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                    {
                        M = Convert.ToDouble(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                        if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                        {
                            Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird übergeben
                        }
                    }
                }

                Kerndurchmesser = durchmesserausgabe - Steigung;
              }

            //Für Whitworth Gewinde
            else   
            {
                for (jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kerndurchmesser = Convert.ToDouble(Witworth[jj, 4]); // Wert aus der Tabelle wird übergeben
                    }
                }
            }
            return Kerndurchmesser;
        }


        static public double BerechnungMaxBelastung(double durchmesserausgabe, double Steigung, double[,] Tabelle, double Streckgrenze, int Gewindeart, string[,]Witworth)
        {
            int jj = 0;
            double M = 0;
            double MaxBelastung = 0;
            if (Gewindeart == 1)
            {
                
                    for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                    {
                        M = Convert.ToDouble(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                        if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                        {
                            Steigung = Tabelle[jj, 5]; // Wert aus der Tabelle wird übergeben
                        }
                    }
                
            }

            MaxBelastung = (Math.Pow((((durchmesserausgabe - 0.6495 * Steigung) + (durchmesserausgabe - 1.2269 * Steigung)) / 2), 2)) * Math.PI * 0.25 * Streckgrenze;


            if (Gewindeart == 3)
            {
                double Spannungsquerschnitt = 0;

                for (jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Spannungsquerschnitt = Convert.ToDouble(Witworth[jj, 3]);
                    }
                }

                MaxBelastung = Streckgrenze * Spannungsquerschnitt;
            }

            return MaxBelastung;
        }


        //Steigung des Witworth Gewindes als Gangzahl und in mm
        static public (double, double) BerechnungWitworthSteigung (string [,] Witworth, double durchmessereingabe)
        {
            double Gangzahl = 0;

            for (int jj = 0; jj <= 7; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
            {
                 double M = Convert.ToDouble(Witworth[jj, 1]); //umwandeln der Strings in der Tabelle in double
                if (durchmessereingabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                {
                   Gangzahl = Convert.ToDouble(Witworth[jj, 2]); // Wert aus der Tabelle wird Gangzahl übergeben
                }
            }

            double Steigung = 25.4/ Gangzahl ;

            return (Gangzahl, Steigung);
        }

      
        static public double[,] Tabellen() // Funktion die ein Array zurückgibt
        {
            // die Werte können nicht mit Formeln errechnet werden, sondern sind auf diese Tabellenwerte genormt
            // deswegen haben wir die als Tabelle hinterlegt um sie bei den Berechnungen bzw. Ausgaben zu verwenden

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


        static public string[,] WitworthTabelle() // Funktion die ein Array zurückgibt
        {
            // die Werte können nicht mit Formeln errechnet werden, sondern sind auf diese Tabellenwerte genormt
            // deswegen haben wir die als Tabelle hinterlegt um sie bei den Berechnungen bzw. Ausgaben zu verwenden

            string[,] Witworth = new string[8, 6];

            //Gewinde Nenndurchmesser
            Witworth[0, 0] = "1/4";
            Witworth[1, 0] = "3/8";
            Witworth[2, 0] = "1/2";
            Witworth[3, 0] = "3/4";
            Witworth[4, 0] = "1";
            Witworth[5, 0] = "1 1/4";
            Witworth[6, 0] = "1 1/2";
            Witworth[7, 0] = "2";

            //Außendurchmesser
            Witworth[0, 1] = "6,35";
            Witworth[1, 1] = "9,53";
            Witworth[2, 1] = "12,7";
            Witworth[3, 1] = "19,05";
            Witworth[4, 1] = "25,4";
            Witworth[5, 1] = "31,75";
            Witworth[6, 1] = "38,1";
            Witworth[7, 1] = "50,8";

            //Gangzahl
            Witworth[0, 2] = "20";
            Witworth[1, 2] = "16";
            Witworth[2, 2] = "12";
            Witworth[3, 2] = "10";
            Witworth[4, 2] = "8";
            Witworth[5, 2] = "7";
            Witworth[6, 2] = "6";
            Witworth[7, 2] = "4,5";

            //Spannungsquerschnitt
            Witworth[0, 3] = "17,5";
            Witworth[1, 3] = "44,1";
            Witworth[2, 3] = "78,4";
            Witworth[3, 3] = "196";
            Witworth[4, 3] = "358";
            Witworth[5, 3] = "577";
            Witworth[6, 3] = "839";
            Witworth[7, 3] = "1491";

            //Kernlochdurchmesser
            Witworth[0, 4] = "4,72";
            Witworth[1, 4] = "7,49";
            Witworth[2, 4] = "9,99";
            Witworth[3, 4] = "15,8";
            Witworth[4, 4] = "21,34";
            Witworth[5, 4] = "27,10";
            Witworth[6, 4] = "32,68";
            Witworth[7, 4] = "43,57";

            //Flankendurchmesser
            Witworth[0, 5] = "5,54";
            Witworth[1, 5] = "8,51";
            Witworth[2, 5] = "11,35";
            Witworth[3, 5] = "17,42";
            Witworth[4, 5] = "23,37";
            Witworth[5, 5] = "29,43";
            Witworth[6, 5] = "35,39";
            Witworth[7, 5] = "47,19";
            return Witworth;

        }
    }
}