using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace PERSISTENCE.Repository
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class
    {
        
    }
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class
    {

    }
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T>, IReadRepositoryAsync<T> where T : class
    {
        public readonly Context dbContext;

        public MyRepositoryAsync(Context dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }        
    }
}
