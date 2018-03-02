using Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Actors
{
    public abstract class EntityBase : Point
    {
        public EntityBase(double x, double y)
            : base(x, y)
        {
        }
    }
}
