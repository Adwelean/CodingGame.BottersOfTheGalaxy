using Core.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interactives
{
    public class Bush : EntityBase
    {
        int radius;

        public Bush(double x, double y, int radius) 
            : base(x, y)
        {
            this.radius = radius;
        }
    }
}
