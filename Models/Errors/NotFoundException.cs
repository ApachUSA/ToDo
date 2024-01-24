namespace ToDo.Models.Errors
{
	public class NotFoundException : Exception
	{

		public NotFoundException(Guid id)
			: base($"Not found task with id {id}")
		{

		}

		public NotFoundException()
			: base($"Task list empty")
		{

		}
	}
}
