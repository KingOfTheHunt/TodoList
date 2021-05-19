using System;

namespace TodoList.Api.Exceptions
{
    public class DbUpdateException : ApplicationException
    {
        public DbUpdateException(string message)
            : base(message)
        {
        }
    }
}
