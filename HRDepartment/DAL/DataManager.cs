using DAL;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataManager : IDataManager
    {
        private HRContext db;
        private DepartmentRepository departmentRepository;
        private EmployeeRepository employeeRepository;
        private EmployeeToProjectRepository employeeToProject;
        private PositionRepository positionRepository;
        private ProjectRepository projectRepository;
        private bool disposed = false;


        public DataManager()
        {
           db = new HRContext();
        }
        
        public IRepository<Department> Departments
        {
            get
            {
                if (departmentRepository == null)
                    departmentRepository = new DepartmentRepository(db);
                return departmentRepository;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        public IRepository<EmployeeToProject> EmployeeToProjects
        {
            get
            {
                if (employeeToProject == null)
                    employeeToProject = new EmployeeToProjectRepository(db);
                return employeeToProject;
            }
        }

        public IRepository<Position> Positions
        {
            get
            {
                if (positionRepository == null)
                    positionRepository = new PositionRepository(db);
                return positionRepository;
            }
        }

        public IRepository<Project> Projects
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(db);
                return projectRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
