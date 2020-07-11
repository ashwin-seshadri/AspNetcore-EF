using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPieShop.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly IPieRepository _pieRepository;
		private readonly ShoppingCart _shoppingCart;

		public ShoppingCartController(IPieRepository pieRepository, ShoppingCart shoppingCart)
		{
			this._pieRepository = pieRepository;
			this._shoppingCart = shoppingCart;
		}

		// GET: /<controller>/
		public IActionResult Index()
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

		public IActionResult AddToCart(int pieId)
		{
			var pie = this._pieRepository.Pies.FirstOrDefault(p => p.Id == pieId);

			if(pie != null)
			{
				this._shoppingCart.AddToCart(pie, 1);
			}

			return RedirectToAction("Index");
		}

		public IActionResult RemoveFromCart(int pieId)
		{
			var pie = this._pieRepository.Pies.FirstOrDefault(p => p.Id == pieId);

			if (pie != null)
			{
				this._shoppingCart.RemoveFromCart(pie);
			}

			return RedirectToAction("Index");
		}
	}
}
