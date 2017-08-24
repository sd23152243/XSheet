using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Xml;
using System.Data.Common;
using XSheet.v2.Util;
using XSheet.v2.CfgBean;
using System.IO;

namespace XSheet.v2.Form
{
    public partial class XDashBoardForm : DevExpress.XtraEditors.XtraForm
    {
        XmlDocument xmlDoc = new XmlDocument();

        //XmlDeclaration xmldecl;
        
        public XDashBoardForm(DashBoardCfg cfg)
        {
            InitializeComponent();
            dash_viewMain.Dashboard = new DevExpress.DashboardCommon.Dashboard();
            LoadXml(cfg.fileLocation);
            dash_viewMain.Dashboard.LoadFromXml("dashboard.xml");
            this.WindowState = FormWindowState.Maximized;
            if (File.Exists("dashboard.xml"))
            {
                File.Delete("dashboard.xml");
            }
            this.Show();
            
        }


        private void LoadXml(String path)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            
            UpdateElement();
        }

        //修改节点   
        public void UpdateElement()
        {
            
            //获取根节点的所有子节点   
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("/Dashboard/DataSources").ChildNodes;
            List<String> values = new List<string>();
            List<String> names = new List<string>();
            names.Add("server");
            names.Add("database");
            names.Add("useIntegratedSecurity");
            names.Add("read only");
            names.Add("generateConnectionHelper");
            names.Add("userid");
            names.Add("password");
            values.Add("");
            values.Add("master");
            values.Add("False");
            values.Add("1");
            values.Add("False");
            values.Add("");
            values.Add("");
            foreach (XmlNode node in nodeList)
            {
                if (node.Name.Contains("OLAP"))
                {
                    continue;
                }
                String connstr = DBUtil.getConnStr(node.SelectSingleNode("Name").InnerText);
                if (connstr == "")
                {
                    continue;
                }
                String[] tmp = connstr.Split(';');
                foreach (String item in tmp)
                {
                    String[] arrtmp = item.Split('=');
                    switch (arrtmp[0].ToString().Trim().ToUpper())
                    {
                        case "DATA SOURCE":
                            values[0] = arrtmp[1].ToString();
                            break;
                        case "USER ID":
                            values[5] = arrtmp[1].ToString();
                            break;
                        case "PASSWORD":
                            values[6] = arrtmp[1].ToString();
                            break;
                        default:
                            break;
                    }
                }

                XmlNode Parameters = node.SelectSingleNode("Connection/Parameters");
                Parameters.RemoveAll();
                for(int i =0;i< 7; i++)
                {
                    XmlElement Parameter = xmlDoc.CreateElement("Parameter");
                    Parameter.SetAttribute("Name", names[i]);
                    Parameter.SetAttribute("Value", values[i]);
                    Parameters.AppendChild(Parameter);
                }
            }
            /* < Parameter Name = "server" Value = "srf-sql" />
            < Parameter Name = "database" Value = "DWH" />
            < Parameter Name = "useIntegratedSecurity" Value = "False" />
            < Parameter Name = "read only" Value = "1" />
            < Parameter Name = "generateConnectionHelper" Value = "false" />
            < Parameter Name = "userid" Value = "IDB_R" />
            < Parameter Name = "password" Value = "guoyuwei2007@r" />*/
            
            xmlDoc.Save("dashboard.xml");
        }
    }
}