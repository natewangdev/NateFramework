using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nate.Data.Core.Models;
using Nate.Data.EntityFrameworkCore.Interfaces;
using Nate.Data.EntityFrameworkCore.Sample.Models.Dtos.Requests;
using Nate.Data.EntityFrameworkCore.Sample.Models.Entities;
using System.Linq.Expressions;

namespace Nate.Data.EntityFrameworkCore.Sample.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var repository = _unitOfWork.GetRepository<User>();
            return await repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool includeDeleted = false)
        {
            var repository = _unitOfWork.GetRepository<User>();
            return await repository.GetAllAsync(includeDeleted);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var repository = _unitOfWork.GetRepository<User>();
            return await repository.Query()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var repository = _unitOfWork.GetRepository<User>();
                    await repository.AddAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                    var address = _mapper.Map<UserAddress>(dto);
                    address.UserId = user.Id;
                    var userAddressRepo = _unitOfWork.GetRepository<UserAddress>();
                    await userAddressRepo.AddAsync(address);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }
        }

        public async Task<User?> UpdateUserAsync(UpdateUserDto userDto)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var user = await repository.GetByIdAsync(userDto.Id);
            if (user == null) return null;
            user.Email = userDto.UserEmail;
            user.Username = userDto.UserName;
            await repository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var repository = _unitOfWork.GetRepository<User>();
            var user = await repository.GetByIdAsync(id);
            if (user != null)
            {
                await repository.DeleteAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<PagedList<User>> GetUserPagedAsync(int pageNumber, int pageSize, string? userName)
        {
            var dbset = _unitOfWork.GetRepository<User>();

            Expression<Func<User, bool>>? filter = null;
            if (!string.IsNullOrEmpty(userName))
            {
                filter = u => u.Username.Contains(userName) || u.Email.Contains(userName);
            }

            return await dbset.GetPagedAsync(pageNumber, pageSize, filter, p => p.OrderByDescending(u => u.CreatedTime));
        }
    }
}
