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
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClientRepository _clientRepository;

        public ClientService(IUnitOfWork unitOfWork, IClientRepository clientRepository)
        {
            _unitOfWork = unitOfWork;
            _clientRepository = clientRepository;
        }

        public async Task<ClientResponse> DeleteAsync(int id)
        {
            var existingClient = await _clientRepository.FindById(id);
            if (existingClient == null)
                return new ClientResponse("Client not found");
            try
            {
                _clientRepository.Remove(existingClient);
                await _unitOfWork.CompleteAsync();
                return new ClientResponse(existingClient);
            }
            catch (Exception e)
            {
                return new ClientResponse("Has ocurred an error deleting the Client " + e.Message);
            }
        }

        public async Task<ClientResponse> GetByIdAsync(int id)
        {
            var existingClient = await _clientRepository.FindById(id);
            if (existingClient == null)
                return new ClientResponse("Client not found");

            return new ClientResponse(existingClient);
        }

        public async Task<IEnumerable<Client>> ListAsync()
        {
            return await _clientRepository.ListAsync();
        }

        public async Task<ClientResponse> SaveAsync(Client client)
        {
            try
            {
                await _clientRepository.AddAsync(client);
                await _unitOfWork.CompleteAsync();

                return new ClientResponse(client);
            }
            catch (Exception e)
            {
                return new ClientResponse($"An error ocurred while saving the Client: {e.Message}");
            }
        }

        public async Task<ClientResponse> UpdateAsync(int id, Client client)
        {
            var existingClient = await _clientRepository.FindById(id);
            if (existingClient == null)
                return new ClientResponse("Client not found");

            existingClient.Name = client.Name;
            existingClient.Phone = client.Phone;
            existingClient.Address = client.Address;
            try
            {
                _clientRepository.Update(existingClient);
                await _unitOfWork.CompleteAsync();

                return new ClientResponse(existingClient);
            }
            catch (Exception ex)
            {
                return new ClientResponse($"An error ocurred while updating the Client: {ex.Message}");
            }
        }
    }
}
