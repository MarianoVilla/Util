using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Dager
{
    public static class XMLUtil
    {
        public static string GetNodeValue(string XMLPath, string Node)
        {
            using (var F = File.Open(XMLPath, FileMode.Open))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(F);
                return doc.SelectSingleNode(Node).Value;
            }
        }
    }
}
