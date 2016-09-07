using DevExpress.XtraBars.Alerter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Util
{
    public static class AlertUtil
    {
        static AlertControl alert = null;
        static System.Windows.Forms.Form form = null;

        public static void setAlert(AlertControl alert, System.Windows.Forms.Form form)
        {
            AlertUtil.alert = alert;
            AlertUtil.form = form;
        }

        public static void Show(String title,String message)
        {
            alert.Show(form, title, message);
        }
    }
}
