using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            Code code = new Code();
            code.DoSomething(new LiptonCreator(100, 25));
            code.DoSomething(new NooriCreator(800,400));
        }
    }
    public abstract class Tea
    {
        public string Name { get; protected set; }
        public int Weight { get; protected set; }
        public int NumberOfBags { get; protected set; }
        public Tea(string _name, int _weight, int _numberOfBags)
        {
            Name = _name;
            Weight = _weight;
            NumberOfBags = _numberOfBags;
        }
    }
    public class Lipton : Tea
    {
        public Lipton(int _weight, int _numberOfBags) : base("Lipton", _weight, _numberOfBags)
        {
        }
    }
    public class Noori : Tea
    {
        public Noori(int _weight, int _numberOfBags) : base("Noori", _weight, _numberOfBags)
        {
        }
    }
    public abstract class Creator
    {
        public abstract Tea FactoryMethod();
    }
    public class LiptonCreator : Creator
    {
        private int Weight { get; }
        private int NumberOfBags { get; }
        public LiptonCreator(int _weight, int _numberOfBags)
        {
            Weight = _weight;
            NumberOfBags = _numberOfBags;
        }
        public override Tea FactoryMethod()
        {
            return new Lipton(Weight, NumberOfBags);
        }
    }
    public class NooriCreator : Creator
    {
        private int Weight { get; }
        private int NumberOfBags { get; }
        public NooriCreator(int _weight, int _numberOfBags)
        {
            Weight = _weight;
            NumberOfBags = _numberOfBags;
        }
        public override Tea FactoryMethod()
        {
            return new Noori(Weight, NumberOfBags);
        }
    }
    public class Code
    {
        public void DoSomething(Creator creator)
        {
            Tea tea = creator.FactoryMethod();
            Console.WriteLine(tea.Name + " added in database");
        }
    }
}
