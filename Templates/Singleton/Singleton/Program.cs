using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton fs = Singleton.Create("vasya");
            Singleton fs2 = Singleton.Create("vasya2");
            Console.WriteLine(fs.name + ' ' + fs2.name);
        }
    }
    public class Singleton
    {
        private static Singleton instance;
        public string name { get; set; }
        private Singleton(string Name)
        {
            name = Name;
        }
        public static Singleton Create(string Name)
        {
            if (instance == null)
                instance = new Singleton(Name);
            return instance;
        }
    }
}
