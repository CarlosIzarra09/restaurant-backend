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
    public class OrderService :IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public OrderService(IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            IEmployeeRepository employeeRepository, 
            IClientRepository clientRepository, IPaymentMethodRepository paymentMethodRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _clientRepository = clientRepository;
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<OrderResponse> DeleteAsync(int id)
        {
            var existingOrder = await _orderRepository.FindById(id);
            if (existingOrder == null)
                return new OrderResponse("Order not found");
            try
            {
                _orderRepository.Remove(existingOrder);
                await _unitOfWork.CompleteAsync();
                return new OrderResponse(existingOrder);
            }
            catch (Exception e)
            {
                return new OrderResponse("Has ocurred an error deleting the order " + e.Message);
            }
        }

        public async Task<OrderResponse> GetByIdAsync(int id)
        {
            var existingOrder = await _orderRepository.FindById(id);
            if (existingOrder == null)
                return new OrderResponse("Order not found");
            return new OrderResponse(existingOrder);
        }

        public async Task<IEnumerable<Order>> ListAndRangeDateAsync(DateTime startAt, DateTime endAt)
        {
            return await _orderRepository.ListAndRangeDateAsync(startAt, endAt);
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _orderRepository.ListAsync();
        }

        public async Task<IEnumerable<Order>> ListByClientIdAndRangeDateAsync(int clientId, DateTime startAt, DateTime endAt)
        {
            return await _orderRepository.ListByClientIdAndRangeDateAsync(clientId, startAt, endAt);
        }

        public async Task<IEnumerable<Order>> ListByClientIdAsync(int clientId)
        {
            return await _orderRepository.ListByClientIdAsync(clientId);
        }

        public async Task<IEnumerable<Order>> ListByEmployeeIdAndRangeDateAsync(int employeeId, DateTime startAt, DateTime endAt)
        {
            return await _orderRepository.ListByEmployeeIdAndRangeDateAsync(employeeId, startAt, endAt);
        }

        public async Task<IEnumerable<Order>> ListByEmployeeIdAsync(int employeeId)
        {
            return await _orderRepository.ListByEmployeeIdAsync(employeeId);
        }

        public async Task<IEnumerable<Order>> ListByPaymentMethodIdAndRangeDateAsync(int paymentMethodId, DateTime startAt, DateTime endAt)
        {
            return await _orderRepository.ListByPaymentMethodIdAndRangeDateAsync(paymentMethodId, startAt, endAt);
        }

        public async Task<IEnumerable<Order>> ListByPaymentMethodIdAsync(int paymentMethodId)
        {
            return await _orderRepository.ListByPaymentMethodIdAsync(paymentMethodId);
        }

        public async Task<OrderResponse> SaveAsync(int clientId, Order order)
        {
            var existingClient = await _clientRepository.FindById(clientId);
            var existingEmployee = await _employeeRepository.FindById(order.EmployeeId);
            var existingPaymentMethod = await _paymentMethodRepository.FindById(order.PaymentMethodId);

            if (existingEmployee == null || existingClient == null || existingPaymentMethod == null)
                return new OrderResponse("Foreign keys not found");

            try
            {
                order.ClientId = clientId;
                await _orderRepository.AddAsync(order);
                await _unitOfWork.CompleteAsync();
                return new OrderResponse(order);
            }
            catch (Exception e)
            {
                return new OrderResponse("Has ocurred an error saving the order " + e.Message);
            }
        }

        public async Task<OrderResponse> UpdateAsync(int id, Order order)
        {
            var existingOrder = await _orderRepository.FindById(id);
            if (existingOrder == null)
                return new OrderResponse("Order not found");

            try
            {
                existingOrder.RequiredTime = order.RequiredTime;
                existingOrder.ShipTime = order.ShipTime;
                existingOrder.ShipDate = order.ShipDate;
                existingOrder.ShipAddress = order.ShipAddress;
                existingOrder.ShipPrice = order.ShipPrice;
                existingOrder.Debt = order.Debt;
                existingOrder.Description = order.Description;
                _orderRepository.Update(existingOrder);
                await _unitOfWork.CompleteAsync();
                return new OrderResponse(existingOrder);
            }
            catch (Exception e)
            {
                return new OrderResponse("Has ocurred an error updating the order " + e.Message);
            }
        }
    }
}
