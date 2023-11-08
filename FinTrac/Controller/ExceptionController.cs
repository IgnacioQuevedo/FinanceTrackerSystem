namespace Controller;

public class ExceptionController : Exception
{
    public ExceptionController(string message) :base(message){}
}