using System;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Context context = new Context(new ConcreteStrategy1());
            context.ExecuteAlghoritm();
        }
    }
    public interface IStrategy
    {
        void Alghoritm();
    }
    public class ConcreteStrategy1 : IStrategy
    {
        public void Alghoritm()
        {
            Console.WriteLine("First strategy");
        }
    }
    public class ConcreteStrategy2 : IStrategy
    {
        public void Alghoritm()
        {
            Console.WriteLine("Second strategy");
        }
    }
    public class Context
    {
        public IStrategy ContextStrategy { get; set; }
        public Context(IStrategy _strategy)
        {
            ContextStrategy = _strategy;
        }
        public void ExecuteAlghoritm()
        {
            ContextStrategy.Alghoritm();
        }
    }
}
