namespace APIBooking.Domain.Exceptions
{
    public class SQLErrorException : DomainException
    {
        public SQLErrorException(string message) : base(message)
        {
            
        }
    }
}
