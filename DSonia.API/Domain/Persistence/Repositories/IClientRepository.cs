using DSonia.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Persistence.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> ListAsync();
        Task<Client> FindById(int id);
        Task AddAsync(Client client);
        void Update(Client client);
        void Remove(Client client);
    }
}
