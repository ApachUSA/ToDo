using Azure;
using System.Net;
using ToDo.Models.Errors;

namespace ToDo.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly RequestDelegate _next;

		/// <summary>
		/// Initializes a new instance of the ExceptionMiddleware class.
		/// </summary>
		/// <param name="logger">Logger for logging exceptions.</param>
		/// <param name="next">Delegate representing the next middleware in the pipeline.</param>
		public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next) 
		{
			_logger = logger;
			_next = next;
		}

		/// <summary>
		/// Invokes the middleware to handle exceptions during the request processing.
		/// </summary>
		/// <param name="context">HttpContext representing the current request and response.</param>
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				
				await HandleExceptionAsync(context, ex);
			}
		}

		/// <summary>
		/// Handles the exception by setting the response status code and providing an appropriate error message.
		/// </summary>
		/// <param name="context">HttpContext representing the current request and response.</param>
		/// <param name="ex">The exception that occurred.</param>
		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			string responseMessage = ex switch
			{
				NotFoundException => ex.Message,
				_ => "Internal server error"
			};

			await context.Response.WriteAsJsonAsync(responseMessage);
		}
	}
}
