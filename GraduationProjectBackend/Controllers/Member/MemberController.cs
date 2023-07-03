﻿using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.Models.Member;
using GraduationProjectBackend.DataAccess.Repositories.Member;
using GraduationProjectBackend.Helper.Member;
using GraduationProjectBackend.Services.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBackend.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MssqlDbContext _context;
        private IMemberService _memberService;
        private UserRepository _userRepository;
        private JwtHelper _jwtHelper;

        public MemberController(MssqlDbContext context, IMemberService memberService, JwtHelper jwtHelper, UserRepository userRepository)
        {
            _context = context;
            _memberService = memberService;
            _jwtHelper = jwtHelper;
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {

            if (_context.users == null)
            {
                return NotFound();
            }
            return await _context.users.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            Console.WriteLine(HttpContext.User.Identity.Name);

            if (_context.users == null)
            {
                return NotFound();
            }
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.userId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(string account, string password)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'MssqlDbContext.users'  is null.");
            }
            User? user = await _memberService.Register(account, password);

            if (user != null)
            {
                return CreatedAtAction("GetUser", new { id = user.userId }, user);
            }
            else
            {
                return StatusCode(500, "Internal server error occurred.");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<String>> Login(string account, string password)
        {
            User? user = await _userRepository.GetByCondition(u => u.account == account);
            String token;

            if (user == null)
            {
                return NotFound();
            }

            if (_memberService.AuthenticatePassword(user, password))
            {
                token = await _memberService.GenerateToken(user);

                return Ok(token);
            }

            return Unauthorized();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.users == null)
            {
                return NotFound();
            }
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> UserExists(int id)
        {
            return (await _userRepository.GetByID(id) != null);
        }

    }
}
