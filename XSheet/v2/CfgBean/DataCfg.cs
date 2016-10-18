using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.CfgBean
{
    public class DataCfg
    {
        //DataName DataDescription	ObjectName	ObjectType	DBName	ServerName	RangeName	CRUDP	SVK	InitStatement
        public String TableTitle { get; set; }//数据名称
        public String TableDesc { get; set; }//数据描述
        public String CRUDP { get; set; }//CRUDP标记
        public String SVK { get; set; }//SELECT CELLVALUECHANGE KEYPRESS事件
        public String RangeName { get; set; }//对象区域名称，不填写时自动生成TB_数据名称
        public String DBName { get; set; }//对象数据库名称
        //public String ObjectName { get; set; }//数据对象（表、视图、存储过程）名称
        //public String ObjectType { get; set; }//数据类型（表、视图、存储过程）
        public String ServerName { get; set; }//对象服务器名称
        public String DefalutStatement { get; set; }//基础SQL查询语句，开发时使用
    }
}
