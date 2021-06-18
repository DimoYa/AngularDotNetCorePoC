namespace MyEdo.Business.Exceptions
{
    public class BadRequestException : MyEdoException
    {
        public override string Message => "Bad request";

        public override string Source => "3";
    }
}
