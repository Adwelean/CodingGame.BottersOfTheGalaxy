using Core.Actors;
using Core.Interactives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Factories
{
    public static class InteractiveObjectFactory
    {
        public enum InteractiveObjectType
        {
            NONE,
            BUSH,
            SPAWN,
        }

        public static EntityBase ParseInteractiveObject(string[] inputs)
        {
            EntityBase entity = null;
            InteractiveObjectType interactiveType = InteractiveObjectType.NONE;

            string entityType = inputs[0]; // BUSH, from wood1 it can also be SPAWN
            int x = int.Parse(inputs[1]);
            int y = int.Parse(inputs[2]);
            int radius = int.Parse(inputs[3]);

            if(Enum.TryParse(entityType, out interactiveType))
            {
                switch(interactiveType)
                {
                    case InteractiveObjectType.BUSH:
                        entity = new Bush(x, y, radius);
                        break;

                    case InteractiveObjectType.SPAWN:
                        entity = new Spawn(x, y);
                        break;
                }
            }

            return entity;
        }
    }
}
