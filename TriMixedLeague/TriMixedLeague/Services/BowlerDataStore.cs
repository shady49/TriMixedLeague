using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriMixedLeague.Models;

namespace TriMixedLeague.Services
{
    public class BowlerDataStore : BowlerStore<Bowler>
    {
        public static List<Bowler> bowlers;

        public BowlerDataStore()
        {
            bowlers = new List<Bowler>();
            for (int dx=0;dx<Global.bowlerslist.Count; dx++)
            {

                bowlers.Add(new Bowler 
                {  
                    Id = Guid.NewGuid().ToString(),
                    Name = Global.bowlerslist[dx].Name,
                    Average = Int32.Parse(Global.bowlerslist[dx].AVG),
                    Handicap = Int32.Parse(Global.bowlerslist[dx].HCP),
                    BowlerHTTP=Global.bowlerslist[dx].HTTPbowler,
                    TeamHTTP=Global.bowlerslist[dx].HTTPteam
                });
                
            }
            //{
            //    new Bowler { Id = Guid.NewGuid().ToString(), Name = Global.bowlerslist[0].Name, Average=0 },
            //    new Bowler { Id = Guid.NewGuid().ToString(), Name = "Second bowler", Average=0 },
            //    new Bowler { Id = Guid.NewGuid().ToString(), Name = "Third bowler", Average=0 },
            //    new Bowler { Id = Guid.NewGuid().ToString(), Name = "Fourth bowler", Average=0 },
            //    new Bowler { Id = Guid.NewGuid().ToString(), Name = "Fifth bowler", Average=0 },
            //    new Bowler { Id = Guid.NewGuid().ToString(), Name = "Sixth bowler", Average=0 }
            //};
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