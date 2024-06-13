using System.Text.Json;
using System.Text;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels;

namespace WebApi.EndPoints
{
    public static class OrderEndPoint
    {

        public static void AddOrderEndPoints(this WebApplication app)
        {
            app.MapGet("/order/getall", GetAll);
            app.MapPost("/order/new", New);
            app.MapDelete("/order/delete", Delete);


        }

        private static IResult GetAll(List<Order> orders)
        {
            var orderViewModel = new List<OrderViewModel>();
            for (int i = 0; i < orders.Count; i++)
            {
                orderViewModel.Add(new OrderViewModel { Id = i, Goods = orders[i].Goods.Select(x => x.Name).ToList() });
            }

            return Results.Content(JsonSerializer.Serialize(orderViewModel), "application/json", Encoding.UTF8);
        }

        private static IResult New(List<Order> orders, List<Good> goods, [FromBody] int[] goodIds)
        {

            var notFound = new List<int>();
            var listofGoods = new List<Good>();

            for (int i = 0; i < goodIds.Length; i++)
            {
                var id = goodIds[i];

                if (id >= goods.Count)
                {
                    notFound.Add(id);
                    continue;
                }

                listofGoods.Add(goods[id]);
            }

            if (listofGoods.Count != 0)
            {
                orders.Add(new Order { Goods = listofGoods });
            }

            var answer = new OrderAddAnswerViewModel { Rusult = "Ok", FailGoodsId = notFound };

            return Results.Content(JsonSerializer.Serialize(answer), "application/json", Encoding.UTF8);
        }

        private static IResult Delete(List<Order> orders, int id)
        {
            string result;

            if (id >= orders.Count)
            {
                result = "Id is not found";

            }
            else
            {
                orders.Remove(orders[id]);
                result = "Ok";
            }

            return Results.Content(JsonSerializer.Serialize(result), "application/json", Encoding.UTF8);
        }

    }
}
