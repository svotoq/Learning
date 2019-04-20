using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Pult invoker = new Pult();
            TV reciver = new TV();
            Command concreteCommand = new Command(reciver);
            invoker.SetCommand(concreteCommand);
            invoker.PressButton();
            invoker.PressUndo();
        }
    }
    interface ICommand
    {
        void Execute();
        void Undo();
    }
    class Command : ICommand
    {
        TV tv;
        public Command(TV _tv)
        {
            tv = _tv;
        }
        public void Execute()
        {
            tv.TurnOn();
        }

        public void Undo()
        {
            tv.TurnOff();
        }
    }
    class TV
    {
        public void TurnOn()
        { Console.WriteLine("TurnOn"); }
        public void TurnOff()
        { Console.WriteLine("TurnOff"); }
    }
    class Pult
    {
        ICommand command;
        public void SetCommand(ICommand _command)
        {
            command = _command;
        }
        public void PressButton()
        {
            command.Execute();
        }
        public void PressUndo()
        {
            command.Undo();
        }
    }
}
