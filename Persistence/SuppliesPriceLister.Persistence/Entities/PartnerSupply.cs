namespace SuppliesPriceLister.Persistence.Entities
{
    public class PartnerSupply
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Uom { get; set; }

        public int PriceInCents { get; set; }

        public System.Guid ProviderId { get; set; }

        public string MaterialType { get; set; }
    }
}