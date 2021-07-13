using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Services;
using DSonia.API.Domain.Services.Communication;
using DSonia.API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net;

namespace DSonia.API.Services
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User
            {
                Id=1,
                FirstName ="Jhon",
                LastName ="Doe",
                Username ="jhon.doe@gmail.com",
                PasswordHash = BCryptNet.BCrypt.HashPassword("test"),

            },
            new User
            {
                Id=2,
                FirstName ="Jason",
                LastName = "Bourne",
                Username = "jason.bourne@treatstone.gov",
                PasswordHash = BCryptNet.BCrypt.HashPassword("password")
            }
        };

        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IMapper mapper)
        {
            this._appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            // TODO: Replace with Repository-based Behavior
            var user = _users.SingleOrDefault(x => x.Username == request.Username);

            if (user == null || !BCryptNet.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new ApplicationException("Username or password is incorrect");
            }

            if (user == null) return null;

            var response = _mapper.Map<User, AuthenticationResponse>(user);
            response.Token = GenerateJwtToken(user);

            return response;
        }

        public IEnumerable<User> GetAll()
        {
            Console.WriteLine("Users Count is " + _users.Count);
            return _users;
        }

        private string GenerateJwtToken(User user) {
            //Object which handler tokens
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public void Register(RegisterRequest request)
        {
            // TODO: Replace with Repository-based Behavior
            if (_users.Any(x => x.Username == request.Username))
                throw new ApplicationException("Username '" + request.Username + "' is already taken");

            var user = _mapper.Map<RegisterRequest, User>(request);

            user.PasswordHash = BCryptNet.BCrypt.HashPassword(request.Password);

            // TODO: Replace with Repository-based Behavior
            _users.Add(user);
            Console.WriteLine("Users Count is " + _users.Count);
        }
    }
}
