using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForestInteractiveTestApp.Controllers;
using ForestInteractiveTestApp.Models;
using static ForestInteractiveTestApp.Common.Constant;

namespace NUnitCore
{
    public  class ScheduleTest : BaseTest
    {
        SchedulerController scheduleController;

        public ScheduleTest() : base((int)TestType.ScheduleTest)
        {
            scheduleController = new SchedulerController(_iSchedule); 
        }

        [Test]
        public void CreateSchedule()
        {
            Schedule model = new Schedule()
            {
                ScheduleId = 5,
                Time = DateTime.Now,
                Recepients = new List<Recepient>()
                {
                    new Recepient()
                    {
                        Name="Test",
                        PhoneNumber="000000000",
                        IsSent = false,
                        MessageId = string.Empty
                    }
                },
                Message = "Test Message",
                IsDone = false,
                IsActive = true
            };

            var response = scheduleController.CreateUpdateSchedule(model);
            Assert.AreEqual(0, response.StatusCode);
        }

        [Test]
        public void GetSchedulesList()
        {
            ScheduleRequestModel model = new ScheduleRequestModel()
            {
                Status = true,
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(1)
            };

            var response = scheduleController.GetSchedulesList(model);
            Assert.AreEqual(0, response.StatusCode);
        }
        
        [Test]
        public void CheckSMSStatus()
        {
            string messageId = string.Format("91d1ce61-b7a2-493d-ad71-c22e8883ec6d");

            var response = scheduleController.CheckSMSStatus(messageId);
            Assert.AreEqual(0, Ok(response));
        }
    }
}
