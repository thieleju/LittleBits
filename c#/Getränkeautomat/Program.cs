
using System.Collections.Generic;

using System;

class GetränkeListe
{

    private String name;

    private List<Getränk> liste = new List<Getränk>();

    public GetränkeListe(String nameListe)
    {
        this.name = nameListe;
    }

    public void addGetränk(Getränk g)
    {
        this.liste.Add(g);
    }

    public void printListe()
    {
        Console.WriteLine("--------------------------------------------------------------------");
        Console.WriteLine("-> " + this.name);
        Console.WriteLine("--------------------------------------------------------------------");

        for (int i = 0; i < liste.Count; i++)
        {
            Console.WriteLine(i + 1 + ". Getränk: " + liste[i].getName() + " kostet " + liste[i].getPreis() + "€ und ist noch " + liste[i].getAnzahl() + " mal vorhanden");
        }

        Console.WriteLine("--------------------------------------------------------------------");
    }

    public bool checkIfValidGetränk(String input)
    {
        int auswahl;
        // check if valid integer
        if (!int.TryParse(input, out auswahl))
        {
            return false;
        }
        // check if valid getränk id
        if (auswahl <= this.liste.Count && auswahl >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Getränk getGetränkByIndex(int index)
    {
        return liste[index];
    }

    public void kaufeGetränk(int index)
    {
        // Getränkekauf Ablauf

        Getränk g = getGetränkByIndex(index);
        Console.WriteLine("Sie haben '" + g.getName() + "' für " + g.getPreis() + "€ ausgewählt");

        double nochzuzahlen = g.getPreis();
        bool exit = false;
        do
        {
            String input = getGeldInput();

            if (checkIfValidDouble(input))
            {

                double eingabeBetrag = Double.Parse(input);

                if (eingabeBetrag > nochzuzahlen)
                {
                    double rückgeld = eingabeBetrag - nochzuzahlen;
                    Console.WriteLine("Sie haben zu viel eingezahlt, Ihr Wecheslgeld beträgt " + rückgeld + "€");
                    liste[index].verringereAnzahl(1);
                    exit = true;
                }
                else if (eingabeBetrag == nochzuzahlen)
                {
                    Console.WriteLine("Sie haben passend bezahlt!");
                    liste[index].verringereAnzahl(1);
                    exit = true;
                }
                else if (eingabeBetrag < nochzuzahlen)
                {
                    nochzuzahlen = nochzuzahlen - eingabeBetrag;
                    Console.WriteLine("Sie müssen noch " + nochzuzahlen + "€ bezahlen");
                }

            }
            else
            {
                Console.WriteLine("Ungültige Zahl!");
            }

        } while (!exit);

    }

    private String getGeldInput()
    {
        Console.WriteLine("Geben Sie bitte den Geldbetrag ein: (€)");
        return Console.ReadLine();
    }

    private bool checkIfValidDouble(String input)
    {
        return Double.TryParse(input, out _);
    }

}

class Getränk
{
    private String name;
    private double preis;
    private int anzahl;

    public Getränk(String nameGetränk, double preisGetränk, int anzahlGetränk)
    {
        this.name = nameGetränk;
        this.preis = preisGetränk;
        this.anzahl = anzahlGetränk;
    }

    public void verringereAnzahl(int i)
    {
        if (this.anzahl < i)
        {
            Console.WriteLine("Fehler, das Getränk " + this.name + " ist nur noch " + this.anzahl + " mal vorhanden!");
        }
        else
        {
            this.anzahl -= i;
        }
    }

    public int getAnzahl()
    {
        return this.anzahl;
    }

    public double getPreis()
    {
        return this.preis;
    }

    public String getName()
    {
        return this.name;
    }
}


public class MainProgramm
{
    static void Main()
    {
        // PROGRAMM STARTS HERE
        GetränkeListe liste = initializeGetränkeListe();

        liste.printListe();

        bool exit = false;
        do
        {
            String input = getInput();

            if (liste.checkIfValidGetränk(input))
            {
                int index = Int32.Parse(input) - 1;
                liste.kaufeGetränk(index);
            }
            else if (input == "q")
            {
                Console.WriteLine("Das Programm wird beendet ...");
                exit = true;
            }
            else if (input == "m")
            {
                liste.printListe();
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe!\n'm' um das menü anzuzeigen und \n'q' um das Programm zu beenden");
            }

        } while (!exit);
    }

    private static GetränkeListe initializeGetränkeListe()
    {
        GetränkeListe gl = new GetränkeListe("Getränke Liste 1");

        gl.addGetränk(new Getränk("Pepsi", 2, 10));
        gl.addGetränk(new Getränk("Cola", 2, 10));
        gl.addGetränk(new Getränk("Fanta", 1.5, 10));
        gl.addGetränk(new Getränk("Jägermeister", 3.14, 10));
        gl.addGetränk(new Getränk("Redbull", 2.99, 10));

        return gl;
    }

    private static String getInput()
    {
        Console.WriteLine("Geben Sie die Zahl des Getränks ein, welches sie kaufen möchten");
        return Console.ReadLine();
    }

}


