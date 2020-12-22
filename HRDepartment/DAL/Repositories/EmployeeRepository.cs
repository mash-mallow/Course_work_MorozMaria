using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private HRContext db;
        private bool disposedValue = false;
        public EmployeeRepository(HRContext hR)
        {
            db = hR;
        }
        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }
        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        public void Create(Employee item)
        {
            db.Employees.Add(item);
        }

        public void Update(Employee item)
        {
            if (item != null)
            {
                db.SaveChanges();
            }
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            return db.Employees.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            var x = db.Employees.Find(id);
            if (x != null)
                db.Employees.Remove(x);
        }

        public Employee FindOne(Func<Employee, bool> predicate)
        {
            return db.Employees.Where(predicate).FirstOrDefault();
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
