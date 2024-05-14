using System;
using System.IO;
using System.Collections.Generic;

class vilcieni
{

    string meklet1;
    string meklet2;
    int mekletKolonnu1 = 1;
    int mekletKolonnu2 = 2;

    public vilcieni(string meklet1, string meklet2)
    {
        this.meklet1 = meklet1;
        this.meklet2 = meklet2;
    }
    public vilcieni() { }
    public void vilc()
    {
        try
        {
            List<(string, string)> lines = new List<(string, string)>();
            using (StreamReader sr = new StreamReader("Saraksts.csv"))
            {
                string line;
                Console.WriteLine("Atbilstošie reisi: ");
                while ((line = sr.ReadLine()) != null)
                {
                    string[] kolonnas = line.Split(',');
                    if ((kolonnas.Length > mekletKolonnu1 && kolonnas[mekletKolonnu1].Trim() == meklet1) && (kolonnas.Length > mekletKolonnu2 && kolonnas[mekletKolonnu2].Trim() == meklet2))
                    {

                        lines.Add((line.Substring(0, 4), line.Substring(4)));
                    }
                }
            }

            if (lines.Count > 0)
            {

                lines.Sort((x, y) => string.Compare(x.Item2, y.Item2));


                foreach (var sortedLine in lines)
                {
                    Console.WriteLine(sortedLine.Item1 + sortedLine.Item2);
                }
            }
            else
            {
                Console.WriteLine("Šāda stacija nepastāv.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void WriteToCSV(List<User> users, string filepath)
    {
        using (StreamWriter sw = new StreamWriter(filepath, true))
        {

            foreach (User user in users)
            {
                sw.WriteLine($"{user.Username},{user.Email},{user.Password}");
            }
        }
    }
    public bool lietot(string meklet)
    {
        try
        {
            using (StreamReader sr = new StreamReader("Lietotāji.csv"))
            {
                string line;
                bool success;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] kolonnas = line.Split(',');
                    if ((kolonnas.Length > mekletKolonnu1 && kolonnas[mekletKolonnu1].Trim() == meklet))
                    {
                        success = true;
                        return success;
                    }
                }
                return false;
            }

        }
        catch (Exception e)
        {

            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
    public void DeleteRow(string meklet)
    {
        try
        {
            List<string> updatedLines = new List<string>();

            bool deleted = false;
            using (StreamReader sr = new StreamReader("Lietotāji.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] kolonnas = line.Split(',');
                    if (kolonnas.Length > mekletKolonnu1 && kolonnas[mekletKolonnu1].Trim() == meklet)
                    {
                        deleted = true;
                    }
                    else
                    {
                        updatedLines.Add(line);
                    }
                }
            }


            if (deleted)
            {
                using (StreamWriter sw = new StreamWriter("Lietotāji.csv"))
                {
                    foreach (string updatedLine in updatedLines)
                    {
                        sw.WriteLine(updatedLine);
                    }
                }
                Console.WriteLine("Lietotājs izdzēsts veiksmīgi.");
            }
            else
            {
                Console.WriteLine("Lietotājs ar norādīto e-pastu nav atrasts.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Kļūda: " + e.Message);
        }
    }

    public void Rediget(string filepath, string email)
    {
        try
        {
            string[] lines = File.ReadAllLines(filepath);


            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length >= 2 && parts[1] == email)
                {
                    Console.WriteLine("Ievadi jaunu lietotāju:");
                    string newUsername = Console.ReadLine();


                    Console.WriteLine("Ievadi jaunu epastu:");
                    string newEmail;
                    while (true)
                    {
                        newEmail = Console.ReadLine();
                        if (newEmail.Contains("@"))
                        {
                            break;
                        }
                        Console.WriteLine("Nederīgs epasta formāts.");
                        Console.WriteLine("Ievadi jaunu epastu:");
                    }

                    Console.WriteLine("Ievadi jaunu paroli:");
                    string newPassword;
                    while (true)
                    {
                        newPassword = Console.ReadLine();
                        if (newPassword.Length >= 6)
                        {
                            break;
                        }
                        Console.WriteLine("Parolei jābūt vismaz 6 burtiem vai skaitļiem.");
                        Console.WriteLine("Ievadi jaunu paroli");
                    }

                    lines[i] = $"{newUsername},{newEmail},{newPassword}";

                    File.WriteAllLines(filepath, lines);

                    Console.WriteLine("Lietotāja informācija tika pārmainīta");
                    return;
                }
            }

            Console.WriteLine("Epasts nav atrasts");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}