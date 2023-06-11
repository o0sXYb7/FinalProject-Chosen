using chosen.Models;
using System.ComponentModel.DataAnnotations;

namespace chosen.Models
{
    public class CommodityQuantityValidator : ValidationAttribute
    {
        private readonly string _tempStorageIdPropertyName;

        public CommodityQuantityValidator(string tempStorageIdPropertyName)
        {
            _tempStorageIdPropertyName = tempStorageIdPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var commodity = (Commodity)validationContext.ObjectInstance;
            var tempStorageId = (int)commodity.GetType().GetProperty(_tempStorageIdPropertyName).GetValue(commodity);

            using (var dbContext = new FinalProjectContext())
            {
                var TempStorageNow = dbContext.TempStorages.FirstOrDefault(t => t.TempStorageId == tempStorageId);

                var commodityNow = dbContext.Commodities.FirstOrDefault(t => t.TempStorageId == tempStorageId) ?? null;

                var totalQuantity = TempStorageNow.PrizeQuantity;

                if (commodityNow != null)
                {
                    totalQuantity += (int)commodityNow.CommodityQuantity;
                }

                if (totalQuantity < commodity.CommodityQuantity)
                {
                    return new ValidationResult($"最大可上架數量為：{totalQuantity}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
