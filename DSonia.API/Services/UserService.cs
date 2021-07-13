using AutoMapper;
using DSonia.API.Domain.Models;
using DSonia.API.Domain.Persistence.Repositories;
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
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this._appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var users = await _userRepository.ListAsync();             

            // TODO: Replace with Repository-based Behavior
            var user = users.SingleOrDefault(x => x.Username == request.Username);

            if (user == null || !BCryptNet.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new ApplicationException("Username or password is incorrect");
            }

            if (user == null) return null;

            var response = _mapper.Map<User, AuthenticationResponse>(user);
            response.Token = GenerateJwtToken(user);

            return response;
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

          

        public async Task<UserResponse> Register(RegisterRequest request)
        {
            var users = await _userRepository.ListAsync();
            if (users.Any(x => x.Username == request.Username))
                return new UserResponse("Username '" + request.Username + "' is already taken");

            
            var user = _mapper.Map<RegisterRequest, User>(request);
            user.PasswordHash = BCryptNet.BCrypt.HashPassword(request.Password);

            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception e)
            {
                return new UserResponse($"An error ocurred while saving the user: {e.Message}");
            }

        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await _userRepository.FindById(id);
            if (existingUser == null)
                return new UserResponse("User not found");

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.PasswordHash = BCryptNet.BCrypt.HashPassword(user.PasswordHash);
            existingUser.Username = user.Username;
            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await _userRepository.FindById(id);
            if (existingUser == null)
                return new UserResponse("User not found");
            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();
                return new UserResponse(existingUser);
            }
            catch (Exception e)
            {
                return new UserResponse("Has ocurred an error deleting the user " + e.Message);
            }
        }
    }
}
