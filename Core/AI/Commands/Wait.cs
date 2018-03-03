using System;
using System.Collections.Generic;
using System.Text;

namespace Core.AI.Commands
{
    public class Wait : CommandBase
    {
        public Wait()
            : base("WAIT")
        {
        }

        public override string Build() => this.Name;
    }
}
