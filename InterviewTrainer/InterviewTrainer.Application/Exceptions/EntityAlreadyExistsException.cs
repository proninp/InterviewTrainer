namespace InterviewTrainer.Application.Exceptions;

public class EntityAlreadyExistsException(string? message) : Exception(message);