using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Prototype prototype = new ConretePrototype1(1);
            Prototype clone = prototype.Clone();
            Console.WriteLine(prototype.Id);
            Console.WriteLine(clone.Id);
        }
    }
    abstract class Prototype
    {
        public int Id { get; set; }
        public Prototype(int _id)
        {
            Id = _id;
        }
        public abstract Prototype Clone();
    }
    class ConretePrototype1 : Prototype
    {
        public ConretePrototype1(int _id) : base(_id)
        {
        }
        public override Prototype Clone()
        {
            return new ConretePrototype1(Id);
        }
    }
    class ConretePrototype2 : Prototype
    {
        public ConretePrototype2(int _id) : base(_id)
        {
        }
        public override Prototype Clone()
        {
            return new ConretePrototype2(Id);
        }
    }
}
