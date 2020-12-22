using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EmployeeToProjectRepository : IRepository<EmployeeToProject>
    {
        private HRContext db;
        private bool disposedValue = false;
        public EmployeeToProjectRepository(HRContext hR)
        {
            db = hR;
        }
        public IEnumerable<EmployeeToProject> GetAll()
        {
            return db.EmployeeToProjects;
        }
        public EmployeeToProject Get(int id)
        {
            return db.EmployeeToProjects.Find(id);
        }

        public void Create(EmployeeToProject item)
        {
            db.EmployeeToProjects.Add(item);
        }

        public void Update(EmployeeToProject item)
        {
            if (item != null)
            {
                db.SaveChanges();
            }
        }

        public IEnumerable<EmployeeToProject> Find(Func<EmployeeToProject, bool> predicate)
        {
            return db.EmployeeToProjects.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            var x = db.EmployeeToProjects.Find(id);
            if (x != null)
                db.EmployeeToProjects.Remove(x);
        }

        public EmployeeToProject FindOne(Func<EmployeeToProject, bool> predicate)
        {
            return db.EmployeeToProjects.Where(predicate).FirstOrDefault();
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
