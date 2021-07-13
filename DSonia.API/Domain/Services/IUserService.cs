using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSonia.API.Domain.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        void Register(RegisterRequest request);
        IEnumerable<User> GetAll();
    }
}
