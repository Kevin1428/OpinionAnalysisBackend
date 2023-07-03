using GraduationProjectBackend.DataAccess.Models.Member;
using System.Linq.Expressions;

namespace GraduationProjectBackend.DataAccess.Repositories
{
    public interface IRepository<Model>
    {
        Task<Model?> GetByID(int id);
        Task<Model?> GetByCondition(Expression<Func<Model, bool>> condition);
        Task<List<Model>> GetAllByCondition(Expression<Func<Model, bool>> condition);
        Task Add(Model model);
        Task Update(Model model);
        Task DeleteById(int id);
        Task DeleteByEntity(Model model);




    }
}
