using System.Net;
using FluentResults;

namespace InterviewTrainer.Application.Implementations.Errors;

public class QuestionErrors
{
    public static Error NoQuestionsFoundByFilter() =>
        new Error("Not a single question was found for the selected parameters.")
            .WithMetadata(nameof(HttpStatusCode), HttpStatusCode.NotFound)
            .WithMetadata("Title", "Not Found");
}