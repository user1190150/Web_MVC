using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haris.Models.ViewModels
{
    internal class ShoppingCartVM
    {
        public IEnumerable<ShoppingCartVM> ShoppingCartList { get; set; }
        public double OrderTotal { get; set; }
    }
}
