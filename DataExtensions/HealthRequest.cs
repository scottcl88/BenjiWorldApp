using Models;
using Repository.Models;

namespace DataExtensions
{
    public class HealthCreateRequest
    {
        public DogModel Dog { get; set; }
    }

    public class HealthUpdateRequest
    {
        public HealthModel Health { get; set; }
    }
}