using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, AuthenticationResponse>();
        }
    }
}
