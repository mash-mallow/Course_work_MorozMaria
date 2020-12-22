using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        private HRContext db;
        private bool disposedValue = false;
        public DepartmentRepository(HRContext hR)
        {
            db = hR;
        }
        public IEnumerable<Department> GetAll()
        {
            return db.Departments;
        }
        public Department Get(int id)
        {
            return db.Departments.Find(id);
        }

        public void Create(Department item)
        {
            db.Departments.Add(item);
        }

        public void Update(Department item)
        {
            if (item != null)
            {
                db.SaveChanges();
            }
        }

        public IEnumerable<Department> Find(Func<Department, bool> predicate)
        {
            return db.Departments.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            var x = db.Departments.Find(id);
            if (x != null)
                db.Departments.Remove(x);
        }

        public Department FindOne(Func<Department, bool> predicate)
        {
            return db.Departments.Where(predicate).FirstOrDefault();
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
