﻿using System.Linq;
using System.Threading.Tasks;

namespace ShopifySharp.Tests.ShopifyOrderRiskService_Tests
{
    static class RiskUtils
    {
        public static string Message = "This looks risky!";

        public static decimal Score = (decimal)0.85;

        public static string Recommendation = "cancel";

        public static string Source = "External";

        public static bool CauseCancel = false;

        public static bool Display = true;

        public static ShopifyOrderRisk CreateRisk()
        {
            return new ShopifyOrderRisk()
            {
                Message = Message,
                Score = Score,
                Recommendation = Recommendation,
                Source = Source,
                CauseCancel = CauseCancel,
                Display = Display,
            };
        }

        public static async Task<long> GetOrderId()
        {
            var service = new ShopifyOrderService(Utils.MyShopifyUrl, Utils.AccessToken);
            var orders = await service.ListAsync(new Filters.ShopifyOrderFilter()
            {
                Limit = 1,
                Fields = "id"
            });

            return orders.First().Id.Value;
        }
    }
}
