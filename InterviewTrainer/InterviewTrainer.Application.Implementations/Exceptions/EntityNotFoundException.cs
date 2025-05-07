namespace InterviewTrainer.Application.Implementations.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base()
    {
    }

    public EntityNotFoundException(string type, long id) : base(
        $"Entity of type {type} with id {id} was not found in database")
    {
    }
}