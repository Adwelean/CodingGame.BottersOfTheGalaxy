using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AI.Commands
{
    public abstract class CommandBase
    {
        string name;

        public CommandBase(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }

        public abstract string Build();
    }
}
