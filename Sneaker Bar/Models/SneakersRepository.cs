using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Sneaker_Bar.Models
{
    public class SneakersRepository
    {
        private readonly SneakersContext context;

        public SneakersRepository(SneakersContext _context) {
            context = _context;
        }

        public IQueryable<Sneakers> GetSneakers() {
            return context.Sneakers.OrderBy(x => x.Company);
        }

        public Sneakers GetSneakersById(int Id) {
            return context.Sneakers.Single(x => x.Id == Id);
        }

        public int SaveSneakers(Sneakers sneakers) {
            if (sneakers.Id == default)
            {
                context.Entry(sneakers).State = EntityState.Added;
            }
            else {
                context.Entry(sneakers).State = EntityState.Modified;
            }
            context.SaveChanges();
            return sneakers.Id;
        }

        public void DeleteSneakers(Sneakers sneakers) {
            context.Sneakers.Remove(sneakers);
            context.SaveChanges();
        }
    }
}
