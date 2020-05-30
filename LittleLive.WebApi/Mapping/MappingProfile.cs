using AutoMapper;
using Entities = LittleLive.Core.Entities;
using Models = LittleLive.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleLive.WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to Models
            CreateMap<Entities.User, Models.User>();
            CreateMap<Entities.School, Models.School>();
            CreateMap<Entities.Class, Models.Class>();
            CreateMap<Entities.Track, Models.Track>();            

            // Model to Entity
            CreateMap<Models.User, Entities.User>();
            CreateMap<Models.School, Entities.School>();
            CreateMap<Models.Class, Entities.Class>();
            CreateMap<Models.Track, Entities.Track>();
        }
    }
}
