namespace MyEdo.Business.Exceptions
{
    public class NotFoundException : MyEdoException
    {
        public override string Message => "Not found";

        public override string Source => "2";
    }
}
