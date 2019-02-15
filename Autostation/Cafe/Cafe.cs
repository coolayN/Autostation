
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autostation.Cafe
{
    class CafeShop:IEnumerable
    {
        List<Good> goods;
        double totalShopPrice;

        public Good this[string name]
        {
            get { return goods.Where((good) => good.name == name).First();}
        }


        public CafeShop(params Good[] good)
        {
            goods = new List<Good>(good);
        }

        public IEnumerator GetEnumerator()
        {
            return goods.GetEnumerator();
        }

        public  string SetTotalShopPrice()
        {
            totalShopPrice = goods.Where((good) => good.num != 0).Select((good) => good.num * good.price).Sum();
            return totalShopPrice.ToString("0.00");
        }

        public IEnumerable<Good> SoldGoods()
        {
            IEnumerable<Good> soldGoods = goods.Where((good) => good.num != 0);
            return soldGoods;

        }

        

    }
}
