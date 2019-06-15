using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MarsRoverKata.Domain
{
    public class Command
    {
        protected Command() { }
        
        public char Code { get; protected set; }

        public Command(char code)
        {
            this.Code = code;
        }
    }

    public class CommandsList : ReadOnlyCollection<Command>
    {
        public static readonly Command MoveForward = new Command('f');
        public static readonly Command MoveBackward = new Command('b');
        public static readonly Command TurnLeft = new Command('l');
        public static readonly Command TurnRight = new Command('r');

        public static readonly ReadOnlyCollection<Command> AllCommands = 
            new ReadOnlyCollection<Command>(new List<Command> { MoveForward, MoveBackward, TurnLeft, TurnRight });

        public CommandsList() : base(AllCommands) { }

        public static Command GetByCode(char code)
        {
            return AllCommands.SingleOrDefault(x => x.Code == code);
        }
    }
}