using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
	/// <summary>
	/// Represents a ToDo task entity.
	/// </summary>
	public class ToDoTask : EntityBase
	{
		/// <summary>
		/// Gets or sets the name of the task.
		/// </summary>
		[Required(ErrorMessage = "Enter a task name")]
		[MaxLength(200, ErrorMessage = "Maximum number of characters is 200")]
		public required string Task_Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the task is finished.
		/// </summary>
		public bool Finished { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the task was finished or created.
		/// </summary>
		public DateTime? Task_Date { get; set; }
	}

}
