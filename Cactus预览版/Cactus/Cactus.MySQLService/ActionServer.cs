﻿using Cactus.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper.Common;
using Cactus.Common;

namespace Cactus.MySQLService
{
    public class ActionServer : IActionServer
    {
        //IDbConnection conn = new SqlConnection(SqlString.MSSQLString);

        public bool Insert(Model.Sys.Action entity)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                int i = conn.Execute("INSERT INTO sys_action(ActionName,ActionUrl)VALUES(@ActionName,@ActionUrl)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.Sys.Action> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Sys.Action entity)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute("UPDATE sys_action SET ActionName=@ActionName,ActionUrl=@ActionUrl WHERE Action_Id =@Action_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                conn.Execute(string.Format("DELETE FROM sys_action WHERE Action_Id in ({0})", ids));
            }
        }

        public List<Model.Sys.Action> GetAll()
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                //List<Model.Sys.Action> list=new List<Model.Sys.Action>();
                string sql = "select * from sys_action";

                Model.Sys.Action actionTemp = new Model.Sys.Action();

                return conn.Query<Model.Sys.Action>(sql);
                //return list;
            }
        }

        public Model.Sys.Action Find(int id)
        {
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string query = "select * FROM sys_action WHERE Action_Id = @id";

                return conn.Query<Model.Sys.Action>(query).SingleOrDefault();
            }
        }

        public List<Model.Sys.Action> ToPagedList(int pageIndex, int pageSize, string keySelector,out int count)
        {
            /*
                * firstIndex:起始索引
                * pageSize:每页显示的数量
                * orderColumn:排序的字段名
                * sql:可以是简单的单表查询语句，也可以是复杂的多表联合查询语句
            */
            using (IDbConnection conn = SqlString.GetMySqlConnection())
            {
                string sql = "select * from sys_action";   
                string sql01 = "select count(*) from sys_action";
                count= conn.Query<int>(sql01).SingleOrDefault();
                Model.Sys.Action actionTemp = new Model.Sys.Action();
                
                string query = "SELECT * from (" + sql + ")as c ORDER BY " + keySelector+" LIMIT " + (pageIndex - 1) * pageSize + "," + pageSize;
                
                return conn.Query<Model.Sys.Action>(query).ToList();
            }
        }
    }
}
