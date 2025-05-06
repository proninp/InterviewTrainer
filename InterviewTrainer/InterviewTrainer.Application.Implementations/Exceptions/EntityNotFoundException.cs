namespace InterviewTrainer.Application.Implementations.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base()
    {
    }

    public EntityNotFoundException(string type, Guid id) : base(
        $"Entity of type {type} with id {id} was not found in database")
    {
    }
}