using MediatR;
using SuppliesPriceLister.Application.Enums;

namespace buildxact_supplies
{
    public class GetBuildingSuppliesRequest : IRequest<GetBuildingSuppliesResponse>
    {
        public SortOrderEnum SortOrder { get; set; }
    }
}