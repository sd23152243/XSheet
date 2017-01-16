using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Navigation;
using XSheet.v2.Data;
using XSheet.v2.Util;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.IO;

namespace XSheet.v2.Privilege
{
    public class XSheetUser
    {
        public String UserName { get; set; }
        public String UserDomain { get; set; }
        public String machineName { get; set; }
        public String OSVersion { get; set; }
        public Boolean logAsDesigner = false;
        public Dictionary<String, String> apps { get; set; } 
        public List<TileBarItem> items { get; set; }
        public XSheetUser(String domain,String name)
        {
            initXSheetUser(domain, name, null, null);
        }

        private void initXSheetUser(string domain, string name, String machineName, String OSVersion)
        {
            this.UserName = name;
            this.UserDomain = domain;
            this.machineName = machineName;
            this.OSVersion = OSVersion;
            this.logAsDesigner = CheckDesigner();
            makeAppList();
        }

        private void makeAppList()
        {
            this.apps = new Dictionary<string,string>();
            this.items = new List<TileBarItem>();
            String execute = String.Format("EXEC XSHEET.[dbo].[sp_CheckAPPList] N'{0}', N'{1}'", UserDomain, UserName);
            DataTable dt = DBUtil.getDataTable("MAIN", execute, "", null, null);
            int colorindex = 0;
            foreach (DataRow row in dt.Rows)
            {
                apps.Add(row[0].ToString().ToUpper(), row[3].ToString());
                TileBarItem item = new TileBarItem();
                item.AppearanceItem.Normal.BackColor = ItemColorList.colorList[colorindex];
                item.AppearanceItem.Normal.Options.UseBackColor = true;
                item.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
                TileItemElement tileitem = new TileItemElement();
                tileitem.Text = row[1].ToString();
                item.Elements.Add(tileitem);
                item.Id = 6 + colorindex;
                item.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Medium;
                ToolTipTitleItem tooltiptitle = new ToolTipTitleItem();
                tooltiptitle.Text = row[0].ToString();
                ToolTipItem tooltip = new ToolTipItem();
                tooltip.LeftIndent = 6 + colorindex;
                tooltip.Text = row[1].ToString();
                SuperToolTip supertooltip = new SuperToolTip();
                supertooltip.Items.Add(tooltiptitle);
                supertooltip.Items.Add(tooltip);
                item.SuperTip = supertooltip;

                item.Tag = row[0].ToString();
                colorindex++;
                if (colorindex >= 4)
                {
                    colorindex = 0;
                }
                items.Add(item);
            }

        }

        private Boolean CheckDesigner()
        {
            String execute = String.Format("EXEC	XSHEET.[dbo].[sp_CheckRight] '{0}', '{1}', '{2}','{3}'", UserDomain, UserName, "DashboardDesigner", "*");
            DataTable dt = DBUtil.getDataTable("MAIN", execute, "", null, null);
            return dt.Rows[0][0].ToString()==""? false:true;
        }

        internal List<TileBarItem> getItemList()
        {
            return this.items;
        }



        public XSheetUser(String domain, String name,String machineName,String OSVersion)
        {
            initXSheetUser(domain, name, machineName, OSVersion);
        }
        public String getPrivilege(XRSheet sheet)
        {
            if (logAsDesigner)
            {
                return "CRUDP";
            }
            String execute = String.Format("EXEC	XSHEET.[dbo].[sp_CheckRight] '{0}', '{1}', '{2}','{3}'", UserDomain, UserName, sheet.app.AppID, sheet.sheetName);
            DataTable dt = DBUtil.getDataTable("MAIN", execute, "", null,null);
            return dt.Rows[0][0].ToString();
        }

        public String getFullUserName()
        {
            return UserDomain + "\\" + UserName;
        }
    }
}
