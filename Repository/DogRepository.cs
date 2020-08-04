using Models;
using NHibernate;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class DogRepository : IRepository
    {
        public List<DogModel> GetAllDogs()
        {
            List<DogModel> dogModels = new List<DogModel>();
            using (ISession session = NHibernateSession.OpenSession())  // Open a session to conect to the database
            {
                var dogs = session.Query<Dog>().Where(c => !c.Deleted.HasValue);
                dogModels = dogs.Select(x => x.ToDogModel()).ToList(); //  Querying to get all the users
            }
            return dogModels;
        }

        public bool CreateDog(string name)
        {
            Dog newDog = new Dog
            {
                Name = name,
                Created = DateTime.UtcNow
            };
            using (ISession session = NHibernateSession.OpenSession())
            {
                //User foundUser = session.Query<User>().FirstOrDefault(u => u.HashedLoginToken == hashedLoginToken);
                //if (foundUser == null) return false;
                //newDog.User = foundUser;
                //newCar.UserId = foundUser.UserId;
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Save(newDog); //  Save the user in session
                    transaction.Commit();   //  Commit the changes to the database
                }
            }
            return true;
        }

        public bool UpdateDog(DogModel dogModel)
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                Dog foundDog = session.Query<Dog>().FirstOrDefault(c => c.DogId == dogModel.DogId.Value);
                if (foundDog == null) return false;
                foundDog.Name = dogModel.Name;
                foundDog.AdoptedDate = dogModel.AdoptedDate;
                foundDog.Birthdate = dogModel.Birthdate;
                foundDog.Gender = (Models.Gender)dogModel.Gender;
                foundDog.Modified = DateTime.UtcNow;
                using (ITransaction transaction = session.BeginTransaction())   //  Begin a transaction
                {
                    session.Update(foundDog); //  Save the user in session
                    transaction.Commit();   //  Commit the changes to the database
                }
            }
            return true;
        }
    }
}