using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyFirstWebApplication1.Controllers.v1.Responses;
using MyFirstWebApplication1.Domain;

namespace MyFirstWebApplication1.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Post, PostResponse>();
        }
    }
}
