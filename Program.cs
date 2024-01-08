using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Kozterulet
{
    public string? Iranyitoszam { get; set; }
    public string? Nev { get; set; }
    public string? KozteruletTipus { get; set; }
    public string? Kerulet { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var kozteruletek = File.ReadAllLines("bp.csv")
            .Skip(1)
            .Select(sor => sor.Split(';'))
            .Where(reszek => reszek.Length >= 4)
            .Select(reszek => new Kozterulet { Iranyitoszam = reszek[0], Nev = reszek[1], KozteruletTipus = reszek[2], Kerulet = reszek[3] })
            .ToList();

        // Feladat 2
        Console.WriteLine($"Nyilvános területek száma: {kozteruletek.Count}");

        // Feladat 3
        var xiiiKerulet = kozteruletek.Count(k => k.Kerulet == "XIII");
        Console.WriteLine($"Nyilvános területek száma a XIII. kerületben: {xiiiKerulet}");

        // Feladat 4
        Console.Write("Adjon meg egy irányítószámot: ");
        var iranyitoszam = Console.ReadLine();
        var iranyitoszamLetezik = kozteruletek.Any(k => k.Iranyitoszam == iranyitoszam);
        Console.WriteLine(iranyitoszamLetezik ? "Az irányítószám létezik." : "Az irányítószám nem létezik.");

        // Task 5
        Console.Write("Adjon meg egy szöveget: ");
        var szoveg = Console.ReadLine();
        if (!string.IsNullOrEmpty(szoveg))
        {
            var egyezoTeruletek = kozteruletek.Where(k => k.Nev?.StartsWith(szoveg) == true);
            foreach (var terulet in egyezoTeruletek)
            {
                Console.WriteLine($"{terulet.Nev} - {terulet.Kerulet}. kerület");
            }
        }
        else
        {
            Console.WriteLine("Nem irt be szöveget");
        }

        // Feladat 6
        var districtCounts = kozteruletek.GroupBy(k => k.Kerulet)
            .Select(g => new { Kerulet = g.Key, Szamlalo = g.Count() })
            .OrderBy(g => g.Kerulet);
        foreach (var district in districtCounts)
        {
            Console.WriteLine($"{district.Kerulet}. kerületben - {district.Szamlalo} közterület van!");
        }
    }
}
