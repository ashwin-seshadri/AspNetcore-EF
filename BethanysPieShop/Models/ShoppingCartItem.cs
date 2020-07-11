using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class ShoppingCartItem
	{
		public int Id { get; set; }
		public Pie Pie { get; set; }
		public int Count { get; set; }
		public string CartId { get; set; }

	}
}
