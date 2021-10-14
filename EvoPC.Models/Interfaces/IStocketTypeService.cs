using EvoPC.Models.DTOs.VM;
using System.Collections.Generic;

namespace EvoPC.Models.Interfaces
{
    public interface ISocketTypeService
    {
        IEnumerable<SocketTypeVM> GetAllSocketType();
        SocketTypeVM GetSocketType(int id);
        void AddSocketType(SocketTypeVM dto);
        void UpdateSocketType(int id, SocketTypeVM dto);
        void DeleteSocketType(int id);
    }
}

