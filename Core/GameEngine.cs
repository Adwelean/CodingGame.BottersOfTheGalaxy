namespace Core
{
    using Core.Actors;
    using Core.Factories;
    using Core.Fights;
    using Core.Interactives;
    using Core.Items;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GameEngine
    {
        FightManager fightManager = new FightManager();

        public List<Bush> Bushes { get; set; }
        public List<Spawn> Spawns { get; set; }
        public List<ItemBase> Items { get; set; }
        public List<Team> Teams { get; set; }
        public Player CurrentPlayer { get; set; }


        public GameEngine()
        {
            Bushes = new List<Bush>();
            Spawns = new List<Spawn>();
            Items = new List<ItemBase>();
            Teams = new List<Team>();
        }

        static void Main(string[] args)
        {
            GameEngine game = new GameEngine();
            game.Initialize();
            game.Start();
        }

        public void Initialize()
        {
            int currentPlayerTeamId;
            string[] inputs;

            if (int.TryParse(Console.ReadLine(), out currentPlayerTeamId))
            {
                CurrentPlayer = new Player(currentPlayerTeamId);
            }

            int bushAndSpawnPointCount = int.Parse(Console.ReadLine()); // useful from wood1, represents the number of bushes and the number of places where neutral units can spawn

            for (int i = 0; i < bushAndSpawnPointCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');

                var interactiveObject = InteractiveObjectFactory.ParseInteractiveObject(inputs);

                if(interactiveObject != null)
                {
                    if (interactiveObject is Bush)
                        Bushes.Add(interactiveObject as Bush);
                    else if (interactiveObject is Spawn)
                        Spawns.Add(interactiveObject as Spawn);
                }
            }

            int itemCount = int.Parse(Console.ReadLine()); // useful from wood2

            for (int i = 0; i < itemCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');

                var item = ItemFactory.ParseItem(inputs);

                if (item != null)
                    Items.Add(item);
            }

            Teams.Add(new Team(currentPlayerTeamId));
            Teams.Add(new Team(currentPlayerTeamId == 0 ? 1 : 0));

            Console.Error.WriteLine(currentPlayerTeamId);
        }

        public void Start()
        {
            string[] inputs;

            this.fightManager.Initialize(CurrentPlayer);
            this.fightManager.AddTeams(Teams);
            this.fightManager.StartFight();


            // game loop
            while (true)
            {
                var currentPlayerTeam = Teams.FirstOrDefault(x => x.Id == CurrentPlayer.TeamId);
                var ennemyTeam = Teams.FirstOrDefault(x => x.Id != CurrentPlayer.TeamId);

                int gold, enemyGold;

                if(int.TryParse(Console.ReadLine(), out gold))
                    currentPlayerTeam.Gold = gold;

                if (int.TryParse(Console.ReadLine(), out enemyGold))
                    ennemyTeam.Gold = enemyGold;
                
                int roundType = int.Parse(Console.ReadLine()); // a positive value will show the number of heroes that await a command
                int entityCount = int.Parse(Console.ReadLine());

                for (int i = 0; i < entityCount; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int teamId = int.Parse(inputs[1]);

                    var entity = EntityFactory.ParseEntity(inputs);

                    // TODO: add method ? observer ?
                    Teams.FirstOrDefault(x => x.Id == teamId).Entities.Add(entity);
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");


                this.fightManager.NextTurn();

                // If roundType has a negative value then you need to output a Hero name, such as "DEADPOOL" or "VALKYRIE".
                // Else you need to output roundType number of any valid action, such as "WAIT" or "ATTACK unitId"
                //Console.WriteLine("WAIT");
                Console.WriteLine(this.fightManager.RenderOutput());
            }
        }
    }
}
