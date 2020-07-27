using Models;
using Repository.Models;

namespace Repository
{
    public static class DogExtensions
    {
        public static DogModel ToCarModel(this Dog dbDog)
        {
            return new DogModel()
            {
                DogId = new DogId() { Value = dbDog.DogId },
                Name = dbDog.Name,
                Birthdate = dbDog.Birthdate,
                AdoptedDate = dbDog.AdoptedDate,
                Created = dbDog.Created,
                Modified = dbDog.Modified,
                Deleted = dbDog.Deleted
            };
        }
    }
}
