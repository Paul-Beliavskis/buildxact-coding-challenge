using System.Collections.Generic;

namespace SuppliesPriceLister.Persistence.Entities
{
    public class Partner
    {
        public string Name { get; set; }

        public string PartnerType { get; set; }

        public string PartnerAddress { get; set; }

        public List<PartnerSupply> Supplies { get; set; }
    }
}
