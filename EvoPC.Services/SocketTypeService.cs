using AutoMapper;
using EvoPC.Models.DTOs.VM;
using EvoPC.Models.Entities;
using EvoPC.Models.Interfaces;
using EvoPC.DataAccess.Interfaces;
using System.Collections.Generic;

namespace EvoPC.Services
{
    public class SocketTypeService : ISocketTypeService
    {
        private readonly IRepository<SocketType, int> socketTypeRep;
        private readonly IMapper mapper;

        public SocketTypeService(IRepository<SocketType, int> socketTypeRep, IMapper mapper)
        {
            this.socketTypeRep = socketTypeRep;
            this.mapper = mapper;
        }

        public void AddSocketType(SocketTypeVM dto)
        {
            var entity = mapper.Map<SocketType>(dto);
            socketTypeRep.Add(entity);
        }

        public void DeleteSocketType(int id)
        {
            var entity = socketTypeRep.GetInstance(id);
            if (entity == null)
                return;

            socketTypeRep.Delete(entity);
        }

        public SocketTypeVM GetSocketType(int id)
        {
            var entity = socketTypeRep.GetInstance(id);
            return mapper.Map<SocketTypeVM>(entity);
        }

        public IEnumerable<SocketTypeVM> GetAllSocketType()
        {
            var procesors = socketTypeRep.GetAll();
            return mapper.Map<List<SocketTypeVM>>(procesors);
        }

        public void UpdateSocketType(int id, SocketTypeVM dto)
        {
            var entity = socketTypeRep.GetInstance(id);
            if (entity == null)
                return;

            mapper.Map(dto, entity);
            socketTypeRep.Update(entity);
        }
    }
}
