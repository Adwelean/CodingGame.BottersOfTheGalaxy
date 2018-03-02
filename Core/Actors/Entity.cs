using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Actors
{
    public class Entity : EntityBase
    {
        int id;

        public Entity(double x, double y, int id) 
            : base(x, y)
        {
            this.id = id;
        }
    }
}
