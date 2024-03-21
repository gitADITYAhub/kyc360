using System.Collections.Generic;

namespace KYC360Api.Models
{
    public class Entity : IEntity
    {
        public List<Address> Addresses { get; set; }
        public List<Date> Dates { get; set; }
        public bool Deceased { get; set; }
        public string Gender { get; set; }
        public string Id { get; set; }
        public List<Name> Names { get; set; }

        public Entity()
        {
            Addresses = new List<Address>();
            Dates = new List<Date>();
            Names = new List<Name>();
        }
    }
}
