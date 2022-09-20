using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using ForestInteractiveTestApp.IRepository;
using ForestInteractiveTestApp.Models;
using ForestInteractiveTestApp.Repository;

namespace NUnitCore
{
    [TestClass]
    public class BaseTest : ControllerBase
    {
        public IRepository<Schedule> _iSchedule;

        public BaseTest(int TestType)
        {
            var container = new UnityContainer();
            container.RegisterType<IConnection, ConnectionFactory>();
            container.Resolve<IConnection>();

            switch (TestType)
            {
                case (int)ForestInteractiveTestApp.Common.Constant.TestType.ScheduleTest:
                    container.RegisterType<IRepository<Schedule>, GenericRepository<Schedule>>();
                    _iSchedule = container.Resolve<IRepository<Schedule>>();
                    break;

                default:
                    break;
            }
        }
    }
}
