using Quartz;
using System;
using System.Threading.Tasks;
using ForestInteractiveTestApp.Core;
using ForestInteractiveTestApp.IRepository;
using ForestInteractiveTestApp.Models;

namespace ForestInteractiveTestApp.Scheduler
{
    public class SMSScheduler : IJob
    {
        ScheduleCore job;

        public SMSScheduler(IRepository<Schedule> irepository)
        {
            job = new ScheduleCore(irepository);
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await job.SendScheduleSMS(DateTime.Now);
            return;
        }
    }
}
