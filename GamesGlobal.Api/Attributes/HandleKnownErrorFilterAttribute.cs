using System.Net;
using GamesGlobal.Core.Exceptions;
using GamesGlobal.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamesGlobal.Api.Attributes;

public class HandleKnownErrorFilterAttribute : ExceptionFilterAttribute
{
    #region Overrides of ExceptionFilterAttribute

	public override void OnException(ExceptionContext context)
	{
		var knownException = new[]
			{ typeof(ArgumentException), typeof(ArgumentNullException), typeof(ArgumentOutOfRangeException) };

		var ex = context.Exception;
		if (knownException.Contains(ex.GetType()))
		{
			var response = Create(ex);
			
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
			context.Result = new JsonResult(response);
			
			WarnAboutException(context, ex);
			return;
		}
		
		var isApiException = ex is ApiObjectException;
		if (isApiException)
		{
			var aex = ex as ApiObjectException;
			var response = Create(ex, aex.Value.ToString());
			
			context.HttpContext.Response.StatusCode = (int)aex.StatusCode;
			context.Result = new JsonResult(response);
			
			WarnAboutException(context, ex);
			return;
		}
		var error = Create(ex, "Internal error has occured. Please contact the 22seven support team for assistance.");
			
		context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		context.Result = new JsonResult(error);
	}

	#endregion

	private ErrorResult Create(Exception ex) => ErrorResult.Wrap(ex.Message, ex);
	
	private ErrorResult Create(Exception ex, string? message) => ErrorResult.Wrap(message ?? ex.Message, ex);

	private ErrorResult Create(string message) => ErrorResult.Wrap(message, null);
	
	private static void WarnAboutException(ExceptionContext context, Exception exception)
	{
		//TODO: Log exception
	}
}