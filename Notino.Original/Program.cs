/*
    Chyba abstrakcia programu
    Nevyuzitie asynchronych metod v programe    
    Vymazat nadbytocne usingy
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Notino.Homework
{
    // Triedu Document by mala byt v separatnom subore, chyba konstruktor
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Natvrdo naprogramovane cesty k cielovym suborom sourceFileName a targetFileName
            // cesty k cielovym suborom by boli poskytnute programu pomocou command line argumentov
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");

            try
            {
                // sourceStream a reader nie je nikdy disposnuty: opravil by som to pomocou Dispose() alebo usingu
                FileStream sourceStream = File.Open(sourceFileName, FileMode.Open);
                var reader = new StreamReader(sourceStream);
                string input = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            // premenu input by trebalo inicializovat este pred try catch(neda sa skompilovat) alebo riadky 66 az nizsie by som premiestnil do try catch
            var xdoc = XDocument.Parse(input);
            var doc = new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };

            var serializedDoc = JsonConvert.SerializeObject(doc);

            // targetStream a reader nie je nikdy disposnuty: opravil by som to pomocou Dispose() alebo usingu
            var targetStream = File.Open(targetFileName, FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(targetStream);
            sw.Write(serializedDoc);
        }
    }
}
