﻿using Machine.Specifications;
using ShopifySharp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifySharp.Tests.ShopifyTransactionService_Tests
{
    [Subject(typeof(ShopifyTransactionService))]
    class When_listing_transactions
    {
        Establish context = () =>
        {
            Service = new ShopifyTransactionService(Utils.MyShopifyUrl, Utils.AccessToken);
            Order = Setup.CreateOrder().Await().AsTask.Result;

            for (var i = 0; i < 2; i++)
            {
                Service
                .CreateAsync(Order.Id.Value, Setup.GenerateTransaction("capture", 5.00))
                .Await();
            }
        };

        Because of = () =>
        {
            Result = Service
                .ListAsync(Order.Id.Value)
                .Await()
                .AsTask
                .Result;
        };

        It should_list_transactions = () =>
        {
            Result.Count().ShouldBeGreaterThanOrEqualTo(2);
        };

        Cleanup after = () =>
        {
            Setup.DeleteOrder(Order.Id.Value).Await();
        };

        static ShopifyTransactionService Service;

        static ShopifyOrder Order;

        static IEnumerable<ShopifyTransaction> Result;
    }
}
