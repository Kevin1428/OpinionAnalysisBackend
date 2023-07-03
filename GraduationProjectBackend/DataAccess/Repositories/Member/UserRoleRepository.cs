using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.Models.Member;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GraduationProjectBackend.DataAccess.Repositories.Member
{
    public class UserRoleRepository : IRepository<UserRole>
    {
        readonly MssqlDbContext _dbcontext;
        public UserRoleRepository(MssqlDbContext mssqlDbcontext)
        {
            _dbcontext = mssqlDbcontext;
        }

        public async Task<UserRole?> GetByID(int id)
        {
            return await _dbcontext.UserRoles.FirstOrDefaultAsync(u => u.UserRoleId == id);
        }
        public async Task<UserRole?> GetByCondition(Expression<Func<UserRole, bool>> condition)
        {
            return await _dbcontext.UserRoles.FirstOrDefaultAsync(condition);
        }
        public async Task<List<UserRole>> GetAllByCondition(Expression<Func<UserRole, bool>> condition)
        {
            return await _dbcontext.UserRoles.Where(condition).ToListAsync();
        }

        public async Task Add(UserRole model)
        {
            await _dbcontext.UserRoles.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteByEntity(UserRole model)
        {
            _dbcontext.UserRoles.Remove(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            UserRole? UserRole = await GetByID(id);
            if (UserRole != null)
            {
                _dbcontext.UserRoles.Remove(UserRole);
                await _dbcontext.SaveChangesAsync();
            }
        }



        public async Task Update(UserRole model)
        {
            _dbcontext.UserRoles.Update(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
