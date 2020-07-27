using FluentNHibernate.Mapping;
using Repository.Models;
using NHibernate;

namespace Repository.Mappings
{
    public class HealthMap : ClassMap<Health>
    {
        public HealthMap()
        {
            Table("Health");
            Id(x => x.HealthId);
            Map(x => x.Waist);
            Map(x => x.Length);
            Map(x => x.Height);
            Map(x => x.Weight);
            Map(x => x.Created);
            Map(x => x.Modified);
            Map(x => x.Deleted);
            References(x => x.Dog);
        }
    }
}
