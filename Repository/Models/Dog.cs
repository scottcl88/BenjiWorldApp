using System;

namespace Repository.Models
{
    public class Dog
    {
        public virtual long DogId { get; set; }
        public virtual string Name { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual DateTime? Birthdate { get; set; }
        public virtual DateTime? AdoptedDate { get; set; }
        public virtual DateTime? Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual DateTime? Deleted { get; set; }
    }

    public enum Gender
    {
        Unknown = 0,
        Male = 1,
        Female = 2,
        Other = 3
    }
}