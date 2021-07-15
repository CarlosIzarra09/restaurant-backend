using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using DSonia.API.Resources;
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
            CreateMap<User, UserResource>();
            CreateMap<Attendance, AttendanceResource>();
            CreateMap<Employee, EmployeeResource>();
            CreateMap<Salary, SalaryResource>();
            CreateMap<Order, OrderResource>();
            CreateMap<PaymentMethod, PaymentMethodResource>();
            CreateMap<Client, ClientResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<OrderDetail, OrderDetailResource>();
        }
    }
}
