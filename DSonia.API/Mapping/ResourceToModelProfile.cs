using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using DSonia.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Mapping
{
    public class ResourceToModelProfile: Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<SaveUserResource, User>();
            CreateMap<SaveAttendanceResource, Attendance>();
            CreateMap<SaveEmployeeResource, Employee>();
            CreateMap<SaveSalaryResource, Salary>();
            CreateMap<SaveOrderResource, Order>();
            CreateMap<SavePaymentMethodResource, PaymentMethod>();
            CreateMap<SaveClientResource, Client>();
        }
    }
}
