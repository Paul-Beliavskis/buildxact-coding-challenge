using System.Collections.Generic;
using System.IO;
using SuppliesPriceLister.Persistence.Entities;
using SuppliesPriceLister.Persistence.Interfaces;

namespace SuppliesPriceLister.Persistence.Repositories
{
    public class JsonStoreRepository : IJsonStoreRepository
    {
        public List<Partner> GetPartners()
        {
            var data = File.ReadAllText("./Database/megacorp.json");

            var partnerList = Newtonsoft.Json.JsonConvert.DeserializeObject<PartnerList>(data);

            return partnerList.Partners;
        }
    }
}
