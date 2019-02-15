using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Autostation.Clients
{
    class ClientsXML
    {
        public string path;
        public XDocument doc;

        public ClientsXML(string path)
        {
            this.path = path;
            XMLSavier();
        }

        public XDocument XMLSavier()
        {
            doc = new XDocument();
            XElement rootElement = new XElement("clients");
            doc.Add(rootElement);
            doc.Save(path);
            return doc;
 
        }
    }
}
