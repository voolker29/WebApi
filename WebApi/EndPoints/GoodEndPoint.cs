using System.Text.Json;
using System.Text;
using WebApi.Models;

namespace WebApi.EndPoints
{
    public static class GoodEndPoint
    {

        public static void AddGoodEndPoints(this WebApplication app)
        {
            app.MapGet("/goods/getall", GetAll);
            app.MapGet("/goods/new", New);
            app.MapDelete("/goods/delete", Delete);


        }

        private static IResult GetAll(List<Good> goods)
        {
            return Results.Content(JsonSerializer.Serialize(goods), "application/json", Encoding.UTF8);
        }

        private static IResult New(List<Good> goods, string name)
        {
            goods.Add(new Good() { Name = name });
            return Results.Content(JsonSerializer.Serialize("Ok"), "application/json", Encoding.UTF8);
        }

        private static IResult Delete(List<Good> goods, List<Order> orders, int id)
        {

            string result;

            if (id >= goods.Count)
            {
                result = "Id is not found";

            }
            else
            {
                var item = goods[id];
                orders.ForEach(o => o.Goods = o.Goods.Where(x => x.Name != item.Name).ToList());
                goods.Remove(item);
                result = "Ok";
            }

            return Results.Content(JsonSerializer.Serialize("Ok"), "application/json", Encoding.UTF8);
        }



    }
}
