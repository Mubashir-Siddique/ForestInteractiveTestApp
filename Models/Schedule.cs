using System;
using System.Collections.Generic;

namespace ForestInteractiveTestApp.Models
{
    public class Schedule : BaseModel
    {
        public int ScheduleId { get; set; }
        public DateTime Time { get; set; }
        public List<Recepient> Recepients { get; set; }
        public string RecepientList { get; set; }
        public string Message { get; set; }
        public bool IsDone { get; set; }
        public bool IsActive { get; set; }
        public string MessageId { get; set; }

    }

}
