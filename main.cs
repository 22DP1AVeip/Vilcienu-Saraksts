using System;
using System.Collections.Generic;
using static vilcieni;


public class Program
{
    static List<User> users = new List<User>();
    static bool loggedIn = false;

    public static void Main(string[] args)
    {
        Console.WriteLine(@"            _ \  _)                                | _)            |             
              |   |  |   _ \  __ \    _` |       __|  |  |   _ \   _` |   _ \   __| 
              ___/   |   __/  |   |  (   |     \__ \  |  |   __/  (   |   __/ \__ \ 
             _|     _| \___| _|  _| \__,_|     ____/ _| _| \___| \__,_| \___| ____/ 
");

        static void SignUp()
        {
            Console.WriteLine("Ievadi lietotājvārdu:");
            string username = Console.ReadLine();

            string email;
            while (true)
            {
                Console.WriteLine("Ievadi epastu:");
                email = Console.ReadLine();

                if (email.Contains("@"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nederīgs epasta formāts.");
                }
            }


            while (true)
            {
                Console.WriteLine("Ievadi paroli:");
                string password = Console.ReadLine();

                if (password.Length < 6)
                {
                    Console.WriteLine("Parolei jābūt vismaz 6 burtiem vai skaitļiem");
                    continue;
                }

                users.Add(new User(username, email, password));
                WriteToCSV(users, "Lietotāji.csv");
                Console.WriteLine("Konts izveidots veiksmīgi.");
                break;
            }
        }


        static void LogIn()
        {
            bool b = true;
            while (b == true)
            {
                Console.WriteLine("Ievadi epastu:");
                string email = Console.ReadLine();

                Console.WriteLine("Ievadi paroli:");
                string password = Console.ReadLine();

                vilcieni vilc = new vilcieni();
                if (vilc.lietot(email) != false)
                {
                    Console.WriteLine($"Sveiki!");
                    b = false;
                }
                else
                {
                    Console.WriteLine("Nepareizs epasts vai parole");
                    continue;
                }
            }
        }
        while (!loggedIn)
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Signup");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    LogIn();
                    loggedIn = true;
                    break;
                case 2:
                    SignUp();
                    continue;
                default:
                    Console.WriteLine("Nepareiza izvēle. Lūdzu ievadi 1, 2 vai 3");
                    continue;
            }
        }
        bool c = true;
        while (c == true)
        {
            Console.WriteLine("1. Pirkt biļeti");
            Console.WriteLine("2. dzēst lietotāju");
            Console.WriteLine("3. Rediģēt informāciju");
            Console.WriteLine("4. Exit");

            int choice1 = int.Parse(Console.ReadLine());

            switch (choice1)
            {
                case 1:
                    c = false;
                    break;
                case 2:
                    vilcieni vilc = new vilcieni();
                    Console.WriteLine("Ievadiet savu e-pastu, ja tiešām vēlaties izdzēst, ievadiet nē, ja nevēlaties.");
                    string d = Console.ReadLine();
                    if (d == "nē")
                    {
                        continue;
                    }
                    vilc.DeleteRow(d);
                    continue;
                case 3:
                    vilcieni vilcien = new vilcieni();
                    Console.WriteLine("Ievadiet savu e-pastu, ja vēlaties rediģētu informāciju, ievadiet nē, ja nevēlaties:");
                    string emailToEdit = Console.ReadLine();
                    if (emailToEdit == "nē")
                    {
                        continue;
                    }
                    vilcien.Rediget("Lietotāji.csv", emailToEdit);
                    continue;
                case 4:
                    Console.WriteLine("Iziet no programmas");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nepareiza izvēle. Lūdzu ievadi 1, 2, 3 vai 4");
                    continue;
            }
        }

        bool a = true;
        int tryagain = 0;
        int donttryagain = 0;
        string sakum = "";
        while (a == true)
        {
            if (tryagain > 0)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Nepareizi ievadīta stacija, mēģiniet atkal");
            }
            if (donttryagain == 0)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Ievadiet sākumpunktu:\nRīga\nSkulte\nJelgava\nSloka\nAizkraukle\nLiepāja\nCēsis\nTukums 2\niziet");
                sakum = Console.ReadLine();
                if (sakum.Equals("iziet"))
                {
                    Environment.Exit(0);
                }
                if (sakum.Equals("Skulte") || sakum.Equals("Rīga") || sakum.Equals("Jelgava") || sakum.Equals("Sloka") || sakum.Equals("Aizkraukle") || sakum.Equals("Liepāja") || sakum.Equals("Cēsis") || sakum.Equals("Tukums 2") || sakum.Equals("iziet"))
                {
                    donttryagain++;
                }
                else
                {
                    tryagain++;
                    continue;
                }
            }

            Console.WriteLine("--------------------------------");
            Console.WriteLine("Ievadiet galapunktu:\nRīga\nSkulte\nJelgava\nSloka\nAizkraukle\nLiepāja\nCēsis\nTukums 2\niziet");
            string gala = Console.ReadLine();
            if (gala.Equals("iziet"))
            {
                Environment.Exit(0);
            }
            if (gala.Equals("Skulte") || gala.Equals("Rīga") || gala.Equals("Jelgava") || gala.Equals("Sloka") || sakum.Equals("Aizkraukle") || gala.Equals("Liepāja") || gala.Equals("Cēsis") || gala.Equals("Tukums 2") || gala.Equals("iziet"))
            {
                donttryagain++;
            }
            else
            {
                tryagain++;
                continue;
            }
            vilcieni vilciens1 = new vilcieni(sakum, gala);
            vilciens1.vilc();
            Vilciens bilete = new Vilciens();
            bilete.PirktBileti("Saraksts.csv");
            a = false;

        }
    }


}
class User
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}