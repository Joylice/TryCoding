using Cactus.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper.Common;
using Cactus.Common;

namespace Cactus.MSSQLService
{
    public class ActionServer : IActionServer
    {
        public bool Insert(Model.Sys.Action entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
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
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                conn.Execute("UPDATE sys_action SET ActionName=@ActionName,ActionUrl=@ActionUrl WHERE Action_Id =@Action_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                conn.Execute(string.Format("DELETE FROM sys_action WHERE Action_Id in ({0})", ids));
            }
        }

        public List<Model.Sys.Action> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
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
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
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
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                string sql = "select * from sys_action ";   
                string sql01 = "select count(*) from sys_action";
                count= conn.Query<int>(sql01).SingleOrDefault();
                Model.Sys.Action actionTemp = new Model.Sys.Action();
                string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex - 1) * pageSize;
                return conn.Query<Model.Sys.Action>(query).ToList();
            }
        }
    }
}
