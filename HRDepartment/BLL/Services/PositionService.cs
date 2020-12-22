using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Infrastructure;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PositionService : IPositionService
    {
        private IDataManager db;
        public PositionService(IDataManager dataManager)
        {
            db = dataManager;
        }
        public IEnumerable<PositionDTO> GetPositions()
        {
            var mapper = new MapperConfiguration(x => x.CreateMap<Position, PositionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Position>, List<PositionDTO>>(db.Positions.GetAll());
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PositionDTO> GetTop5()
        {
            var config = new MapperConfiguration(x => x.CreateMap<Position, PositionDTO>());
            var mapper = new Mapper(config);
            var positions = mapper.Map<IEnumerable<Position>, List<PositionDTO>>(db.Positions.GetAll());
            var top = positions.OrderByDescending(employee => (employee.Hours / employee.Salary));
            var top5 = top.Take(5);
            return top5;
        }

        public void UpdatePosition(PositionDTO positionDTO)
        {
            Position position = db.Positions.Get(positionDTO.PositionId);
            if (position == null)
            {
                throw new ValidationException("Відсутній підрозділ з вказаним ID");
            }

            position.Name = positionDTO.Name;
            position.Hours = positionDTO.Hours;
            position.Salary = positionDTO.Salary;

            db.Positions.Update(position);
        }
    }
}
