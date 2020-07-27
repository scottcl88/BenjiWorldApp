using Models;
using Repository.Models;

namespace DataExtensions
{
    public class DogCreateRequest
    {
        public DogModel Dog { get; set; }
    }

    public class DogUpdateRequest
    {
        public DogModel Dog { get; set; }
    }
}