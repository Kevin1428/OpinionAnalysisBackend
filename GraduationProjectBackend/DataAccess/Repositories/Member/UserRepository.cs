using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.Models.Member;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GraduationProjectBackend.DataAccess.Repositories.Member
{
    public class UserRepository : IRepository<User>
    {
        readonly MssqlDbContext _dbcontext;
        public UserRepository(MssqlDbContext mssqlDbcontext)
        {
            _dbcontext = mssqlDbcontext;
        }


        public async Task<User?> GetByID(int id)
        {
            return await _dbcontext.users.FirstOrDefaultAsync(u => u.userId == id);
        }
        public async Task<User?> GetByCondition(Expression<Func<User, bool>> condition)
        {
            return await _dbcontext.users.FirstOrDefaultAsync(condition);
        }
        public async Task<List<User>> GetAllByCondition(Expression<Func<User, bool>> condition)
        {
            return await _dbcontext.users.Where(condition).ToListAsync();
        }

        public async Task Add(User model)
        {
            await _dbcontext.users.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteByEntity(User model)
        {
            _dbcontext.users.Remove(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            User? user = await GetByID(id);
            if (user != null)
            {
                _dbcontext.users.Remove(user);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task Update(User model)
        {
            _dbcontext.users.Update(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
