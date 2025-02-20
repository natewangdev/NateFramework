using Microsoft.AspNetCore.Mvc;
using Nate.Core.Constants;
using Nate.Core.Extensions;
using Nate.Core.Models.ApiResult;
using Nate.Data.Core.Models;
using Nate.Data.EntityFrameworkCore.Sample.Models.Dtos.Requests;
using Nate.Data.EntityFrameworkCore.Sample.Models.Entities;
using Nate.Data.EntityFrameworkCore.Sample.Services;
using System.ComponentModel.DataAnnotations;

namespace Nate.Data.EntityFrameworkCore.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ApiResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return users.ToApiResult();
        }

        [HttpGet("GetAllIncludeDeleted")]
        public async Task<ApiResult<IEnumerable<User>>> GetAllIncludeDeleted()
        {
            var users = await _userService.GetAllAsync(true);
            return users.ToApiResult();
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<User>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return "查无此人".ToFailResult<User>(ApiResultCode.NotFound);
            }
            return user.ToApiResult();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(CreateUserDto createUserDto)
        {
            await _userService.CreateUserAsync(createUserDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<User>> Update(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id)
            {
                return ApiResult<User>.Fail("失败啦");
            }

            if (updateUserDto.UserEmail == null)
            {
                throw new ValidationException("UserEmail 不能为null");
            }

            var user = await _userService.UpdateUserAsync(updateUserDto);
            return ApiResult<User>.Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<User>>> GetPaged(int pageNumber, int pageSize, [FromQuery] string? searchTerm)
        {
            var users = await _userService.GetUserPagedAsync(pageNumber, pageSize, searchTerm);
            return Ok(users);
        }
    }
}
