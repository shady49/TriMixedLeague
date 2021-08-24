using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriMixedLeague.Models;

namespace TriMixedLeague.Services
{
    public class MockDataStore : IDataStore<Bowler>
    {
        public static List<Bowler> bowlers;

        public MockDataStore()
        {
            bowlers = new List<Bowler>()
            {
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Team 1", Average=0, Indicator="t", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" },
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Bowler1", Average=0, Indicator="b", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" },
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Bowler2", Average=0, Indicator="b", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" },
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Bowler3", Average=0, Indicator="b", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" },
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Team 2", Average=0, Indicator="t", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" },
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Bowler1", Average=0, Indicator="b", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" },
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Bowler2", Average=0, Indicator="b", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" },
                new Bowler { Id = Guid.NewGuid().ToString(), Name="Bowler3", Average=0, Indicator="b", BowlerHTTP="", Game1=0, Game2=0, Game3=0, Handicap=0, TeamHTTP="" }
            };
        }

        public async Task<bool> AddItemAsync(Bowler item)
        {
            bowlers.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Bowler item)
        {
            var oldItem = bowlers.Where((Bowler arg) => arg.Id == item.Id).FirstOrDefault();
            bowlers.Remove(oldItem);
            bowlers.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = bowlers.Where((Bowler arg) => arg.Id == id).FirstOrDefault();
            bowlers.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Bowler> GetItemAsync(string id)
        {
            return await Task.FromResult(bowlers.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Bowler>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(bowlers);
        }
    }
}