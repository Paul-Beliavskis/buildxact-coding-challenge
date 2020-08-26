using System.Collections.Generic;
using SuppliesPriceLister.Persistence.Entities;

namespace SuppliesPriceLister.Persistence.Interfaces
{
    public interface IJsonStoreRepository
    {
        List<Partner> GetPartners();
    }
}
