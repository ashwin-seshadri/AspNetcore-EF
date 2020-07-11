using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class ShoppingCart
	{
		private const string CartId = "CartId";
		private readonly AppDbContext _context;

		public string Id { get; set; }
		public IList<ShoppingCartItem> Items { get; set; }

		public ShoppingCart(AppDbContext context)
		{
			this._context = context;
		}

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			var session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

			if (session == null)
			{
				throw new InvalidOperationException("Session not available.");
			}

			var cartId = session.GetString(CartId);

			if (cartId == null)
			{
				cartId = Guid.NewGuid().ToString();
				session.SetString(CartId, cartId);
			}

			var context = services.GetRequiredService<AppDbContext>();

			return new ShoppingCart(context) { Id = cartId };
		}

		public void AddToCart(Pie pie, int count)
		{
			var item = this._context.ShoppingCartItems.SingleOrDefault(i => i.Pie.Id == pie.Id && i.CartId == this.Id);
			if(item == null)
			{
				item = new ShoppingCartItem { Pie = pie, Count = 1, CartId = this.Id };
				this._context.ShoppingCartItems.Add(item);
			}
			else
			{
				item.Count++;
			}
			
			this._context.SaveChanges();
		}

		public int RemoveFromCart(Pie pie)
		{
			var item = this._context.ShoppingCartItems.SingleOrDefault(i => i.Pie.Id == pie.Id && i.CartId == this.Id);
			var localCount = 0;

			if (item != null)
			{
				if(item.Count > 1)
				{
					item.Count--;
					localCount = item.Count;
				}
				else
				{
					this._context.ShoppingCartItems.Remove(item);
				}

				this._context.SaveChanges();
			}

			return localCount;
		}

		public IList<ShoppingCartItem> GetCartItems()
		{
			return Items ?? (Items = this._context.ShoppingCartItems.Where(i => i.CartId == this.Id).Include(i => i.Pie).ToList());
		}

		public void Clear()
		{
			var items = this._context.ShoppingCartItems.Where(i => i.CartId == this.Id);
			this._context.ShoppingCartItems.RemoveRange(items);
			this._context.SaveChanges();
		}

		public decimal GetTotal()
		{
			return this._context.ShoppingCartItems.Where(i => i.CartId == this.Id).Select(i => i.Pie.Price * i.Count).Sum();
		}
	}
}
