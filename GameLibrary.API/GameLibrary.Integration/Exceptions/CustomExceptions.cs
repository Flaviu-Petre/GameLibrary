namespace GameLibrary.Integration.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message) { }
    public EntityNotFoundException(string entityName, int id) 
        : base($"{entityName} with ID {id} was not found.") { }
}

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}

public class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message) { }
    public DatabaseException(string message, Exception innerException) 
        : base(message, innerException) { }
}
