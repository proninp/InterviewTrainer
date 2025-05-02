namespace InterviewTrainer.Application.Exceptions;

public class BusinessRuleViolationException(string? message) : Exception(message);