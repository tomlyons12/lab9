using System;
using System.Collections;

public enum EnumTaryfa { taryfa1 = 1, taryfa2 = 2, taryfa3 = 4 };

public interface IAbonent
{
    string PodajDane();
    public void Zadzwon(double czas, EnumTaryfa taryfa);
    public (int, decimal) PodsumowanieRozmow();

}

public class Abonent : IAbonent
{
    public string imie;
    public string nazwisko;
    public string numerTelefonu;
    private List<Polaczenie> polaczenia;

    public Abonent(string imie, string nazwisko, string numerTelefonu)
    {
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.numerTelefonu = numerTelefonu;
        this.polaczenia = new List<Polaczenie>();
    }

    public string PodajDane()
    {
        return $"{imie} {nazwisko} {{{numerTelefonu}}}";
    }

    public void Zadzwon(double czas, EnumTaryfa taryfa)
    {
        double losowaLiczba = new Random().NextDouble();

        if (losowaLiczba < 0.3)
        {
            polaczenia.Add(new Polaczenie(0, 0, false));
        }
        else
        {
            decimal oplata = (decimal)czas * (decimal)taryfa;
            polaczenia.Add(new Polaczenie(czas, oplata, true));
        }
    }

    public (int, decimal) PodsumowanieRozmow()
    {
        int udanePolaczenia = 0;
        decimal sumaOplat = 0;

        foreach (var polaczenie in polaczenia)
        {
            if (polaczenie.Wykonane)
            {
                udanePolaczenia++;
                sumaOplat += polaczenie.Oplata;
            }
        }

        return (udanePolaczenia, sumaOplat);
    }

    public override string ToString()
    {
        var podsumowanie = PodsumowanieRozmow();
        return $"{PodajDane()}, [liczba rozmów: {podsumowanie.Item1}, opłata: {podsumowanie.Item2:F2} zł]";
    }
}

class Polaczenie
{
    public double CzasTrwania { get; set; }
    public decimal Oplata { get; set; }
    public bool Wykonane { get; set; }

    public Polaczenie(double czasTrwania, decimal oplata, bool wykonane)
    {
        CzasTrwania = czasTrwania;
        Oplata = oplata;
        Wykonane = wykonane;
    }
}

class Operatorsieci
{
    string nazwa;
    Dictionary<string, Abonent> abonenci;

    public Operatorsieci(string nazwa)
    {
        this.nazwa = nazwa;
        this.abonenci = new Dictionary<string, Abonent>();
    }

    public void DodajAbonenta(Abonent abonent)
    {
        if (!abonenci.ContainsKey(abonent.numerTelefonu))
        {
            abonenci.Add(abonent.numerTelefonu, abonent);
        }
        else
        {
            Console.WriteLine($"Abonent o numerze {abonent.numerTelefonu} już istnieje w operatorze {nazwa}.");
        }
    }

    public Abonent WyszukajAbonenta(string numerTelefonu)
    {
        if (abonenci.ContainsKey(numerTelefonu))
        {
            return abonenci[numerTelefonu];
        }
        else
        {
            Console.WriteLine($"Nie znaleziono abonenta o numerze {numerTelefonu} w operatorze {nazwa}.");
            return null;
        }
    }

    public override string ToString()
    {
        decimal sumarycznyZysk = 0;

        Console.WriteLine($"Operator: {nazwa} [sumaryczny zysk: {sumarycznyZysk:F2}zł]");

        foreach (var abonent in abonenci.Values)
        {
            var podsumowanie = abonent.PodsumowanieRozmow();
            Console.WriteLine($"{abonent.PodajDane()}, [liczba rozmów: {podsumowanie.Item1}, opłata: {podsumowanie.Item2:F2}zł]");
            sumarycznyZysk += podsumowanie.Item2;
        }

        return $"Operator: {nazwa} [sumaryczny zysk: {sumarycznyZysk:F2}zł]";
    }

}

class Program
{

    static void Main()
    {
        Operatorsieci operatorSieci = new Operatorsieci("IDEA");

        Abonent abonent1 = new Abonent("Jan", "Kowalski", "777-034-232");
        abonent1.Zadzwon(10.5, EnumTaryfa.taryfa1);
        abonent1.Zadzwon(5.0, EnumTaryfa.taryfa2);
        abonent1.Zadzwon(7.0, EnumTaryfa.taryfa3);

        Abonent abonent2 = new Abonent("Edyta", "Nowak", "666-634-009");
        abonent2.Zadzwon(7.0, EnumTaryfa.taryfa1);
        abonent2.Zadzwon(8.5, EnumTaryfa.taryfa2);
        abonent2.Zadzwon(5.0, EnumTaryfa.taryfa3);

        Abonent abonent3 = new Abonent("Marian", "Waligóra", "744-934-229");

        operatorSieci.DodajAbonenta(abonent1);
        operatorSieci.DodajAbonenta(abonent2);
        operatorSieci.DodajAbonenta(abonent3);

        Console.WriteLine(operatorSieci.ToString());

        // Przykład wyszukiwania abonenta
        string numerTelefonuDoWyszukania = "777-034-232";
        Abonent znalezionyAbonent = operatorSieci.WyszukajAbonenta(numerTelefonuDoWyszukania);

        if (znalezionyAbonent != null)
        {
            Console.WriteLine($"Znaleziono abonenta o numerze telefonu {numerTelefonuDoWyszukania}: {znalezionyAbonent.PodajDane()}");
        }
    }
}







