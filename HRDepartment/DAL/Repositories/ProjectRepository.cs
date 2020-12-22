using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private HRContext db;
        private bool disposedValue = false;
        public ProjectRepository(HRContext hR)
        {
            db = hR;
        }
        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }
        public Project Get(int id)
        {
            return db.Projects.Find(id);
        }

        public void Create(Project item)
        {
            db.Projects.Add(item);
        }

        public void Update(Project item)
        {
            if (item != null)
            {
                db.SaveChanges();
            }
        }

        public IEnumerable<Project> Find(Func<Project, bool> predicate)
        {
            return db.Projects.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            var x = db.Projects.Find(id);
            if (x != null)
                db.Projects.Remove(x);
        }

        public Project FindOne(Func<Project, bool> predicate)
        {
            return db.Projects.Where(predicate).FirstOrDefault();
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
