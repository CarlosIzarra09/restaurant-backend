using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services
{
    public interface IOrderDetailService
    {
        Task<OrderDetailResponse> AssignOrderDetailAsync(int orderId, int productId);
        Task<OrderDetailResponse> UnassignOrderDetailAsync(int orderId, int productId);
    }
}
