using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;

namespace WebApi
{
    public class Seed
    {
        public void Fill(IServiceProvider services) 
        {

            using (var scope = services.CreateScope())
            {
                FillGoods(scope.ServiceProvider);
                FillOrders(scope.ServiceProvider);
            }


        }


        private void FillGoods(IServiceProvider serviceProvider)
        {

            var goods = serviceProvider.GetService<List<Good>>();
            
            for (int i = 0; i < 100; i++)
            {
                goods.Add(new Good { Name = $"Good {i}" });
            }

        }

        private void FillOrders(IServiceProvider serviceProvider)
        {

            var goods = serviceProvider.GetService<List<Good>>();
            var orders = serviceProvider.GetService<List<Order>>();

            var random = new Random();
            
            for (int i = 0; i < 50; i++) // 50 заказов
            {
                var resultGoods = goods.GetRange(random.Next(84), random.Next(15)); //84 макс верх граница индекса, 15 макс кол товаров        

                orders.Add(new Order { Goods = resultGoods });
            }

        }


    }
}
