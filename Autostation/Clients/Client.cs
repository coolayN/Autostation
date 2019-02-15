using Autostation.Cafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Autostation.Clients
{
    class Client
    {
        public int number { get; private set; }
        public double sum { get; private set; }
        public string date { get; private set; }
        public string time { get; private set; }
        public string goodsToArch { get; private set; }
        public List<Good> goods = new List<Good>();

        public Client( DateTime dateTime)
        {
            date = dateTime.ToShortDateString();
            time = dateTime.ToShortTimeString();
        }

        public Client(string num, string sum, string date, string time ,string listOfGoods)
        {
            number = int.Parse(num);
            string sumR = sum.Replace('.', ',');
            this.sum = double.Parse(sumR);
            this.date = date;
            this.time = time;
            goodsToArch = listOfGoods;
        }

        public void SetGoods(double sum, List<Good> goods)
        {
            this.sum = sum;
            this.goods.AddRange(goods);
        }

        private string ListOfGoodsToString()
        {
            foreach (Good good in goods.Where((good)=>good.num!=0))
            {
                goodsToArch += good.name + ":" + good.num.ToString() + " ";
            }
            return goodsToArch;
        }

        public  void SavingClientToXML(XDocument document, int number )
        {
            this.number = number;
            XElement client = new XElement("client",
                new XElement("number", number),
                new XElement("sum", sum),
                new XElement("date", date),
                new XElement("time", time),
                new XElement("goods", ListOfGoodsToString()));

            document.Root.Add(client);
            document.Save("cl1");                       
        }
    }
}
