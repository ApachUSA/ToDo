using ToDo.Models;
using ToDo.Repository.Interfaces;

namespace ToDo.Services.Interfaces
{
	/// <summary>
	/// Interface for task management service.
	/// </summary>
	public interface ITaskService
	{
		/// <summary>
		/// Asynchronously retrieves a list of tasks.
		/// </summary>
		/// <returns>List of tasks.</returns>
		Task<List<ToDoTask>?> GetAsync();

		/// <summary>
		/// Asynchronously retrieves a specific task by its ID.
		/// </summary>
		/// <param name="id">ID of the task to retrieve.</param>
		/// <returns>The task with the specified ID.</returns>
		Task<ToDoTask?> GetAsync(Guid id);

		/// <summary>
		/// Asynchronously creates a new task.
		/// </summary>
		/// <param name="toDoTask">Task to be created.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		Task CreateAsync(ToDoTask toDoTask);

		/// <summary>
		/// Asynchronously updates an existing task.
		/// </summary>
		/// <param name="toDoTask">Task to be updated.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		Task UpdateAsync(ToDoTask toDoTask);

		/// <summary>
		/// Asynchronously marks a task as finished.
		/// </summary>
		/// <param name="id">ID of the task to be marked as finished.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		Task FinishAsync(Guid id);

		/// <summary>
		/// Asynchronously marks a finished task as unfinished.
		/// </summary>
		/// <param name="id">ID of the task to be marked as unfinished.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		Task UnFinishAsync(Guid id);

		/// <summary>
		/// Asynchronously deletes a task by its ID.
		/// </summary>
		/// <param name="id">ID of the task to be deleted.</param>
		/// <returns>Task representing the asynchronous operation.</returns>
		Task DeleteAsync(Guid id);
	}

}
