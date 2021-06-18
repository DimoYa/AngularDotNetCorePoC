namespace MyEdo.Business.Exceptions
{
    using System;
    public class MyEdoException : Exception
    {
        public string GenerateApiError()
        {
            return $"{{id: {this.Source}, error: \"{this.Message}\" }}";
        }
    }
}
