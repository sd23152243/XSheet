using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XSheet.Util;

namespace XSheet.v2.Util
{
    class ConfigUtil
    {
        private static string file = @"\\ichart3t\发行文件\DBConn.config";
        //private static string file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"DBConn.config";
        public static string GetConnectionString(string key)
        {
            String connstr =  GetAttributeValue("//connectionStrings", key, "connectionString");

            connstr = DESUtil.DecryptString(connstr, DESUtil.GenerateKey());
            return connstr;
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
                if (File.Exists(@"\\ichart3t\发行文件\DBConn.config"))
                {
                    XmlDocument xml = new XmlDocument();

                    xml.Load(file);

                    XmlNode xNode = xml.SelectSingleNode(node);

                    XmlElement element = (XmlElement)xNode.SelectSingleNode("//add[@name='" + key + "']");
                    value = element.GetAttribute(name).ToString();
                    return value;
                }
                else
                {
                    AlertUtil.Show("error", "DBConfig无法定位！！");
                    return null;
                }
                
            }
            catch {
                AlertUtil.Show("error", "DBConfig异常！");
            }

            return value;
        }
    }
}
