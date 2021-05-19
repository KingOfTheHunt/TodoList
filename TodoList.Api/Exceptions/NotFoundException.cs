using System;

namespace TodoList.Api.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
