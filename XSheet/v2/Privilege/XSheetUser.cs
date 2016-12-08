using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.Data;
using XSheet.v2.Util;

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
        }

        private Boolean CheckDesigner()
        {
            String execute = String.Format("EXEC	XSHEET.[dbo].[sp_CheckRight] '{0}', '{1}', '{2}','{3}'", UserDomain, UserName, "DashboardDesigner", "*");
            DataTable dt = DBUtil.getDataTable("MAIN", execute, "", null, null);
            return dt.Rows[0][0].ToString()==""? false:true;
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
