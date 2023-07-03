using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.Models.Favorite;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GraduationProjectBackend.DataAccess.Repositories.Favorite
{
    public class FavoriteFolderItemRepository : IRepository<FavoriteFolderItem>
    {
        readonly MssqlDbContext _dbcontext;
        public FavoriteFolderItemRepository(MssqlDbContext mssqlDbcontext)
        {
            _dbcontext = mssqlDbcontext;
        }


        public async Task<FavoriteFolderItem?> GetByID(int id)
        {
            return await _dbcontext.favoriteFolderItems.Include(ffi => ffi.FavoriteItem)
                                                        .FirstOrDefaultAsync(ffi => ffi.favoriteFolderItemId == id);
        }
        public async Task<FavoriteFolderItem?> GetByCondition(Expression<Func<FavoriteFolderItem, bool>> condition)
        {
            return await _dbcontext.favoriteFolderItems.Include(fi => fi.FavoriteItem)
                                             .FirstOrDefaultAsync(condition);
        }
        public async Task<List<FavoriteFolderItem>> GetAllByCondition(Expression<Func<FavoriteFolderItem, bool>> condition)
        {
            return await _dbcontext.favoriteFolderItems.Include(fi => fi.FavoriteItem)
                                             .Where(condition)
                                             .ToListAsync();
        }

        public async Task Add(FavoriteFolderItem model)
        {
            await _dbcontext.favoriteFolderItems.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteByEntity(FavoriteFolderItem model)
        {
            _dbcontext.favoriteFolderItems.Remove(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            FavoriteFolderItem? FavoriteFolderItem = await GetByID(id);
            if (FavoriteFolderItem != null)
            {
                _dbcontext.favoriteFolderItems.Remove(FavoriteFolderItem);
                await _dbcontext.SaveChangesAsync();
            }
        }



        public async Task Update(FavoriteFolderItem model)
        {
            _dbcontext.favoriteFolderItems.Update(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}

