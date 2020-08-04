using DataExtensions;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class DogManager
    {
        private readonly DogRepository _dogRepository;

        public DogManager()
        {
            _dogRepository = new DogRepository();
        }

        public List<DogModel> GetAllDogs()
        {
            return _dogRepository.GetAllDogs().ToList();
        }
        public DogModel GetDogById(DogId dogId)
        {
            return _dogRepository.GetDogById(dogId);
        }

        public bool CreateNewDog(DogCreateRequest request)
        {
            return _dogRepository.CreateDog(request.Dog.Name);
        }

        public bool UpdateDog(DogUpdateRequest request)
        {
            return _dogRepository.UpdateDog(request.Dog);
        }
    }
}