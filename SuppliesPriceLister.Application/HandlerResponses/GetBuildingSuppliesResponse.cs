using System.Collections.Generic;
using SuppliesPriceLister.Domain.Models;

namespace SuppliesPriceLister.Application.HandlerResponses
{
    public class GetBuildingSuppliesResponse
    {
        public IEnumerable<Supply> Supplies { get; set; }
    }
}