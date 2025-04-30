﻿namespace InterviewTrainer.Application.Contracts.Repositories;

public interface IUnitOfWork
{
    Task<int> CommitAsync();
}