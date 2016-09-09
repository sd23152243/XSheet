using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XSheet.v2.Util
{
    class ConfigUtil
    {
        private static string file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"DBConn.config";

        public static string GetConnectionString(string key)
        {
            return GetAttributeValue("//connectionStrings", key, "connectionString");
        }

        public static string GetProviderName(string key)
        {
            return GetAttributeValue("//connectionStrings", key, "providerName");
        }

        public static string GetAttributeValue(string node, string key,String name)
        {
            string value = string.Empty;

            try
            {
                if (File.Exists(file))
                {
                    XmlDocument xml = new XmlDocument();

                    xml.Load(file);

                    XmlNode xNode = xml.SelectSingleNode(node);

                    XmlElement element = (XmlElement)xNode.SelectSingleNode("//add[@name='" + key + "']");
                    value = element.GetAttribute(name).ToString();
                    
                }
            }
            catch { }

            return value;
        }
    }
}
