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

            Console.WriteLine("\n\n\n Folgende Eingaben werden ausgewertet..\n\n");
            Console.WriteLine(" Gewindeauswahl:");
            Console.WriteLine(" Durchmesserauswahl:");
            Console.WriteLine(" Gewindelänge:");
            Console.WriteLine(" Schaftlänge:");
            Console.WriteLine(" Schraubenkopfauswahl:");
            Console.WriteLine(" Material:");
            Console.WriteLine(" (Steigung):");

            Console.WriteLine("\n\nFolgende Berechnungen werden ausgegeben..\n\n");

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
                double laengenausgabeschaft = SchaftLaenge(laengenausgabegewinde);
                Console.Clear();
                (string schraubenkopfausgabe, int Schraubenkopfnummer) = Schraubenkopf();
                Console.Clear();
            
                Console.Clear();
                (string materialklasse, double zugfestigkkeit, double streckgrenze) = Material();
                Console.Clear();


                Console.WriteLine("\n Folgende Eingabeparameter werden übermittelt..\n");
                Console.WriteLine(" Gewindeart: " + gewindeart);
                Console.WriteLine(" Durchmesser: " + durchmessereingabe+" mm");
                Console.WriteLine(" Gewindelänge: " + laengenausgabegewinde + " mm");
                Console.WriteLine(" Schaftlänge: " + laengenausgabeschaft + " mm"); //Gewindeloser Teil
                double gesamtlänge = laengenausgabeschaft + laengenausgabegewinde;
                Console.WriteLine(" Gesamtlänge: "+gesamtlänge+" mm");
                Console.WriteLine(" Schraubenkopf: " + schraubenkopfausgabe);
                Console.WriteLine(" Material: " + materialklasse);

                string schraubenbezeichnung = "";
                if (gewindeauswahl==1)
                {
                    schraubenbezeichnung = SchraubenbezeichnungMX(gewindeauswahl, Schraubenkopfnummer, durchmessereingabe, gesamtlänge, materialklasse);
                }

                if (gewindeauswahl==2)
                {
                    schraubenbezeichnung = SchraubenbezeichnungMF(gewindeauswahl, Schraubenkopfnummer, durchmessereingabe, steigung, gesamtlänge, materialklasse);
                }
                
               
                Console.WriteLine("\n "+schraubenbezeichnung);
                Console.WriteLine("\n\n Berechnete Größen:");

                //Hier kommen die Methoden für die Berechnungen:
                Console.WriteLine("\n Schraubengeometrie:\n");

                double gewindepreis = 0;



                //Ausgabe metrisches Regelgewinde
                if (gewindeauswahl == 1)
                {
                    gewindepreis=PreisberechnungMX(durchmessereingabe, laengenausgabegewinde, laengenausgabeschaft, dichte);
                }
                
                //Ausgabe metrisches Feingewinde
                if (gewindeauswahl == 2)
                {
                    gewindepreis=PreisberechnungMF(steigung, durchmessereingabe, laengenausgabegewinde, laengenausgabeschaft, dichte);
                }

                //Ausgabe Witworth-Gewinde
                if (gewindeauswahl == 3)
                {
                    gewindepreis = PreisberechnungWW(durchmessereingabe, laengenausgabegewinde, laengenausgabeschaft, dichte);
                }

                if (gewindeauswahl == 1)
                {
                    Console.WriteLine("Flankenwinkel: " + flankenwinkel + "°");
                }

                if (gewindeauswahl == 2)
                {
                    Console.WriteLine("Flankenwinkel: " + flankenwinkel + "°");
                }
                if (gewindeauswahl == 3)
                {
                    Console.WriteLine("Flankenwinkel: " + flankenwinkel + "°");
                }


                

                
                double kopfpreis = 0;
                
                //Schlüsselweite für metrische Gewinde
                if (gewindeauswahl != 3)
                {
                    double schlüsselweiteAusgabe = AusgabeSchlüsselweite(Schraubenkopfnummer, durchmessereingabe, Tabellen());
                    Console.WriteLine("Schlüsselweite: " + schlüsselweiteAusgabe + " mm");
                    
                    double kopfhöhe = (AusgabeKopfhöhe(Schraubenkopfnummer, durchmessereingabe, Tabellen()));
                    Console.WriteLine("Kopfhöhe: " + kopfhöhe+" mm");

                    double kopfvolumen = 0;
                    
                    if (Schraubenkopfnummer==1) //Sechskant
                    {
                        kopfvolumen = Sechskantvolumen(schlüsselweiteAusgabe, kopfhöhe);
                        kopfpreis = PreisberechnungSechskant(schlüsselweiteAusgabe, kopfhöhe, dichte);
                    }

                    if (Schraubenkopfnummer==2) //Zylinder
                    {
                        double kopfdurchmesserZylinder = AusgabeKopfdurchmesser(Schraubenkopfnummer, durchmessereingabe, Tabellen());
                        kopfvolumen = Zylindervolumen(kopfdurchmesserZylinder, kopfhöhe);
                        kopfpreis = PreisberechnungZylinder(Schraubenkopfnummer, durchmessereingabe, kopfhöhe, dichte);
                    }

                    if (Schraubenkopfnummer==3) //Senkkopf
                    {
                        double kopfdurchmesserSenkkopf = AusgabeKopfdurchmesser(Schraubenkopfnummer, durchmessereingabe, Tabellen());
                        kopfvolumen = Senkkopfvolumen(kopfdurchmesserSenkkopf, kopfhöhe, durchmessereingabe);
                        kopfpreis = PreisberechnungSenkkopf(Schraubenkopfnummer, durchmessereingabe, kopfhöhe, dichte);
                    }
                    //Console.WriteLine("Kopfvolumen: "+Math.Round(kopfvolumen,2)+" mm^3");
                    //Console.WriteLine("Kopfpreis: "+Math.Round(kopfpreis,2)+" Euro");
                }
                

               
                Console.WriteLine("\nFestigkeitswerte:\n");
                //Zugfestigkeit und Streckgrenze ausgeben
                Console.WriteLine("Rm = " + zugfestigkkeit + " MPa");
                Console.WriteLine("Re = " + streckgrenze + " MPa");
                Console.WriteLine("Maximal zulässige Belastung:  " + string.Format("{0:0.00}", (BerechnungMaxBelastung(durchmessereingabe, steigung, Tabellen(), streckgrenze, gewindeauswahl, WitworthTabelle())/1000 ) )+ " kN"  );

                Console.WriteLine("\nSonstiges: \n");
                //Kernlochdurchmesser             
                Console.WriteLine("Durchmesser der Kernlochbohrung:  " + BerechnungKernlochbohrung(durchmessereingabe, steigung, Tabellen(), gewindeauswahl, WitworthTabelle()) + " mm");

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
                
                double kaufpreis = Math.Round(gewindepreis + kopfpreis, 2);
                Console.WriteLine("Kaufpreis pro Schraube: " + kaufpreis + " Euro");

                Console.ReadKey();

                Console.WriteLine("\n\n Neu berechnen? (j/n)");
                auswahl = Console.ReadLine();
            }
            while (auswahl == "j");

        }
        //Hauptprogramm Ende








        static public string SchraubenbezeichnungMX(int gewindeauswahl, int schraubenkopfnummer,double durchmessereingabe,double gesamtlänge, string materialart)
        {
            string schraubenbezeichnung="";
            string gewindetyp = "";
            string bezugsnorm = "";
            string schraubenname = "";
            

            if (schraubenkopfnummer == 1 & gewindeauswahl == 1)//Sechskant Regelgewinde
            {
                schraubenname = "Sechskantschraube";
                bezugsnorm = "DIN EN ISO 4014";
                gewindetyp = "M";
            }

            if (schraubenkopfnummer == 2 & gewindeauswahl == 1)//Zylinder Regelgewinde
            {
                schraubenname = "Zylinderschraube";
                bezugsnorm = "DIN EN ISO 4762";
                gewindetyp = "M";
            }

            if (schraubenkopfnummer == 3 & gewindeauswahl == 1)//Senkschraube Regelgewinde
            {
                schraubenname = "Senkschraube";
                bezugsnorm = "DIN EN ISO 10642";
                gewindetyp = "M";
                
            }

            schraubenbezeichnung = schraubenname+" "+bezugsnorm+" - "+gewindetyp+durchmessereingabe+" x "+gesamtlänge+" - "+materialart;
            return schraubenbezeichnung;
        }

        static public string SchraubenbezeichnungMF(int gewindeauswahl, int schraubenkopfnummer, double durchmessereingabe, double steigung, double gesamtlänge, string materialart)
        {
            string schraubenbezeichnung = "";
            string gewindetyp = "";
            string bezugsnorm = "";
            string schraubenname = "";


            if (schraubenkopfnummer == 1 & gewindeauswahl == 2)//Sechskant Feingewinde
            {
                schraubenname = "Sechskantschraube";
                bezugsnorm = "DIN EN ISO 8765";
                gewindetyp = "M";

            }


            if (schraubenkopfnummer == 2 & gewindeauswahl == 2)//Sechskant Feingewinde
            {
                schraubenname = "Zylinderschraube";
                bezugsnorm = "DIN 34821";
                gewindetyp = "M";
            }

            schraubenbezeichnung = schraubenname + " " + bezugsnorm + " - " + gewindetyp + durchmessereingabe + " x " + steigung + " - " + gesamtlänge + " - " + materialart;
            return schraubenbezeichnung;
        }

        static public double PreisberechnungMX(double durchmessereingabe, double laengenausgabegewinde, double laengenausgabeschaft, double dichte)
        {
            double preisMX = 0.08; // z.B. 0,08 €/g
            Console.WriteLine("Steigung des metrischen Regelgewindes:  " + BerechnungSteigung(Tabellen(), durchmessereingabe) + " mm");

            double gewindevolumenMX = Math.Round(GewindevolumenMX(durchmessereingabe, laengenausgabegewinde), 2);
            //Console.WriteLine("Gewindevolumen: " + gewindevolumenMX + " mm^3");

            double schaftvolumenMX = Math.Round(SchaftvolumenMX(durchmessereingabe, laengenausgabeschaft), 2);
            //Console.WriteLine("Schaftvolumen: " + schaftvolumenMX + " mm^3");

            double gewindemasseMX = Math.Round(dichte * gewindevolumenMX, 2);
            //Console.WriteLine("Gewindemasse: " + gewindemasseMX + " g");

            double schaftmasseMX = Math.Round(dichte * schaftvolumenMX, 2);
            
            //Console.WriteLine("Schaftmasse: " + schaftmasseMX + " g");

            double gewindepreisMX = Math.Round(preisMX * gewindemasseMX, 2);
            double schaftpreisMX = Math.Round(preisMX * schaftmasseMX, 2);

            double gesamtpreisMX = Math.Round(gewindepreisMX + schaftpreisMX, 2);

            //Console.WriteLine("Gewindepreis: " + gesamtpreisMX + " Euro");

            return gesamtpreisMX;
        }

        static public double PreisberechnungWW(double durchmessereingabe, double laengenausgabegewinde, double laengenausgabeschaft, double dichte)
        {
            double preisWW = 0.1; // z.B. 0,1€/g
            (double Gangzahl, double WitworthSteigung) = BerechnungWitworthSteigung(WitworthTabelle(), durchmessereingabe);
            Console.WriteLine("Gangzahl des Whitworth-Gewindes:  " + Gangzahl);
            Console.WriteLine("Steigung des Whitworth-Gewindes:  " + string.Format("{0:0.00}", WitworthSteigung) + " mm");

            double gewindevolumenWW = Math.Round(GewindevolumenWW(durchmessereingabe, laengenausgabegewinde, WitworthSteigung), 2);
            //Console.WriteLine("Gewindevolumen: "+ gewindevolumenWW+" mm^3");

            double schaftvolumenWW = Math.Round(SchaftvolumenWW(durchmessereingabe, laengenausgabeschaft), 2);
            //Console.WriteLine("Schaftvolumen: " + schaftvolumenWW + " mm^3");

            double gewindemasseWW = Math.Round(dichte * gewindevolumenWW, 2);
            //Console.WriteLine("Gewindemasse: "+gewindemasseWW+" g");

            double schaftmasseWW = Math.Round(dichte * schaftvolumenWW, 2);
            //Console.WriteLine("Schaftmasse: " + schaftmasseWW + " g");

            double gewindepreisWW = Math.Round(preisWW * gewindemasseWW, 2);
            double schaftpreisWW = Math.Round(preisWW * schaftmasseWW, 2);

            double gesamtpreisWW = Math.Round(gewindepreisWW + schaftpreisWW, 2);

            //Console.WriteLine("Gewindepreis: " + gesamtpreisWW + " Euro");

            return gesamtpreisWW;

        }

        static public double PreisberechnungSechskant(double schlüsselweiteAusgabe, double kopfhöhe, double dichte)
        {
            double preisSechskant = 0.5;
            double kopfpreis = Sechskantvolumen(schlüsselweiteAusgabe, kopfhöhe)*dichte*preisSechskant;
            return kopfpreis;
        }

        static public double PreisberechnungZylinder(int Schraubenkopfnummer, double durchmessereingabe, double kopfhöhe, double dichte)
        {
            double preisZylinder = 0.2;
            double kopfdurchmesserZylinder = AusgabeKopfdurchmesser(Schraubenkopfnummer, durchmessereingabe, Tabellen());
            Console.WriteLine("Kopfdurchmesser: " + kopfdurchmesserZylinder + " mm");

            double kopfpreisZylinder = Zylindervolumen(kopfdurchmesserZylinder, kopfhöhe)*dichte*preisZylinder;
            return kopfpreisZylinder;
        }

        static public double PreisberechnungSenkkopf(int Schraubenkopfnummer, double durchmessereingabe, double kopfhöhe, double dichte)
        {
            double preisSenkkopf = 0.6;
            double kopfdurchmesserSenkkopf = AusgabeKopfdurchmesser(Schraubenkopfnummer, durchmessereingabe, Tabellen());
            Console.WriteLine("Kopfdurchmesser: " + kopfdurchmesserSenkkopf + " mm");

            double kopfpreisSenkkopf = Senkkopfvolumen(kopfdurchmesserSenkkopf, kopfhöhe, durchmessereingabe)*dichte*preisSenkkopf;

            return kopfpreisSenkkopf;
        }

        static public double PreisberechnungMF(double steigung, double durchmessereingabe, double laengenausgabegewinde, double laengenausgabeschaft, double dichte)
        {
            double preisMF = 0.12; // z.B. 0,12€/g
            Console.WriteLine("Ausgewählte Steigung: " + steigung + " mm");

            double gewindevolumenMF = Math.Round(GewindevolumenMF(durchmessereingabe, laengenausgabegewinde, steigung), 2);
            //Console.WriteLine("Gewindevolumen: " + gewindevolumenMF + " mm^3");

            double schaftvolumenMF = Math.Round(SchaftvolumenMF(durchmessereingabe, laengenausgabeschaft), 2);
            //Console.WriteLine("Schaftvolumen: " + schaftvolumenMF + " mm^3");

            double gewindemasseMF = Math.Round(dichte * gewindevolumenMF, 2);
            //Console.WriteLine("Gewindemasse: " + gewindemasseMF + " g");

            double schaftmasseMF = Math.Round(dichte * schaftvolumenMF, 2);
            //Console.WriteLine("Schaftmasse: " + schaftmasseMF + " g");

            double gewindepreisMF = Math.Round(preisMF * gewindemasseMF, 2);
            double schaftpreisMF = Math.Round(preisMF * schaftmasseMF, 2);

            double gesamtpreisMF = Math.Round(gewindepreisMF + schaftpreisMF, 2);

            //Console.WriteLine("Gewindepreis: " + gesamtpreisMF + " Euro");

            return gesamtpreisMF;
        }

        static public double GewindevolumenMX(double durchmessereingabe, double laengenausgabegewinde)
        {  
            double flankendurchmessre = durchmessereingabe - (0.6495 * (BerechnungSteigung(Tabellen(), durchmessereingabe)));
            double gewindevolumenMX = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
            Console.WriteLine("Flankendurchmesser: " + Math.Round(flankendurchmessre, 2) + " mm");
            return gewindevolumenMX;
        }

        static public double GewindevolumenMF(double durchmessereingabe, double laengenausgabegewinde, double steigung)
        {            
            double flankendurchmessre = durchmessereingabe - (0.6495 * steigung);
            double gewindevolumenMF = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
            Console.WriteLine("Flankendurchmesser: " + Math.Round(flankendurchmessre, 2) + " mm");
            return gewindevolumenMF;
        }

        static public double GewindevolumenWW(double durchmessereingabe, double laengenausgabegewinde, double WitworthSteigung)
        {            
            double flankendurchmessre = durchmessereingabe - (0.64 * WitworthSteigung);
            double gewindevolumenWW = (Math.PI / 4) * (Math.Pow(flankendurchmessre, 2)) * laengenausgabegewinde;
            Console.WriteLine("Flankendurchmesser: "+Math.Round(flankendurchmessre,2)+" mm");
            return gewindevolumenWW;
        }


        static public double SchaftvolumenMX(double durchmessereingabe, double laengenausgabeschaft)
        {
            double schaftvolumenMX = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenMX;
        }

        static public double SchaftvolumenMF(double durchmessereingabe, double laengenausgabeschaft)
        {
            double schaftvolumenMF = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenMF;
        }

        static public double SchaftvolumenWW(double durchmessereingabe, double laengenausgabeschaft)
        {
            double schaftvolumenWW = (Math.PI / 4) * (Math.Pow(durchmessereingabe, 2)) * laengenausgabeschaft;

            return schaftvolumenWW;
        }

        static public double Sechskantvolumen(double schlüsselweiteAusgabe, double kopfhöhe )
        {
            double kopfvolumenSechsKant = Math.Round(kopfhöhe * 3 / 2 * schlüsselweiteAusgabe / 2 / 0.8660254038 * schlüsselweiteAusgabe,2);
            
            return kopfvolumenSechsKant;
        }

        static public double Zylindervolumen(double kopfdurchmesserZylinder, double kopfhöhe)
        {
            double kopfvolumenZylinder = Math.Round(kopfhöhe * Math.PI / 4 * kopfdurchmesserZylinder * kopfdurchmesserZylinder, 2);

            return kopfvolumenZylinder;
        }

        static public double Senkkopfvolumen(double kopfdurchmesserSenkkopf, double kopfhöhe,double durchmessereingabe)
        {
            double kopfvolumenSenkKopf = (kopfdurchmesserSenkkopf+durchmessereingabe)/2*kopfhöhe;

            return kopfvolumenSenkKopf;
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
        static public double SchaftLaenge(double eingabeLaengegewinde)
        {
            Console.Clear();
            double eingabeLaengeSchaft;

            Console.WriteLine("\n Schaftlänge eingeben: \n");
            Console.WriteLine("(Gewindefreier Teil)");

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


            Console.WriteLine("\n Material auswählen: \n");
            Console.WriteLine(" <1> 8.8");
            Console.WriteLine(" <2> 10.9");
            Console.WriteLine(" <3> 12.9");
            Console.WriteLine(" <4> A4-50");


            materialauswahl = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (materialauswahl)
            {
                case 1:
                    materialart = "8.8";
                    materialzugfestigkeit = 800;
                    materialstreckgrenze = 640;

                    break;
                case 2:
                    materialart = "10.9";
                    materialzugfestigkeit = 1000;
                    materialstreckgrenze = 900;
                    break;
                case 3:
                    materialart = "12.9";
                    materialzugfestigkeit = 1200;
                    materialstreckgrenze = 1080;
                    break;
                case 4:
                    materialart = "A4-50";
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
            Console.WriteLine(" <3> Whitworth Gewinde");

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



        static public double AusgabeSchlüsselweite(int schraubenkopfnummer, double durchmesserausgabe, double[,]Tabelle)
        {
            double Schlüsselweite = 0;
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            if (schraubenkopfnummer==1)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Schlüsselweite = Tabelle[jj, 7]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            if (schraubenkopfnummer == 2)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Schlüsselweite = Tabelle[jj, 9]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            if (schraubenkopfnummer == 3)
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Schlüsselweite = Tabelle[jj, 11]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            return Schlüsselweite;
        }

        static public double AusgabeKopfdurchmesser(int schraubenkopfnummer, double durchmesserausgabe, double[,] Tabelle)
        {
            double kopfdurchmesser = 0;
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln
            

            if (schraubenkopfnummer == 2)//Zylinder
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        kopfdurchmesser = Tabelle[jj, 8]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            if (schraubenkopfnummer == 3) //Senkkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        kopfdurchmesser = Tabelle[jj, 12]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            return kopfdurchmesser;
        }

        static public double AusgabeKopfhöhe(int schraubenkopfnummer, double durchmesserausgabe, double[,] Tabelle)
        {
            double Kopfhöhe = 0;
            int jj = 0; // Variable die zum hochzählen verwendet werden soll
            int M = 0; // double der in der Tabelle steht in einen int umwandeln


            if (schraubenkopfnummer == 1) //Sechskantkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kopfhöhe = Tabelle[jj, 6]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            if (schraubenkopfnummer == 2) //Zylinderkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kopfhöhe = Tabelle[jj, 0]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            if (schraubenkopfnummer == 3) //Senkkopf
            {
                for (jj = 0; jj <= 8; jj++) // durchsuchen der Tabelle nach dem richtigen Durchmesser
                {
                    M = Convert.ToInt32(Tabelle[jj, 0]); //umwandeln der Strings in der Tabelle in int
                    if (durchmesserausgabe == M) // Vergleich ob in dem Tabellenfeld der gleiche Wert steht wie in der Eingabe
                    {
                        Kopfhöhe = Tabelle[jj, 10]; // Wert aus der Tabelle wird Durchgangsbohrung übergeben     
                    }
                }
            }

            return Kopfhöhe;
        }



        static public double BerechnungDurchgangsbohrung(double[,] Tabelle, double durchmesserausgabe) 
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
                    MaxBelastung = (Math.Pow((((durchmesserausgabe - 0.6495 * Steigung) + (durchmesserausgabe - 1.2269 * Steigung)) / 2), 2)) * Math.PI * 0.25 * Streckgrenze;

            }



            if(Gewindeart == 2)
            {
                MaxBelastung = (Math.Pow((((durchmesserausgabe - 0.6495 * Steigung) + (durchmesserausgabe - 1.2269 * Steigung)) / 2), 2)) * Math.PI * 0.25 * Streckgrenze;
            }

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

            double[,] Tabelle = new double[9, 13];

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

            //Kopfhöhe Sechskantschraube
            Tabelle[0, 6] = 2;
            Tabelle[1, 6] = 2.8;
            Tabelle[2, 6] = 3.5;
            Tabelle[3, 6] = 4;
            Tabelle[4, 6] = 5.3;
            Tabelle[5, 6] = 6.4;
            Tabelle[6, 6] = 7.5;
            Tabelle[7, 6] = 10;
            Tabelle[8, 6] = 12.5;

            //Schlüsselweite Sechskantschraube
            Tabelle[0, 7] = 5.5;
            Tabelle[1, 7] = 7;
            Tabelle[2, 7] = 8;
            Tabelle[3, 7] = 10;
            Tabelle[4, 7] = 13;
            Tabelle[5, 7] = 16;
            Tabelle[6, 7] = 18;
            Tabelle[7, 7] = 24;
            Tabelle[8, 7] = 30;

            //Kopfdurchmesser Zylinderkopfschraube
            Tabelle[0, 8] = 5.5;
            Tabelle[1, 8] = 7;
            Tabelle[2, 8] = 8.5;
            Tabelle[3, 8] = 10;
            Tabelle[4, 8] = 13;
            Tabelle[5, 8] = 16;
            Tabelle[6, 8] = 18;
            Tabelle[7, 8] = 24;
            Tabelle[8, 8] = 30;

            //Kopfhöhe Zylinderkopfschraube = Nenndurchmesser

            //Schlüsselweite des Innensechskants bei Zylinderkopfschrauben
            Tabelle[0, 9] = 2.5;
            Tabelle[1, 9] = 3;
            Tabelle[2, 9] = 4;
            Tabelle[3, 9] = 5;
            Tabelle[4, 9] = 6;
            Tabelle[5, 9] = 8;
            Tabelle[6, 9] = 10;
            Tabelle[7, 9] = 14;
            Tabelle[8, 9] = 17;

            //Kopfhöhe Senkschrauben
            Tabelle[0, 10] = 1.9;
            Tabelle[1, 10] = 2.5;
            Tabelle[2, 10] = 3.1;
            Tabelle[3, 10] = 3.7;
            Tabelle[4, 10] = 5;
            Tabelle[5, 10] = 6.2;
            Tabelle[6, 10] = 7.4;
            Tabelle[7, 10] = 8.8;
            Tabelle[8, 10] = 10.2;

            //Schlüsselweite des Innensechskants bei Senkschrauben
            Tabelle[0, 11] = 2;
            Tabelle[1, 11] = 2.5;
            Tabelle[2, 11] = 3;
            Tabelle[3, 11] = 4;
            Tabelle[4, 11] = 5;
            Tabelle[5, 11] = 6;
            Tabelle[6, 11] = 8;
            Tabelle[7, 11] = 10;
            Tabelle[8, 11] = 12;

            //Kopfdurchmesser bei Senkschrauben
            Tabelle[0, 12] = 5.5;
            Tabelle[1, 12] = 7.5;
            Tabelle[2, 12] = 9.4;
            Tabelle[3, 12] = 11.3;
            Tabelle[4, 12] = 15.2;
            Tabelle[5, 12] = 19.2;
            Tabelle[6, 12] = 23.1;
            Tabelle[7, 12] = 29;
            Tabelle[8, 12] = 36;

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