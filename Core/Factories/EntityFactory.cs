using Core.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Factories
{
    public static class EntityFactory
    {
        public static Entity ParseEntity(string[] inputs)
        {
            Entity entity = null;

            int unitId = int.Parse(inputs[0]);
            int x = int.Parse(inputs[3]);
            int y = int.Parse(inputs[4]);

            entity = new Entity(x, y, unitId);
            
            // TODO: add this to entity
            int attackRange = int.Parse(inputs[5]);
            int health = int.Parse(inputs[6]);
            int maxHealth = int.Parse(inputs[7]);
            int shield = int.Parse(inputs[8]); // useful in bronze
            int attackDamage = int.Parse(inputs[9]);
            int movementSpeed = int.Parse(inputs[10]);
            int stunDuration = int.Parse(inputs[11]); // useful in bronze
            int goldValue = int.Parse(inputs[12]);
            int countDown1 = int.Parse(inputs[13]); // all countDown and mana variables are useful starting in bronze
            int countDown2 = int.Parse(inputs[14]);
            int countDown3 = int.Parse(inputs[15]);
            int mana = int.Parse(inputs[16]);
            int maxMana = int.Parse(inputs[17]);
            int manaRegeneration = int.Parse(inputs[18]);
            string heroType = inputs[19]; // DEADPOOL, VALKYRIE, DOCTOR_STRANGE, HULK, IRONMAN
            int isVisible = int.Parse(inputs[20]); // 0 if it isn't
            int itemsOwned = int.Parse(inputs[21]); // useful from wood1

            string unitType = inputs[2]; // UNIT, HERO, TOWER, can also be GROOT from wood1

            // TODO: derive base type to children
            //entity = Reflection.ToDerived<Entity, [ParsedUnitTypeClass]>(entity);

            return entity;
        }
    }
}
