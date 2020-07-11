using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class OrderRepository : IOrderRepository
	{
		private readonly AppDbContext _dbContext;
		private readonly ShoppingCart _shoppingCart;

		public OrderRepository(AppDbContext dbContext, ShoppingCart shoppingCart)
		{
			this._dbContext = dbContext;
			this._shoppingCart = shoppingCart;
		}
		public void CreateOrder(Order order)
		{
			order.OrderPlaced = DateTime.Now;
			order.Total = this._shoppingCart.GetTotal();

			order.Details = new List<OrderDetail>();

			foreach(var item in this._shoppingCart.Items)
			{
				var detail = new OrderDetail
				{
					Count = item.Count,
					PieId = item.Pie.Id,
					Price = item.Pie.Price
				};

				order.Details.Add(detail);
			}

			this._dbContext.Orders.Add(order);

			this._dbContext.SaveChanges();
		}
	}
}
