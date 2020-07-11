using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Components
{
	public class ShoppingCartSummary: ViewComponent
	{
		private readonly ShoppingCart _shoppingCart;

		public ShoppingCartSummary(ShoppingCart shoppingCart)
		{
			this._shoppingCart = shoppingCart;
		}

		public IViewComponentResult Invoke()
		{
			var items = this._shoppingCart.GetCartItems();
			this._shoppingCart.Items = items;

			var model = new ShoppingCartViewModel
			{
				ShoppingCart = this._shoppingCart,
				Total = this._shoppingCart.GetTotal()
			};

			return View(model);
		}
	}
}
