namespace DataManagers;

public class ExceptionController : Exception
{
    public ExceptionController(string message) :base(message){}
}