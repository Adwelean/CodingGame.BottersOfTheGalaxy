namespace Core.Factories
{
    using System;

    using Core.Actors;
    using Core.Utils;

    public static class EntityFactory
    {
        public static Entity ParseEntity(string[] inputs)
        {
            EntityType entityType;
            Entity entity = null;

            int unitId = int.Parse(inputs[0]);
            int x = int.Parse(inputs[3]);
            int y = int.Parse(inputs[4]);
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
            int isVisible = int.Parse(inputs[20]); // 0 if it isn't
            int itemsOwned = int.Parse(inputs[21]); // useful from wood1

            //entity = new Entity(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);

            string unitType = inputs[2]; // UNIT, HERO, TOWER, can also be GROOT from wood1

            if(Enum.TryParse(unitType, out entityType))
            {
                switch (entityType)
                {
                    case EntityType.UNIT:
                        //entity = UnBoxingHelper.ToDerived<Entity, Creep>(entity);
                        entity = new Creep(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);
                        break;

                    case EntityType.HERO:
                        HeroType heroType;
                        string tmpHeroType = inputs[19]; // DEADPOOL, VALKYRIE, DOCTOR_STRANGE, HULK, IRONMAN

                        // TODO: refactoring
                        if(Enum.TryParse(tmpHeroType, out heroType))
                        {
                            /*entity = UnBoxingHelper.ToDerived<Entity, Hero>(entity);
                            (entity as Hero).HeroType = heroType;*/

                            entity = new Hero(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue, countDown1, countDown2, countDown3, mana, maxMana, manaRegeneration, isVisible, itemsOwned, heroType);
                        }
                        break;

                    case EntityType.TOWER:
                        //entity = UnBoxingHelper.ToDerived<Entity, Tower>(entity);
                        entity = new Tower(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);
                        break;

                    case EntityType.GROOT:
                        //entity = UnBoxingHelper.ToDerived<Entity, Groot>(entity);
                        entity = new Groot(x, y, unitId, attackRange, health, maxHealth, shield, attackDamage, movementSpeed, stunDuration, goldValue);
                        break;
                }
            }


            return entity;
        }
    }
}
