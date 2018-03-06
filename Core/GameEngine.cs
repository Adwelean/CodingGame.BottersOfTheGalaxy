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
        Team playerTeam;
        Team ennemyTeam;

        FightManager fightManager = new FightManager();

        public List<Bush> Bushes { get; set; }
        public List<Spawn> Spawns { get; set; }
        public List<ItemBase> Items { get; set; }
        public Player CurrentPlayer { get; set; }
        public Team PlayerTeam { get => this.playerTeam; set => this.playerTeam = value; }
        public Team EnnemyTeam { get => this.ennemyTeam; set => this.ennemyTeam = value; }


        public GameEngine()
        {
            Bushes = new List<Bush>();
            Spawns = new List<Spawn>();
            Items = new List<ItemBase>();
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

            PlayerTeam = new Team(currentPlayerTeamId);
            EnnemyTeam = new Team(currentPlayerTeamId == 0 ? 1 : 0);
        }

        public void Start()
        {
            string[] inputs;

            this.fightManager.Initialize(CurrentPlayer, Items);
            this.fightManager.AddTeam(this.playerTeam);
            this.fightManager.AddTeam(this.ennemyTeam);
            this.fightManager.Picking();
            //Console.WriteLine(this.fightManager.RenderOutput());

            // game loop
            while (true)
            {
                try
                {
                    

                    PlayerTeam.Gold = int.Parse(Console.ReadLine());
                    EnnemyTeam.Gold = int.Parse(Console.ReadLine());

                    int roundType = int.Parse(Console.ReadLine()); // a positive value will show the number of heroes that await a command
                    int entityCount = int.Parse(Console.ReadLine());

                    List<KeyValuePair<int, Entity>> newEntities = new List<KeyValuePair<int, Entity>>();

                    for (int i = 0; i < entityCount; i++)
                    {
                        inputs = Console.ReadLine().Split(' ');
                        int teamId = int.Parse(inputs[1]);

                        newEntities.Add(new KeyValuePair<int, Entity>(teamId, EntityFactory.ParseEntity(inputs)));
                    }

                    PlayerTeam.UpdateEntities(newEntities.Where(x => x.Key == PlayerTeam.Id).Select(x => x.Value).ToList());
                    EnnemyTeam.UpdateEntities(newEntities.Where(x => x.Key == EnnemyTeam.Id).Select(x => x.Value).ToList());

                    newEntities.ForEach(entity =>
                    {
                        var team = (PlayerTeam.Id == entity.Key) ? PlayerTeam : EnnemyTeam;

                        if (team.Entities.Any(x => x.Id == entity.Value.Id))
                            team.Entities.FirstOrDefault(x => x.Id == entity.Value.Id).Update(entity.Value);
                        else
                            team.Entities.Add(entity.Value);
                    });

                    
                    // Write an action using Console.WriteLine()
                    // To debug: Console.Error.WriteLine("Debug messages...");


                    this.fightManager.NextTurn();

                    // If roundType has a negative value then you need to output a Hero name, such as "DEADPOOL" or "VALKYRIE".
                    // Else you need to output roundType number of any valid action, such as "WAIT" or "ATTACK unitId"
                    //Console.WriteLine("WAIT");
                    Console.WriteLine(this.fightManager.RenderOutput());
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.InnerException);
                    Console.WriteLine("WAIT");
                }
            }
        }
    }
}
