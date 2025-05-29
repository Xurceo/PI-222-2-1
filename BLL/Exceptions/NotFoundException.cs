using System;

namespace BLL.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(object entity)
            : base($"Entity of type '{entity?.GetType().Name ?? "Unknown"}' not found.")
        {
        }

        public NotFoundException(Type entityType)
            : base($"Entity of type '{entityType?.Name ?? "Unknown"}' not found.")
        {
        }
    }
}