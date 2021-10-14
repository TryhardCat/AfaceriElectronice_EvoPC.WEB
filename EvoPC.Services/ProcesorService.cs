using AutoMapper;
using EvoPC.DataAccess.Interfaces;
using EvoPC.Models.DTOs;
using EvoPC.Models.DTOs.VM;
using EvoPC.Models.Entities;
using EvoPC.Models.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EvoPC.Services
{
    public class ProcesorService : IProcesorServices
    {
        private const string
            imgFolderName = "img";
        
        private readonly IRepository<Procesor, int> procesorRep;
        private readonly IRepository<SocketType, int> socketTypeRep;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProcesorService(IRepository<Procesor, int> procesorRep, IRepository<SocketType, int> socketTypeRep, 
            IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            this.procesorRep = procesorRep;
            this.socketTypeRep = socketTypeRep;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
        }

        public void AddProcesor(ProcesorVM dto)
        {
            SaveImage(dto);

            var entity = mapper.Map<Procesor>(dto);
            procesorRep.Add(entity);

        }

        public void DeleteProcesor(int id)
        {
            var entity = procesorRep.GetInstance(id);

            if (string.IsNullOrWhiteSpace(entity.ImagePath))
            {

                var filePath = Path.Combine(hostingEnvironment.WebRootPath, entity.ImagePath);

                if (File.Exists(filePath))
                    File.Delete(filePath);

            }

            procesorRep.Delete(entity);
        }

        public IEnumerable<ProcesorVM> GetAllProcesors()
        {
            var list = procesorRep.GetAll();
            return mapper.Map<List<ProcesorVM>>(list);
        }

        public ProcesorVM GetProcesor(int id)
        {
            var entity = procesorRep.GetInstance(id);
            return mapper.Map<ProcesorVM>(entity);
        }

        
        public void UpdateProcesor(int id, ProcesorVM dto)
        {
            var entity = procesorRep.GetInstance(id);
            var oldFileRelativePath = entity.ImagePath;
            if (dto.ProcImage == null)
                dto.ImagePath = oldFileRelativePath;
            else
            {
                if (!string.IsNullOrWhiteSpace(oldFileRelativePath))
                {
                    var oldFileFullPath = Path.Combine(hostingEnvironment.WebRootPath, oldFileRelativePath);
                    if (File.Exists(oldFileFullPath))
                        File.Delete(oldFileFullPath);
                }

                SaveImage(dto);
            }

            mapper.Map(dto, entity);
            procesorRep.Update(entity);
        }

        public List<IdNameDto> GetSocketTypes()
        {
            return socketTypeRep.GetAll().Select(e => new IdNameDto(e.Id, e.Name)).ToList();
        }

        private void SaveImage(ProcesorVM dto)
        {
            if (dto.ProcImage == null)
                return;

            var imgFolderPath = Path.Combine(hostingEnvironment.WebRootPath, imgFolderName);

            if(!Directory.Exists(imgFolderPath))
                Directory.CreateDirectory(imgFolderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(dto.ProcImage.FileName);
            var imgFullPath = Path.Combine(imgFolderPath, fileName);

            using (var fileStream = new FileStream(imgFullPath, FileMode.Create))
                dto.ProcImage.CopyTo(fileStream);

            dto.ImagePath = Path.Combine(imgFolderName, fileName);
        }
    }
}
