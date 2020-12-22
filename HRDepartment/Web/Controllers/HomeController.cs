﻿using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeService employeeService;
        private IDepartmentService departmentService;
        private IEmployeeToProjectService employeeToProjectService;
        private IPositionService positionService;
        private IProjectService projectService;
        public HomeController(IEmployeeService employee, IDepartmentService dep, IEmployeeToProjectService emp, 
            IPositionService pos, IProjectService proj)
        {
            employeeService = employee;
            departmentService = dep;
            employeeToProjectService = emp;
            positionService = pos;
            projectService = proj;
        }
        public ActionResult Index()
        {
            IEnumerable<EmployeeDTO> employees = employeeService.GetEmployees();
            IEnumerable<PositionDTO> positionsDTO = positionService.GetPositions();
            IEnumerable<DepartmentDTO> deartmentsDTO = departmentService.GetDepartments();
            var positions = (from x in employees
                            join p in positionsDTO on x.PositionId equals p.PositionId
                            select p.Name).ToList();
            var deps = (from x in employees
                       join d in deartmentsDTO on x.DepartmentId equals d.DepartmentId
                       select d.Name).ToList();

            var mapper = new MapperConfiguration(x => x.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            var employeesModel = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employees);
            List<FullEmployeeViewModel> fullEmployees = new List<FullEmployeeViewModel>();
            for(var i = 0; i < positions.Count(); i++)
            {
                fullEmployees.Add(new FullEmployeeViewModel { 
                    EmployeeId = employeesModel[i].EmployeeId,
                    Name = employeesModel[i].Name,
                    Surname = employeesModel[i].Surname,
                    Experience = employeesModel[i].Experience,
                    PaymentNum = employeesModel[i].PaymentNum,
                    DepartmentName = deps[i],
                    Position = positions[i]
                });
            }

            return View(fullEmployees);
        }
        public ActionResult EmployeesProjects(int id)
        {
            var projects = projectService.GetProjectsByEmployee(id);
            var mapper = new MapperConfiguration(x => x.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            var projectsModel = mapper.Map<IEnumerable<ProjectDTO>, List<ProjectViewModel>>(projects);
            return View(projectsModel);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            var employee = new EmployeeViewModel { EmployeeId = 0 };
            return View(employee);
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeViewModel model)
        {
            var counter = employeeService.GetEmployees().Last();
            var counterId = counter.EmployeeId + 1;
            model.EmployeeId = counterId;

            var deps = departmentService.GetDepartments();
            var depId = from x in deps select x.DepartmentId;

            var positions = positionService.GetPositions();
            var posId = from x in positions select x.PositionId;

            if (depId.Contains(model.DepartmentId))
            {
                if (posId.Contains(model.PositionId))
                {
                    var dep = deps.Where(x => x.DepartmentId == model.DepartmentId).FirstOrDefault().DepartmentId;
                    var pos = positions.Where(x => x.PositionId == model.PositionId).FirstOrDefault().PositionId;

                    EmployeeDTO newEmpl = new EmployeeDTO
                    {
                        EmployeeId = model.EmployeeId,
                        Name = model.Name,
                        Surname = model.Surname,
                        Experience = model.Experience,
                        PaymentNum = model.PaymentNum,
                        DepartmentId = dep,
                        PositionId = pos
                    };
                    employeeService.AddEmployee(newEmpl);
                }
                
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult UpdateEmployee()
        {
            var employee = new EmployeeViewModel { EmployeeId = 0 };
            return View(employee);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeViewModel model)
        {
            var counterId = model.EmployeeId;

            var deps = departmentService.GetDepartments();
            var depId = from x in deps select x.DepartmentId;

            var positions = positionService.GetPositions();
            var posId = from x in positions select x.PositionId;

            if (depId.Contains(model.DepartmentId))
            {
                if (posId.Contains(model.PositionId))
                {
                    var dep = deps.Where(x => x.DepartmentId == model.DepartmentId).FirstOrDefault().DepartmentId;
                    var pos = positions.Where(x => x.PositionId == model.PositionId).FirstOrDefault().PositionId;

                    EmployeeDTO newEmpl = new EmployeeDTO
                    {
                        EmployeeId = model.EmployeeId,
                        Name = model.Name,
                        Surname = model.Surname,
                        Experience = model.Experience,
                        PaymentNum = model.PaymentNum,
                        DepartmentId = dep,
                        PositionId = pos
                    };
                    employeeService.UpdateEmployee(newEmpl);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeleteEmployee()
        {
            var model = new EmployeeViewModel();

            return View(model);
        }

       [HttpPost]
        public ActionResult DeleteEmployee(EmployeeViewModel model)
        {
            employeeService.DeleteEmployee(model.EmployeeId);
            employeeToProjectService.DeleteEmployeeToProjects(model.EmployeeId);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetAllEmployeesSortedByName()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>());
            var mapper = new Mapper(config);
            IEnumerable<EmployeeViewModel> employees = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeService.GetAllEmployeesSortedByName());
            return View(employees);
        }

        public ActionResult GetAllEmployeesSortedBySurname()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>());
            var mapper = new Mapper(config);
            IEnumerable<EmployeeViewModel> employees = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeService.GetAllEmployeesSortedBySurname());
            return View(employees);
        }

    }
}