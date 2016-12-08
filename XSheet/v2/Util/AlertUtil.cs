using DevExpress.XtraBars.Alerter;
using DevExpress.XtraSplashScreen;
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
        static private SplashScreenManager splashManager = null;

        public static void setAlert(AlertControl alert, System.Windows.Forms.Form form)
        {
            AlertUtil.alert = alert;
            AlertUtil.form = form;
            splashManager = new DevExpress.XtraSplashScreen.SplashScreenManager(form, typeof(global::XSheet.v2.Form.process), true, true);
        }

        public static void Show(String title,String message)
        {
            if (form == null)
            {
                System.Windows.Forms.MessageBox.Show(message);
            }
            else
            {
                alert.Show(form, title, message);
            }
            if (title == "error" || title == "err")
            {
                LogWriter.log(message);
            }
        }

        public static void StartWait()
        {
            if (!splashManager.IsSplashFormVisible)
            {
                splashManager.ShowWaitForm();
            }
        }

        public static void StopWait()
        {
            if (splashManager.IsSplashFormVisible)
            {
                splashManager.CloseWaitForm();
            }
        }
    }
}
