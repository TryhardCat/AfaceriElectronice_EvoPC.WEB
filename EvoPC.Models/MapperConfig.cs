using AutoMapper;
using EvoPC.Models.DTOs.VM;
using EvoPC.Models.Entities;

namespace EvoPC.Models
{
    public static class MapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SocketType, SocketTypeVM>();
                cfg.CreateMap<SocketTypeVM, SocketType>();

                cfg.CreateMap<Procesor, ProcesorVM>();
                cfg.CreateMap<ProcesorVM, Procesor>();
            });

            return config.CreateMapper();
        }
    }
}

