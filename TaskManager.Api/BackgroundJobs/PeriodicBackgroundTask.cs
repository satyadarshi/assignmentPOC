using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using TaskManager.Application.Common.Email;
using TaskManager.Data.Context;
using TaskManager.Data.Repositories;

namespace TaskManager.Api.BackgroundJobs
{
    public class PeriodicBackgroundTask : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromDays(15);
        private readonly ILogger<PeriodicBackgroundTask> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public PeriodicBackgroundTask(ILogger<PeriodicBackgroundTask> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _scopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);

            while (!stoppingToken.IsCancellationRequested &&
                   await timer.WaitForNextTickAsync(stoppingToken))
            {
               
                using IServiceScope scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ITaskRepository>();
                var email = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var all = repository.GetTasksAsync();
               
                foreach (var _task in all.Result.Where(x=>x.IsMailSent==false))
                {
                     bool result= await email.SendMail(_task.TaskName, _task.AssignedTo);
                    if (true)
                    {
                        _logger.LogInformation("Successfully Email Triggered for " + _task.TaskName);
                        var task = await repository.GetTaskByIdAsync(_task.Id);
                        task.IsMailSent = true;
                        await repository.UpdateTaskAsync(task);
                    }
                   
                }

            }
        }
    }
}
