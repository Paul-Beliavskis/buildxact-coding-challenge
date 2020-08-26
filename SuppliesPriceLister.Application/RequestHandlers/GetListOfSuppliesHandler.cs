using System;
using System.Collections.Generic;
using System.IO;
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
                        Price = ConvertToAUD(supply.PriceInCents)
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

            IEnumerable<Supply> sortedSupplyList = new List<Supply>();

            if (request.SortOrder == Enums.SortOrderEnum.Desc)
            {
                sortedSupplyList = from supply in supplyList
                                   orderby supply.Price descending
                                   select supply;
            }

            var response = new GetBuildingSuppliesResponse
            {
                Supplies = sortedSupplyList
            };

            return Task.FromResult(response);
        }

        private double ConvertToAUD(int usCents)
        {
            var configFile = File.ReadAllText("./appsettings.json");

            var exchangeRateModel = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrencyExchangeModel>(configFile);

            var aud = usCents / 100 * exchangeRateModel.AudUsdExchangeRate;

            var roundedAud = Math.Round(aud, 2);

            return roundedAud;
        }
    }
}
