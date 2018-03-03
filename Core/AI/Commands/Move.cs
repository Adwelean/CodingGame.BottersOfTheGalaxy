using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AI.Commands
{
    public class Move : CommandBase
    {
        double x;
        double y;

        public Move(double x, double y)
            : base("MOVE")
        {
            this.x = x;
            this.y = y;
        }

        public override string Build() => $"MOVE {x} {y}";
    }
}
