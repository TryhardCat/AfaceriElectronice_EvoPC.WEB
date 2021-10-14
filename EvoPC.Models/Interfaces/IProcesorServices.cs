using EvoPC.Models.DTOs;
using EvoPC.Models.DTOs.VM;
using System.Collections.Generic;

namespace EvoPC.Models.Interfaces
{
    public interface IProcesorServices
    {
        IEnumerable<ProcesorVM> GetAllProcesors();
        ProcesorVM GetProcesor(int id);
        void AddProcesor(ProcesorVM dto);
        void UpdateProcesor(int id, ProcesorVM dto);
        void DeleteProcesor(int id);
        List<IdNameDto> GetSocketTypes();
    }
}
