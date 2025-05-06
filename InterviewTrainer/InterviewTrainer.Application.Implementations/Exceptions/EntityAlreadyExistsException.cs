namespace InterviewTrainer.Application.Implementations.Exceptions;

public class EntityAlreadyExistsException(string? message) : Exception(message);