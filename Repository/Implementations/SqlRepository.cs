using ToDo.Models;
using ToDo.Repository.Interfaces;

namespace ToDo.Repository.Implementations
{
	public class SqlRepository<T> : IBaseRepository<T> where T : EntityBase
	{
		private readonly ToDo_DB_Context _dbContext;

		public SqlRepository(ToDo_DB_Context dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task CreateAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public IQueryable<T> GetAll()
		{
			return _dbContext.Set<T>();
		}

		public async Task<T?> GetByIdAsync(Guid id)
		{
			return await _dbContext.FindAsync<T>(id);
		}

		public async Task UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}
