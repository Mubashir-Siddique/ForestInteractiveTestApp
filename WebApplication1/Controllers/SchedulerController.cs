using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ForestInteractiveTestApp.Core;
using ForestInteractiveTestApp.IRepository;
using ForestInteractiveTestApp.Models;

namespace ForestInteractiveTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        ScheduleCore SchedulerCoreCore;
        public SchedulerController(IRepository<Schedule> irepository)
        {
            SchedulerCoreCore = new ScheduleCore(irepository);
        }

        [HttpPost]
        [Route("GetScheduleList")]
        public APIResponse GetSchedulesList(ScheduleRequestModel model)
        {
            return SchedulerCoreCore.GetSchedulesList(model);
        }

        [HttpPost]
        [Route("GetScheduleSMSList")]
        public APIResponse GetSMSScheduleList(Schedule model)
        {
            return SchedulerCoreCore.GetSMSScheduleList(model);
        }


        [HttpPost]
        [Route("CreateUpdateSchedule")]
        public APIResponse CreateUpdateSchedule(Schedule model)
        {
            return SchedulerCoreCore.CreateUpdateSchedule(model);
        }

        [HttpGet]
        [Route("SendScheduleSMS")]
        public IActionResult SendScheduleSMS()
        {
            return Ok(SchedulerCoreCore.SendScheduleSMS(DateTime.Now)); 
        }        
        
        [HttpGet]
        [Route("CheckSMSStatus/{messageId}")]
        public IActionResult CheckSMSStatus(string messageId)
        {
            return Ok(SchedulerCoreCore.CheckSMSStatus(messageId)); 
        }
    }
}
