using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuppliesPriceLister.Application.HandlerRequests;
using SuppliesPriceLister.Application.HandlerResponses;
using SuppliesPriceLister.Domain.Models;
using SuppliesPriceLister.Persistence.Repositories;

namespace SuppliesPriceLister.Application.RequestHandlers
{
    public class GetListOfSuppliesHandler : IRequestHandler<GetBuildingSuppliesRequest, GetBuildingSuppliesResponse>
    {
        public Task<GetBuildingSuppliesResponse> Handle(GetBuildingSuppliesRequest request, CancellationToken cancellationToken)
        {
            //Normally I would use DI here but to save time will do a dodgy
            var jsonRepository = new JsonStoreRepository();
            var csvStoreRepository = new CsvStoreRepository();

            var partners = jsonRepository.GetPartners();

            var supplyList = new List<Supply>();
            foreach (var partner in partners)
            {
                foreach (var supply in partner.Supplies)
                {
                    var supplyModel = new Supply
                    {
                        SupplyId = supply.Id.ToString(),
                        ItemName = supply.MaterialType,
                        Price = supply.PriceInCents
                    };

                    supplyList.Add(supplyModel);
                }

            }

            var humphriesSupplyList = csvStoreRepository.GetHumphriesSupplyList()
            .Select(x => new Supply
            {
                ItemName = x.Description,
                SupplyId = x.HumphriesSupplyId,
                Price = x.CostInAUD
            });

            supplyList.AddRange(humphriesSupplyList);

            IEnumerable<Supply> sortedSupplyList;

            if (request.SortOrder == Enums.SortOrderEnum.Desc)
            {
                sortedSupplyList = supplyList.OrderByDescending(i => i);
            }

            var response = new GetBuildingSuppliesResponse
            {
                Supplies = supplyList
            };

            return Task.FromResult(response);
        }
    }
}
