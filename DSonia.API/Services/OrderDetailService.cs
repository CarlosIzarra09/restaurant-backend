using DSonia.API.Domain.Models;
using DSonia.API.Domain.Persistence.Repositories;
using DSonia.API.Domain.Services;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderDetailService(IUnitOfWork unitOfWork, 
            IOrderDetailRepository orderDetailRepository, 
            IOrderRepository orderRepository, 
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderDetailResponse> AssignOrderDetailAsync(int orderId, int productId)
        {
            var existingOrder = await _orderRepository.FindById(orderId);
            var existingProduct = await _productRepository.FindById(productId);

            if (existingOrder == null || existingProduct == null)
                return new OrderDetailResponse("orderId or productId not found");

            try
            {
                var orderDetail = new OrderDetail();
                orderDetail.OrderId = existingOrder.Id;
                orderDetail.ProductId = existingProduct.Id;

                await _orderDetailRepository.AddAsync(orderDetail);
                await _unitOfWork.CompleteAsync();
                return new OrderDetailResponse(orderDetail);
            }
            catch (Exception e)
            {
                return new OrderDetailResponse($"Has ocurred an error assign the order detail: {e.Message}");
            }
        }

        public async Task<OrderDetailResponse> UnassignOrderDetailAsync(int orderId, int productId)
        {
            var existingOrderDetail = await _orderDetailRepository.FindByOrderIdAndProductId(orderId, productId);
            if (existingOrderDetail == null)
                return new OrderDetailResponse("OrderDetail not found");

            try
            {
                _orderDetailRepository.Remove(existingOrderDetail);
                await _unitOfWork.CompleteAsync();
                return new OrderDetailResponse(existingOrderDetail);
            }
            catch (Exception e)
            {
                return new OrderDetailResponse($"Has ocurred an error unassign the order detail: {e.Message}");
            }
        }
    }
}
