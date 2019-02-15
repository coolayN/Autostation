using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Autostation.Clients;

namespace Autostation
{
    public partial class Archive : Form
    {
        DateTime date;
        XDocument doc;
        IEnumerable<Client> clients;

        public Archive()
        {
            InitializeComponent();        
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            date = dateTimePicker1.Value;
            buttonShow.Enabled = true;
        }

   
        private void ClientSelector()
        {
            doc = XDocument.Load("cl1");
            clients = from cl in doc.Element("clients").Elements("client")
                          where cl.Element("date").Value == date.ToShortDateString()
                          select new Client(cl.Element("number").Value, cl.Element("sum").Value,
                          cl.Element("date").Value, cl.Element("time").Value, cl.Element("goods").Value);   
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {

            ClientSelector();
            //BindingSource bindingSource1 = new BindingSource();
            //bindingSource1.DataSource = clients;
            //dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = clients.ToList();
        }
      
    }
}
