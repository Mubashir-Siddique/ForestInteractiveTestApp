using System;

namespace ForestInteractiveTestApp.Models
{
    public class ScheduleRequestModel
    {
        public bool Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
