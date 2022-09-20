using System;

namespace ForestInteractiveTestApp.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.DeletedOn = Convert.ToDateTime("01-01-1900");
            this.InsertedOn = Convert.ToDateTime("01-01-1900");
            this.UpdatedOn = Convert.ToDateTime("01-01-1900");
        }

        public DateTime DeletedOn { get; set; }
        public DateTime InsertedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
