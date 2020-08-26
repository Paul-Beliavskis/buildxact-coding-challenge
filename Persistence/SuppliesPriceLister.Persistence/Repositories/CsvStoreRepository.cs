using System.Collections.Generic;
using System.IO;
using SuppliesPriceLister.Persistence.Entities;
using SuppliesPriceLister.Persistence.Interfaces;

namespace SuppliesPriceLister.Persistence.Repositories
{
    public class CsvStoreRepository : ICsvStoreRepository
    {
        public List<HumphriesSupply> GetHumphriesSupplyList()
        {
            var HumphriesSupplyList = new List<HumphriesSupply>();

            //Read one line at a time to ensure memory is not overloaded 
            using (StreamReader sr = new StreamReader("./Database/humphries.csv"))
            {
                sr.ReadLine();

                while (sr.Peek() >= 0)
                {
                    var line = sr.ReadLine().Split(',');

                    var humphriesSupply = new HumphriesSupply()
                    {
                        HumphriesSupplyId = line[0],
                        Description = line[1],
                        Unit = line[2],
                        CostInAUD = double.Parse(line[3])
                    };

                    HumphriesSupplyList.Add(humphriesSupply);
                }
            }

            return HumphriesSupplyList;
        }
    }
}
