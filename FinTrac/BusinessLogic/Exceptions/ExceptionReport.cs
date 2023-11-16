namespace BusinessLogic.Exceptions;

public class ExceptionReport : Exception
{
    public ExceptionReport(string message) : base(message) { }
}