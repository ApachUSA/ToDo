using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using ToDo.Models.Errors;
using ToDo.Repository.Interfaces;
using ToDo.Services.Interfaces;

namespace ToDo.Services.Implementations
{
	public class TaskService : ITaskService
	{
		private readonly IBaseRepository<ToDoTask> _repository;

		public TaskService(IBaseRepository<ToDoTask> repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Asynchronously creates a new task.
		/// </summary>
		/// <param name="toDoTask">Task to be created.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		public async Task CreateAsync(ToDoTask toDoTask)
		{
			SetTime(toDoTask);
			await _repository.CreateAsync(toDoTask);
		}

		/// <summary>
		/// Asynchronously deletes a task by its ID.
		/// </summary>
		/// <param name="id">ID of the task to be deleted.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		public async Task DeleteAsync(Guid id)
		{
			var toDoTask = await GetAsync(id);

			await _repository.DeleteAsync(toDoTask!);
		}

		/// <summary>
		/// Asynchronously marks a task as finished.
		/// </summary>
		/// <param name="id">ID of the task to be marked as finished.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		public async Task FinishAsync(Guid id)
		{
			var toDoTask = await GetAsync(id);

			SetTime(toDoTask!);
			toDoTask!.Finished = true;

			await UpdateAsync(toDoTask);
		}

		/// <summary>
		/// Asynchronously marks a finished task as unfinished.
		/// </summary>
		/// <param name="id">ID of the task to be marked as unfinished.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		public async Task UnFinishAsync(Guid id)
		{
			var toDoTask = await GetAsync(id);

			SetTime(toDoTask!);
			toDoTask!.Finished = false;

			await UpdateAsync(toDoTask);
		}

		/// <summary>
		/// Asynchronously retrieves a list of tasks.
		/// </summary>
		/// <returns>List of tasks.</returns>
		public async Task<List<ToDoTask>?> GetAsync()
		{
			return await _repository.GetAll().ToListAsync() ?? throw new NotFoundException();
		}

		/// <summary>
		/// Asynchronously retrieves a specific task by its ID.
		/// </summary>
		/// <param name="id">ID of the task to retrieve.</param>
		/// <returns>The task with the specified ID.</returns>
		public async Task<ToDoTask?> GetAsync(Guid id)
		{
			return await _repository.GetByIdAsync(id) ?? throw new NotFoundException(id);
		}

		/// <summary>
		/// Asynchronously updates an existing task.
		/// </summary>
		/// <param name="toDoTask">Task to be updated.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		public async Task UpdateAsync(ToDoTask toDoTask)
		{
			await _repository.UpdateAsync(toDoTask);
		}

		/// <summary>
		/// Sets the finish date of a ToDoTask to the current date and time.
		/// </summary>
		/// <param name="task">The ToDoTask for which to set the finish date.</param>
		private static void SetTime(ToDoTask task) => task.Task_Date = DateTime.Now;

	}
}
