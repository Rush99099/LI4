namespace BMW.Data.Data
{
    public class DAOException : Exception
    {
        public DAOException() { }

        public DAOException(string message)
            : base(message)
        { }

        public DAOException(string? message, Exception? innerException)
            : base(message, innerException)
        { }
    }
}