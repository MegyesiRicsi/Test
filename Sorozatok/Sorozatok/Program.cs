using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sorozatok
{
    class C {
        public string cim, resz,date2;
        public DateTime date;
        public int hossz;
        public bool latta;
       
        public C(string[] sorok)
        {
           bool le= DateTime.TryParse(sorok[0], out date);
            date2 = sorok[0];
            cim = sorok[1];
           resz = sorok[2];
            hossz = int.Parse(sorok[3]);
            latta = sorok[4] == "1" ? true : false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var lista = new List<C>();
            var sr = new StreamReader("lista.txt");
          var sors = new string[5];
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < 5; i++)
                {
                    sors[i] = sr.ReadLine();
                }
                lista.Add(new C(sors));
            }           
            sr.Close();
            var db = (from rec in lista where rec.date2 != "NI" select rec).Count();
            Console.WriteLine("2. feladat");
            Console.WriteLine($"A listában {db} db vetitési dátummal rendelkező epizód van.");
            var latott = (from sor in lista where sor.latta == true select sor).Count();
            Console.WriteLine("3. feladat");
            Console.WriteLine($"A listában lévő epizódok {(double)latott/lista.Count()*100:0.##}%-át látta");
            var ido = (from sor in lista where sor.latta==true select sor.hossz).Sum();
            var idon =(ido/60)/24 ;
            var idoh = (ido / 60) % 24;
            var idop = ido%60;
            Console.WriteLine($"{idon} {idoh} {idop} {ido}");
            Console.WriteLine("dátum be");
            var datum =DateTime.Parse( Console.ReadLine());
            var newlist = new List<C>();
            int lep = 0;
            do
            {
                if (lista[lep].latta == false)
                {
                    Console.WriteLine($" {lista[lep].date} {lista[lep].resz} {lista[lep].cim}   {lista[lep].latta}");
                }
                lep++;
            } while (lista[lep].date <= datum);


            Console.ReadKey(); 
        }
    }
}
