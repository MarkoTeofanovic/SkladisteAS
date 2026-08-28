using System;
using System.IO;
using Skladiste.Obavestenja;

namespace Skladiste.Infrastructure;

// Adapter - IObavestenjeSlanje port, upisuje obavestenje u tekstualnu datoteku
public class KonzolnoObavestenjeSlanje : IObavestenjeSlanje
{
    private readonly string _putanja;

    public KonzolnoObavestenjeSlanje(string putanja = "obavestenja.log")
    {
        _putanja = putanja;
    }

    public void Posalji(string poruka)
    {
        File.AppendAllText(_putanja, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {poruka}{Environment.NewLine}");
    }
}
