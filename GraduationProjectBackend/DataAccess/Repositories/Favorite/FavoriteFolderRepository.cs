using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.Models.Favorite;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GraduationProjectBackend.DataAccess.Repositories.Favorite
{
    public class FavoriteFolderRepository : IRepository<FavoriteFolder>
    {

        readonly MssqlDbContext _dbcontext;
        public FavoriteFolderRepository(MssqlDbContext mssqlDbcontext)
        {
            _dbcontext = mssqlDbcontext;
        }


        public async Task<FavoriteFolder?> GetByID(int id)
        {
            return await _dbcontext.FavoriteFolders.Include(ff => ff.FavoriteFolderItems)
                                                   .ThenInclude(fi => fi.FavoriteItem)
                                                   .FirstOrDefaultAsync(u => u.FavoriteFolderId == id);
        }
        public async Task<FavoriteFolder?> GetByCondition(Expression<Func<FavoriteFolder, bool>> condition)
        {
            return await _dbcontext.FavoriteFolders.Include(ff => ff.FavoriteFolderItems)
                                             .ThenInclude(fi => fi.FavoriteItem)
                                             .FirstOrDefaultAsync(condition);
        }
        public async Task<List<FavoriteFolder>> GetAllByCondition(Expression<Func<FavoriteFolder, bool>> condition)
        {
            return await _dbcontext.FavoriteFolders.Include(ff => ff.FavoriteFolderItems)
                                             .ThenInclude(fi => fi.FavoriteItem)
                                             .Where(condition)
                                             .ToListAsync();
        }

        public async Task Add(FavoriteFolder model)
        {
            await _dbcontext.FavoriteFolders.AddAsync(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteByEntity(FavoriteFolder model)
        {
            _dbcontext.FavoriteFolders.Remove(model);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            FavoriteFolder? FavoriteFolder = await GetByID(id);
            if (FavoriteFolder != null)
            {
                _dbcontext.FavoriteFolders.Remove(FavoriteFolder);
                await _dbcontext.SaveChangesAsync();
            }
        }



        public async Task Update(FavoriteFolder model)
        {
            _dbcontext.FavoriteFolders.Update(model);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
