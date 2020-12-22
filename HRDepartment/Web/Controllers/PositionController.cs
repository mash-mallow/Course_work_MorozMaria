using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class PositionController : Controller
    {
        private IPositionService positionService;
        private IEmployeeService employeeService;
        public PositionController(IPositionService position, IEmployeeService employee)
        {
            positionService = position;
            employeeService = employee;
        }
        public ActionResult Index()
        {
            var positions = positionService.GetPositions();
            var mapper = new MapperConfiguration(x => x.CreateMap<PositionDTO, PositionViewModel>()).CreateMapper();
            var positionsModel = mapper.Map<IEnumerable<PositionDTO>, List<PositionViewModel>>(positions);
            return View(positionsModel);
        }
        public ActionResult BestEmployee(int id)
        {
            var bestEmployee = employeeService.GetBestEmployee(id);
            if(bestEmployee!=null)
            { 
            var mapper = new MapperConfiguration(x => x.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            var positionsModel = mapper.Map<EmployeeDTO, EmployeeViewModel>(bestEmployee);
            return View(positionsModel);
            }
            else return Content("There is no best employee");
        }

        [HttpGet]
        public ActionResult UpdatePosition()
        {
            var pos = new PositionViewModel();
            return View(pos);
        }
        [HttpPost]
        public ActionResult UpdatePosition(PositionViewModel model)
        {
            var counterId = model.PositionId;

            PositionDTO newPos = new PositionDTO
            {
                PositionId = counterId,
                Name = model.Name,
                Hours = model.Hours,
                Salary = model.Salary
            };
            positionService.UpdatePosition(newPos);

            return RedirectToAction("Index", "Department");
        }

        public ActionResult Top5Positions()
        {
            var positions = positionService.GetTop5();
            var mapper = new MapperConfiguration(x => x.CreateMap<PositionDTO, PositionViewModel>()).CreateMapper();
            var positionsModel = mapper.Map<IEnumerable<PositionDTO>, List<PositionViewModel>>(positions);
            return View(positionsModel);
        }
    }
}