using System.Collections.Generic;

namespace Planetoid
{
    public class GameMode
    {
        private List<Player> players = new List<Player>();
        private Universe universe;
        public List<Player> Players { get => players; set => players = value; }


        public void StartGame(Universe universe)
        {
            this.universe = universe;

            if (this.players.Count == 0) throw new System.Exception("Needs at least 1 player to start the game");
            if (this.universe.Planets.Count == 0) throw new System.Exception("Needs at least 1 planet to start the game");

            this.AssignStartPositions();
        }

        public void AssignStartPositions()
        {
            List<Planet> freePlanets = new List<Planet>(universe.Planets);
            foreach (Player player in players)
            {
                Planet choosenPlanet = null;
                //choose random planet
                foreach (Planet planet in freePlanets)
                {
                    choosenPlanet = planet;
                    this.MakeHomePlanet(player, choosenPlanet);
                }
                if (choosenPlanet != null)
                {
                    freePlanets.Remove(choosenPlanet);
                }
            }
        }

        public void MakeHomePlanet(Player player, Planet planet)
        {
            HQ hQ = new HQ();
            player.HQ = hQ;
            planet.AddBuildingSomewhere(new HQ());
           
        }

        public void AddPlayer(string name)
        {
            Player player = new Player(new BuilderService());
            player.Name = name;
            this.players.Add(player);
        }
    }
}
