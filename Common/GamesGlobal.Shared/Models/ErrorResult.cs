namespace GamesGlobal.Shared.Models;

public class ErrorResult
{
    public string Error { get; private set; }
	
#if DEBUG
    public string Detail { get; set; }
    public string[] DetailStack { get; set; }
#endif
	
    private ErrorResult(string errorMessage)
    {
        Error = errorMessage;
    }

    public static ErrorResult Wrap(string errorMessage , Exception? exception)
    {
        var errorResult = new ErrorResult(errorMessage);
		
#if DEBUG
        if (exception == null) return errorResult;
        errorResult.Detail = exception.Message;
        errorResult.DetailStack = exception.StackTrace?.Split('\n').Select(x=>x.Trim()).ToArray();
#endif
		
        return errorResult;
    }
}