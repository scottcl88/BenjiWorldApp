using System;

namespace Repository.Models
{
    public class Health
    {
        public virtual long HealthId { get; set; }
        public virtual Dog Dog { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual decimal Height { get; set; }
        public virtual decimal Length { get; set; }
        public virtual decimal Waist { get; set; }
        public virtual DateTime? Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual DateTime? Deleted { get; set; }
    }
}