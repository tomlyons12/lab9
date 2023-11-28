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
    string imie;
    string numerTel;
    int liczbaPolaczen;
    decimal kosztRozmow;

    public Abonent(string imie, string numerTel, int liczbaPolaczen, decimal kosztRozmow)
    {
        this.imie = imie;
        this.numerTel = numerTel;
        this.liczbaPolaczen = 0;
        this.kosztRozmow = 0.0m;
    }

    public string PodajDane()
    {
        return $"Imie: {imie}, numer telefonu: {numerTel}";
    }

    public void Zadzwon(double czas, EnumTaryfa taryfa)
    {
        liczbaPolaczen++;
        decimal rozmowacena = (decimal)czas * (decimal)taryfa;
        kosztRozmow += rozmowacena;
    }

    public (int, decimal) PodsumowanieRozmow()
    {
        return (liczbaPolaczen, kosztRozmow);
    }

}

class Polaczenie
{
    double czasTrwania;
    decimal oplata;
    bool wykonanie;

    Polaczenie(double czasTrwania, decimal oplata, bool wykonanie)
    {
        this.czasTrwania = czasTrwania;
        this.oplata = oplata;
        this.wykonanie = wykonanie;
    }
}





class Program
{

    static void Main()
    {
        IAbonent abonent = new Abonent("Jan Kowalski", "123456", 0, 0.0m);
        abonent.Zadzwon(10.5, EnumTaryfa.taryfa1);
        abonent.Zadzwon(5.0, EnumTaryfa.taryfa2);

        Console.WriteLine(abonent.PodajDane());
        var podsumowanie = abonent.PodsumowanieRozmow();
        Console.WriteLine($"Liczba polaczen {podsumowanie.Item1}, koszt rozmow: {podsumowanie.Item2}");
    }
}







