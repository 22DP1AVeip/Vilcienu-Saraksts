using System;
using System.Collections.Generic;
using System.IO;

public class Bilete
{
    public int VilcienaNr { get; set; }
    public string SakPietura { get; set; }
    public string Galapunkts { get; set; }
    public string IzbrLaiks { get; set; }
    public string IebrLaiks { get; set; }
    public double Cena { get; set; }

    public Bilete(int vilcienaNr, string sakPietura, string galapunkts, string izbrLaiks, string iebrLaiks, double cena)
    {
        VilcienaNr = vilcienaNr;
        SakPietura = sakPietura;
        Galapunkts = galapunkts;
        IzbrLaiks = izbrLaiks;
        IebrLaiks = iebrLaiks;
        Cena = cena;
    }

}

public class Vilciens
{
    public static List<Bilete> saraksti;

    public void PirktBileti(string failaNosaukums)
    {
        saraksti = new List<Bilete>();
        try
        {
            using (StreamReader sr = new StreamReader(failaNosaukums))
            {
                string rinda;
                while ((rinda = sr.ReadLine()) != null)
                {
                    string[] dati = rinda.Split(',');

                    int vilcienaNr;
                    if (!int.TryParse(dati[0], out vilcienaNr))
                    {
                        continue;
                    }

                    string sakPietura = dati[1].Trim();
                    string galapunkts = dati[2].Trim();
                    string izbrLaiks = dati[3].Trim();
                    string iebrLaiks = dati[4].Trim();
                    double cena = double.Parse(dati[5]);
                    saraksti.Add(new Bilete(vilcienaNr, sakPietura, galapunkts, izbrLaiks, iebrLaiks, cena));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Kļūda lasot vilcienu sarakstus: " + ex.Message);
            Console.WriteLine("Stack trace: " + ex.StackTrace);
        }



        Console.WriteLine("\nIevadiet vilcienaNr, lai iegādātos biļetes:");
        int vilcienaNr1 = int.Parse(Console.ReadLine());
        var izveletaisSaraksts = saraksti.Find(saraksts => saraksts.VilcienaNr == vilcienaNr1);
        if (izveletaisSaraksts != null)
        {
            Console.WriteLine($"\nJūs izvēlējāties šo vilcienu sarakstu:");
            Console.WriteLine($"Vilciena numurs: {izveletaisSaraksts.VilcienaNr}");
            Console.WriteLine($"Sākuma pietura: {izveletaisSaraksts.SakPietura}");
            Console.WriteLine($"Galapunkts: {izveletaisSaraksts.Galapunkts}");
            Console.WriteLine($"Izbraukšanas laiks: {izveletaisSaraksts.IzbrLaiks}");
            Console.WriteLine($"Iebraukšanas laiks: {izveletaisSaraksts.IebrLaiks}");
            Console.WriteLine($"Cena: {izveletaisSaraksts.Cena}");


            Console.WriteLine("\nIevadiet biļešu skaitu, kuru vēlaties iegādāties:");
            int biletesSkaits = int.Parse(Console.ReadLine());


            double kopejaCena = biletesSkaits * izveletaisSaraksts.Cena;
            Console.WriteLine($"\nKopējā cena par {biletesSkaits} biļetēm: {kopejaCena} eiro");
            Console.WriteLine("Biļetes veiksmīgi iegādātas!");
            for (int i = 0;i < biletesSkaits;i++){
                QR qr = new QR();
                qr.qrcode();
            }
        }
        else
        {
            Console.WriteLine("Nederīgs vilcienaNr. Lūdzu, izvēlieties derīgu sarakstu.");
        }
    }
}
