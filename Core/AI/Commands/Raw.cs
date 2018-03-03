using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AI.Commands
{
    public class Raw : CommandBase
    {
        public Raw(string command)
            : base(command)
        {
        }

        public override string Build() => Name;
    }
}
