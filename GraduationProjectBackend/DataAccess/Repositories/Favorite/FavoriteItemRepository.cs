using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.Models.Favorite;
using GraduationProjectBackend.DataAccess.Models.Member;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GraduationProjectBackend.DataAccess.Repositories.Favorite
{
    public class FavoriteItemRepository : IRepository<FavoriteItem>
    {
        readonly MssqlDbContext _dbcontext;
        public FavoriteItemRepository(MssqlDbContext mssqlDbcontext)
        {
            _dbcontext = mssqlDbcontext;
        }


        public async Task<FavoriteItem?> GetByID(int id)
        {
            return await _dbcontext.FavoriteItems.FirstOrDefaultAsync(u => u.FavoriteItemId == id);
        }
        public async Task<FavoriteItem?> GetByCondition(Expression<Func<FavoriteItem, bool>> condition)
        {
            return await _dbcontext.FavoriteItems.FirstOrDefaultAsync(condition);
        }
        public async Task<List<FavoriteItem>> GetAllByCondition(Expression<Func<FavoriteItem, bool>> condition)
        {
            return await _dbcontext.FavoriteItems.Where(condition).ToListAsync();
        }

        public async Task Add(FavoriteItem model)
        {
            await _dbcontext.FavoriteItems.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteByEntity(FavoriteItem model)
        {
            _dbcontext.FavoriteItems.Remove(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            FavoriteItem? FavoriteItem = await GetByID(id);
            if (FavoriteItem != null)
            {
                _dbcontext.FavoriteItems.Remove(FavoriteItem);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task Update(FavoriteItem model)
        {
            _dbcontext.FavoriteItems.Update(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
