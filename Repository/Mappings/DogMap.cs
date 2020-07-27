using FluentNHibernate.Mapping;
using Repository.Models;
using NHibernate;

namespace Repository.Mappings
{
    public class DogMap : ClassMap<Dog>
    {
        public DogMap()
        {
            Table("Dog");
            Id(x => x.DogId);
            Map(x => x.Name);
            Map(x => x.Gender);
            Map(x => x.Birthdate);
            Map(x => x.AdoptedDate);
            Map(x => x.Created);
            Map(x => x.Modified);
            Map(x => x.Deleted);
        }
    }
}
