using DataExtensions;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class HealthManager
    {
        private readonly HealthRepository _healthRepository;

        public HealthManager(HealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }

        public List<HealthModel> GetAllHealth(DogModel dogModel)
        {
            return _healthRepository.GetAllHealthForDog(dogModel.DogId).ToList();
        }

        public bool CreateHealth(HealthCreateRequest request)
        {
            return _healthRepository.CreateHealth(request.Dog.DogId);
        }

        public bool UpdateHealth(HealthUpdateRequest request)
        {
            return _healthRepository.UpdateHealth(request.Health);
        }
    }
}