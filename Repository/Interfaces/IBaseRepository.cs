using ToDo.Models;

namespace ToDo.Repository.Interfaces
{
	public interface IBaseRepository<T> where T : EntityBase
	{
		Task CreateAsync(T entity);

		Task DeleteAsync(T entity);

		IQueryable<T> GetAll();

		Task<T?> GetByIdAsync(Guid id);

		Task UpdateAsync(T entity);
	}
}
