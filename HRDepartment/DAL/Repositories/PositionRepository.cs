using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PositionRepository : IRepository<Position>
    {
        private HRContext db;
        private bool disposedValue = false;
        public PositionRepository(HRContext hR)
        {
            db = hR;
        }
        public IEnumerable<Position> GetAll()
        {
            return db.Positions;
        }
        public Position Get(int id)
        {
            return db.Positions.Find(id);
        }

        public void Create(Position item)
        {
            db.Positions.Add(item);
        }

        public void Update(Position item)
        {
            if (item != null)
            {
                db.SaveChanges();
            }
        }

        public IEnumerable<Position> Find(Func<Position, bool> predicate)
        {
            return db.Positions.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            var x = db.Positions.Find(id);
            if (x != null)
                db.Positions.Remove(x);
        }

        public Position FindOne(Func<Position, bool> predicate)
        {
            return db.Positions.Where(predicate).FirstOrDefault();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
