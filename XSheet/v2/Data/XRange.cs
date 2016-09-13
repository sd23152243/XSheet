using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XSheet.Data;
using XSheet.Util;
using XSheet.v2.CfgBean;
using XSheet.v2.Util;

namespace XSheet.v2.Data
{ 
    public abstract class XRange
    {
        public String Name { get; set; }//DefinedName 名称
        public XRSheet rsheet { get; set; }//DefinedName 所在的位置
        private String InitStatement { get; set; }
        //Name中所绑定的Command，String为事件，XCommand为事件对应命令
        protected Dictionary<SysEvent, Dictionary<int, XCommand>> commands = new Dictionary<SysEvent, Dictionary<int, XCommand>>();
        public DataCfg cfg { get; set; }//从配置Sheet中读取的配置信息
        protected XData data { get; set; }//当前Range中所包含的
        //public DbDataAdapter da { get; set; }
        private Workbook book;

        

        protected abstract void p_init();
        public Dictionary<String, XAction> actions { get; set; }
        public abstract Range getRange();
        abstract public int isInRange(Range range);
        abstract public String getType();
        abstract public Boolean isSelectable();
        abstract public void onSelect(Boolean isMutil);
        
        abstract public void doResize(int rowcount);
        public XRange()
        {
            commands = new Dictionary<SysEvent, Dictionary<int, XCommand>>();            
        }
        //程序初始化
        public void init(DataCfg cfg,IWorkbook book)
        {
            this.cfg = cfg;
            this.Name = cfg.RangeName;
            if (LocateRange(book))
            {
                this.data = new XData();
                data.ServerName = cfg.ServerName;
                data.DBName = cfg.DBName;
                this.InitStatement = cfg.InitStatement;
                p_init();
            }
        }

        public List<string> getValueByTableCol(String tablename,int col)
        {
            XRange PRange = rsheet.SearchRangeByName(tablename);
            return PRange.getSelectedValueByColIndex(col);
        }

        public abstract List<string> getSelectedValueByColIndex(int col);

        protected abstract Boolean LocateRange(IWorkbook book);//设置存入区域内容

        public abstract Boolean setRange(Range range);

        public virtual List<String> getRealStatement(String statement)
        {
            //return getRange().Worksheet.Workbook.Worksheets["Config"].Range[cfg.InitStatement][0].DisplayText;  
            String tableName = null;
            List<List<String>> lists = new List<List<string>>();
            List<String> result = new List<string>() ;
            if (statement.Length > 0)
            {
                rsheet.app.getSheetByName("Config").Calculate();
                statement = rsheet.app.getSheetByName("Config")[statement][0].DisplayText;
                Regex reg = new Regex("@#(.+?)#@");
                MatchCollection matches = reg.Matches(statement);

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        String[] strParams = match.Groups[1].Value.Split('$');
                        if (tableName == null)
                        {
                            tableName = strParams[0];
                        }
                        else if (tableName != strParams[0])
                        {
                            AlertUtil.Show("禁止参数来源于多张表", "监测到当前命令参数存在" + tableName + "," + strParams + "，请检查Action配置");
                            return null;
                        }
                        int col = -1;
                        try
                        {
                            col = int.Parse(strParams[1]);
                        }
                        catch (Exception e)
                        {
                            AlertUtil.Show("error", e.ToString());
                            return null;
                        }

                        List<String> values = getValueByTableCol(tableName, col);
                        if (values.Count == 0)
                        {
                            values.Add("NULL");
                        }
                        lists.Add(values);
                    }
                    for (int i = 0; i < lists[0].Count; i++)
                    {
                        String tmp = statement;
                        for (int j = 0; j < matches.Count; j++)
                        {
                            tmp = tmp.Replace(matches[j].Value, lists[j][i]);
                        }
                        result.Add(tmp);
                    }
                }
                else
                {
                    result.Add(statement);
                }
            }
            else
            {
                result.Add("");
            }
            return result;
        }

        public virtual String getRealStatement(String statement,int seq)
        {
            String tableName = null;
            List<List<String>> lists = new List<List<string>>();
            String result = "";
            if (statement.Length > 0)
            {
                Regex reg = new Regex("@#(.+?)#@");
                MatchCollection matches = reg.Matches(statement);

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        String[] strParams = match.Groups[1].Value.Split('$');
                        if (tableName == null)
                        {
                            tableName = strParams[0];
                        }
                        else if (tableName != strParams[0])
                        {
                            AlertUtil.Show("禁止参数来源于多张表", "监测到当前命令参数存在" + tableName + "," + strParams + "，请检查Action配置");
                            return null;
                        }
                        int col = -1;
                        try
                        {
                            col = int.Parse(strParams[1]);
                        }
                        catch (Exception e)
                        {
                            AlertUtil.Show("error", e.ToString());
                            return null;
                        }

                        List<String> values = getValueByTableCol(tableName, col);
                        lists.Add(values);
                    }
                    int i = seq;
                    String tmp = statement;
                    for (int j = 0; j < matches.Count; j++)
                    {
                        tmp = tmp.Replace(matches[j].Value, lists[j][i]);
                    }
                    if (result == "")
                    {
                        result = tmp;
                    }
                    else
                    {
                        result += "UNION ALL " + tmp;
                    }
                }
                else
                {
                    result = statement;
                }
            }
            return result;
        }

        public abstract void fill(DataTable dt);

        /*public abstract void fill(String sql);

        public abstract void fill();*/

        public virtual DataTable getDataTable()
        {
            return data.getDataTable();
        }

        public void DspShow()
        {
            Range range = getRange();
            range.Worksheet.Workbook.Worksheets.ActiveWorksheet = range.Worksheet;
            range.Worksheet.ScrollTo(range[-1,0]);
        }

        public virtual Boolean isDataValied()
        {
            return data.isValied();
        }

        public DbDataAdapter getDbDataAdapter(String sql)
        {
            //TODO
            throw new NotImplementedException();
        }

        public XCommand getCommandByEvent(SysEvent e,int id)
        {
            XCommand cmd;
            try
            {
                 cmd= commands[e][id];
            }
            catch (Exception)
            {
                cmd = null;
            }
            return cmd;
        }

        public Dictionary<int,XCommand> getCommandByEvent(SysEvent e)
        {
            try
            {
                return  commands[e];
            }
            catch (Exception)
            {
                return null;
            }
        }
        /*
        public void setDefaultCommand(XCommand cmd)//设置默认命令，即当查找事件存在命令，则不设置，不存在则设置
        {
            throw new NotImplementedException();
        }
        */
        public void setCommand(XCommand cmd)//设置命令
        {
            Dictionary<int,XCommand> cmds = getCommandByEvent(cmd.e);
            if (cmds == null)
            {
                cmds = new Dictionary<int, XCommand>();
                cmds.Add(cmd.CommandSeq, cmd);
                commands.Add(cmd.e, cmds);
            }
            else
            {
                cmds.Add(cmd.CommandSeq, cmd);
            }
        }

        public abstract void newData(int count);

        public int isInRange(AreasCollection areas)
        {
            int i = 0;
            foreach (Range range in areas)
            {
                i = isInRange(range);
                if (i < 0)
                {
                    break;
                }
            }
            return i;
        }

        public abstract List<String> getValiedLFunList();//接口，获取当前Range可用功能列表 返回C R U D P CS CM US RO

        public abstract String doSearch();

        public abstract String doUpdate();

        public abstract String doInsert();

        public abstract String doDelete();

        public abstract String doSearch(List<String> sqls);

        public abstract String ExecuteSql(List<String> sqls);

        public virtual void Refresh()
        {
            data.Refresh();
            fill(data.getDataTable());
        }

        public abstract void onUpdateSelect(bool v);

        public abstract void ResetSelected();
    }
}
