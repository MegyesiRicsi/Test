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
        public int hossz,ev,honap,nap;
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
        static string hetnapja(int ev, int ho, int nap)
        {
            var napok = new string[] { "v","h","k","sze","cs","p","szo"};
            var honapok = new int[] { 0, 3, 2, 5, 0, 3, 5, 1, 4, 6, 2, 4 };
            if (ho<3)
            {
                ev -= 1;
                
            }
            int index = (ev + ev / 4 - ev / 100 + ev / 400 + honapok[ho - 1] + nap) % 7;
            return napok[index];
        }
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
            Console.Write("dátum be");
            var datum =DateTime.Parse( Console.ReadLine());
            var newlist = new List<C>();
            
            foreach (var item in lista)
            {
                if (item.date<=datum)
                {
                    if (item.latta == false && item.date2!="NI")
                    {
                        Console.WriteLine($"{item.resz}\t{item.cim}");
                    }
                }
                
            }
            Console.Write("Adja meg a hét egy napját (például cs)! Nap= ");
            string napocska= Console.ReadLine();
            var last = (from sor in lista where  hetnapja(sor.date.Year,sor.date.Month,sor.date.Day)==napocska group sor by sor.cim);
            foreach (var item in last)
            {
                Console.WriteLine(item.Key);
            }

            Console.ReadKey(); 
        }
    }
}
