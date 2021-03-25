using System;

/*
 Hello World Programm
 Version 1.1
 letzte Aenderung 25.03.2021
 Team_E_HSP
*/

namespace Hello_World_Team_E_HSP
{
    class Program
    {
        static void Main(string[] args) // Hier beginnt das Hauptprogramm 
        {
            // Ausgabe Benutzer 1
            Console.WriteLine("Hello World von Jan");

            // Ausgabe Benutzer 2
            Console.WriteLine("Hello World von Marvin");

            // Ausgabe Benutzer 3
            Console.WriteLine("Hello World von Jan");

            // Ausgabe Benutzer 4
            Console.WriteLine("Hello World von Richard");

            // Zeile die Konflikt triggern soll
            Console.WriteLine("Hello");

            // warte bis der User eine Taste drueckt
            Console.ReadKey();

        } // Hier endet das Hauptprogramm
    }
}