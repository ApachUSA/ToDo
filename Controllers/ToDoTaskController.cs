using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Services.Interfaces;

namespace ToDo.Controllers
{
	/// <summary>
	/// Controller for managing ToDo tasks.
	/// </summary>
	public class ToDoTaskController : Controller
	{
		private readonly ITaskService _taskService;

		/// <summary>
		/// Constructor for ToDoTaskController.
		/// </summary>
		/// <param name="taskService">Task service dependency.</param>
		public ToDoTaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		/// <summary>
		/// Displays the main index view.
		/// </summary>
		/// <returns>Returns the main index view.</returns>
		[HttpGet]
		public IActionResult Index() => View();

		/// <summary>
		/// Gets a partial view of the task list with optional sorting.
		/// </summary>
		/// <param name="sortOrder">Sorting order (default is "By date in reverse").</param>
		/// <returns>Returns a partial view with the sorted task list.</returns>
		[HttpGet]
		public async Task<IActionResult> TaskListPartial(string? sortOrder = "By date in reverse")
		{
			var taskList = await _taskService.GetAsync();

			switch (sortOrder)
			{
				case "A-Z":
					taskList = taskList?.OrderBy(x => x.Task_Name).ToList();
					break;
				case "Z-A":
					taskList = taskList?.OrderByDescending(x => x.Task_Name).ToList();
					break;
				case "By date":
					taskList = taskList?.OrderBy(x => x.Task_Date).ToList();
					break;
				case "By date in reverse":
					taskList = taskList?.OrderByDescending(x => x.Task_Date).ToList();
					break;
			}

			return PartialView(taskList?.OrderBy(x => x.Finished).ToList());
		}

		/// <summary>
		/// Creates a new task.
		/// </summary>
		/// <param name="toDoTask">Task to be created.</param>
		/// <returns>Returns Ok with a message indicating task creation.</returns>
		[HttpPost]
		public async Task<IActionResult> CreateTask(ToDoTask toDoTask)
		{
			await _taskService.CreateAsync(toDoTask);
			return Ok("Task created");
		}

		/// <summary>
		/// Gets a partial view for updating a task.
		/// </summary>
		/// <param name="id">Task ID to be updated.</param>
		/// <returns>Returns a partial view for updating a task.</returns>
		[HttpGet]
		public async Task<IActionResult> UpdateTaskPartial(Guid id)
		{
			var task = await _taskService.GetAsync(id);
			return PartialView(task);
		}

		/// <summary>
		/// Updates an existing task.
		/// </summary>
		/// <param name="toDoTask">Task to be updated.</param>
		/// <returns>Returns Ok with a message indicating task update.</returns>
		[HttpPut]
		public async Task<IActionResult> UpdateTask(ToDoTask toDoTask)
		{
			await _taskService.UpdateAsync(toDoTask);
			return Ok("The task has been updated");
		}

		/// <summary>
		/// Marks a task as finished.
		/// </summary>
		/// <param name="id">Task ID to be marked as finished.</param>
		/// <returns>Returns Ok with a message indicating task completion.</returns>
		[HttpPut]
		public async Task<IActionResult> Finish(Guid id)
		{
			await _taskService.FinishAsync(id);
			return Ok("Task completed");
		}

		/// <summary>
		/// Marks a finished task as unfinished (returns to active state).
		/// </summary>
		/// <param name="id">Task ID to be marked as unfinished.</param>
		/// <returns>Returns Ok with a message indicating task reactivation.</returns>
		[HttpPut]
		public async Task<IActionResult> UnFinish(Guid id)
		{
			await _taskService.UnFinishAsync(id);
			return Ok("The task is returned to the active state");
		}

		/// <summary>
		/// Deletes a task.
		/// </summary>
		/// <param name="id">Task ID to be deleted.</param>
		/// <returns>Returns Ok with a message indicating task deletion.</returns>
		[HttpDelete]
		public async Task<IActionResult> DeleteTask(Guid id)
		{
			await _taskService.DeleteAsync(id);
			return Ok("Task deleted");
		}
	}

}
