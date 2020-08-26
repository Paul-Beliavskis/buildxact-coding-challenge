using MediatR;
using SuppliesPriceLister.Application.Enums;
using SuppliesPriceLister.Application.HandlerResponses;

namespace SuppliesPriceLister.Application.HandlerRequests
{
    public class GetBuildingSuppliesRequest : IRequest<GetBuildingSuppliesResponse>
    {
        public SortOrderEnum SortOrder { get; set; }
    }
}