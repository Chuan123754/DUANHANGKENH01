﻿using appAPI.Models;
using appAPI.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using appAPI.Helper;

namespace appAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _repo;

        public AccountController( IAccountRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var accounts = await _repo.GetAllAsync();
            return Ok(accounts); 
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _repo.SignUpAsync(model);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok("Tạo tài khoản thành công");
            }
            catch (InvalidOperationException ex) // ex bên repo
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> SignIn(SignInModel model)
        //{
        //    try
        //    {
        //        var userWithRoles = await _repo.SignInAsync(model);

        //        if (userWithRoles == null)
        //        {
        //            return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không đúng!" });
        //        }

        //        return Ok(new
        //        {
        //            UserId = userWithRoles.User.Id,
        //            Name = userWithRoles.User.Name,
        //            Email = userWithRoles.User.Email,
        //            Phone = userWithRoles.User.Phone,
        //            Address = userWithRoles.User.Address,
        //            Roles = userWithRoles.Roles // Trả về danh sách vai trò
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = ex.Message });
        //    }
        //}



        [HttpPost("Login")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var token = await _repo.SignInAsync(model);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            await _repo.SignOutAsync();
            return Ok();
        }
        [HttpPut("UpdateAccount/{id}")]
        public async Task<IActionResult> UpdateAccountAsync(Account account , string id)
        {
            try
            {
                var updateAccount = await _repo.GetAccountById(id);
                if (updateAccount == null)
                {
                    return NotFound();
                }                   
                var result = await _repo.UpdateAccountAsync(account,id);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccountAsync(string idAccount)
        {
            try
            {
                var result = await _repo.DeleteAccountAsync(idAccount);
                if (result.Succeeded)
                {
                    return Ok(result);
                }    
                return BadRequest(result.Errors);   
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetAccountById(string idAccount)
        {
            try
            {
                var result = await _repo.GetAccountById(idAccount);
                if(result == null)
                {
                    return NotFound();
                }    
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllAccount")]
        //[Authorize(Roles = ApplicationRole.Admin)]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            try
            {
                var lstAccount = await _repo.GetAllAccountsAsync();
                return Ok(lstAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPatch("ToggleLock/{idAccount}")]
        public async Task<IActionResult> ToggleLockAccountAsync(string idAccount)
        {
            try
            {
                var result = await _repo.ToggleLockAccountAsync(idAccount);
                if (result.Succeeded)
                {
                    return NotFound(result.Errors);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetPasswordHashByEmail")]
        public async Task<IActionResult> GetPasswordHashByEmail(string email)
        {
            try
            {
                var passwordHash = await _repo.GetPasswordHashByEmail(email);
                if (string.IsNullOrEmpty(passwordHash))
                {
                    return NotFound("Không tìm thấy PasswordHash.");
                }
                return Ok(passwordHash);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
