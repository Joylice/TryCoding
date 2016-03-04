﻿using Cactus.IService;
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
    public class RoleServer : IRoleServer
    {
        //IDbConnection conn = new SqlConnection(SqlString.MSSQLString);

        public bool Insert(Model.Sys.Role entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                int i = conn.Execute("INSERT INTO sys_role(RoleName,ActionIds)VALUES(@RoleName,@ActionIds)", entity);
                if (i > 0) { return true; } else { return false; }
            }
        }

        public bool InsertBatch(List<Model.Sys.Role> datas)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Sys.Role entity)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                conn.Execute("UPDATE sys_role SET RoleName=@RoleName,ActionIds=@ActionIds WHERE Role_Id =@Role_Id", entity);
            }
        }

        public void Delete(string ids)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                conn.Execute(string.Format("DELETE FROM sys_role WHERE Role_Id in ({0})", ids));
            }
        }

        public List<Model.Sys.Role> GetAll()
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                Model.Sys.Role roleTemp = new Model.Sys.Role();
                return conn.Query<Model.Sys.Role, Model.Sys.User, Model.Sys.Role>("select a.*,b.* from sys_role as a left join sys_user as b on a.Role_Id=b.RoleId", (role, user) =>
                {

                    if (roleTemp == null || roleTemp.Role_Id != role.Role_Id)
                        roleTemp = role;
                    if (user != null) {
                        if (role.Users == null) { role.Users = new List<Model.Sys.User>(); }
                        role.Users.Add(user);
                    }
                    
                    return role;

                }, null, null, "User_Id", null, null).Distinct().ToList();
            }
        }

        public List<Model.Sys.Role> ToPagedList(int pageIndex, int pageSize, string keySelector,out int count)
        {
            /*
                * firstIndex:起始索引
                * pageSize:每页显示的数量
                * orderColumn:排序的字段名
                * sql:可以是简单的单表查询语句，也可以是复杂的多表联合查询语句
            */
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                string sql = "select a.*,b.* from sys_role as a left join sys_user as b on a.Role_Id=b.RoleId";
                string sql01 = "select count(*) from sys_role";
                count = conn.Query<int>(sql01).SingleOrDefault();

                string query = "select top " + pageSize + " o.* from (select row_number() over(order by " + keySelector + ") as rownumber,* from(" + sql + ") as oo) as o where rownumber>" + (pageIndex - 1) * pageSize;
                Model.Sys.Role roleTemp = new Model.Sys.Role();
                return conn.Query<Model.Sys.Role, Model.Sys.User, Model.Sys.Role>(query, (role, user) =>
                {

                    if (roleTemp == null || roleTemp.Role_Id != role.Role_Id)
                        roleTemp = role;
                    if (user != null)
                    {
                        if (role.Users == null) { role.Users = new List<Model.Sys.User>(); }
                        role.Users.Add(user);
                    }
                    return role;

                }, null, null, "User_Id", null, null).Distinct().ToList();
            }
        }

        public Model.Sys.Role Find(int id)
        {
            using (IDbConnection conn = SqlString.GetSqlConnection(SqlString.MSSQLString))
            {
                string query = "select a.*,b.* from sys_role as a left join sys_user as b on a.Role_Id=b.RoleId WHERE a.Role_Id = @id";
                return conn.Query<Model.Sys.Role, Model.Sys.User, Model.Sys.Role>(query, (role, user) =>
                {
                    if (user != null)
                    {
                        if (role.Users == null) { role.Users = new List<Model.Sys.User>(); }
                        role.Users.Add(user);
                    }
                    return role;

                }, new { id = id }, null, "User_Id", null, null).SingleOrDefault();
            }
        }
    }
}
