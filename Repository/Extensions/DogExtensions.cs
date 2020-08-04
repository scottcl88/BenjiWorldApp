using Models;
using Repository.Models;

namespace Repository
{
    public static class DogExtensions
    {
        public static DogModel ToDogModel(this Dog dbDog)
        {
            return new DogModel()
            {
                DogId = new DogId() { Value = dbDog.DogId },
                Name = dbDog.Name,
                Birthdate = dbDog.Birthdate,
                AdoptedDate = dbDog.AdoptedDate,
                Gender = (global::Models.Gender)dbDog.Gender,
                Created = dbDog.Created,
                Modified = dbDog.Modified,
                Deleted = dbDog.Deleted
            };
        }

        public static HealthModel ToHealthModel(this Health dbHealth)
        {
            return new HealthModel()
            {
                Dog = dbHealth.Dog.ToDogModel(),
                Created = dbHealth.Created,
                Modified = dbHealth.Modified,
                Deleted = dbHealth.Deleted
            };
        }
    }
}
