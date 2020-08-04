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
            Id(x => x.DogId).Not.Nullable();
            Map(x => x.Name).Not.Nullable();            
            Map(x => x.Gender).CustomType<Gender>().Not.Nullable();
            Map(x => x.Birthdate).Nullable();
            Map(x => x.AdoptedDate).Nullable();
            Map(x => x.Created).Not.Nullable();
            Map(x => x.Modified).Not.Nullable();
            Map(x => x.Deleted).Nullable();
        }
    }
}
