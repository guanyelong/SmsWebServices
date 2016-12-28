using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysWeb
{
    public static class SQLHelp
    {
        private static readonly string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 返回执行增加、删除、修改操作后造成影响的行数
        /// </summary>
        /// <param name="sql">要执行的Sql语句</param>
        /// <param name="cmdType">要执行的命令类型</param>
        /// <param name="pms">传入的参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = cmdType;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 返回数据库查询结果首行首列的值
        /// </summary>
        /// <param name="sql">要执行的Sql语句</param>
        /// <param name="cmdType">要执行的命令类型</param>
        /// <param name="pms">传入的参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            object obj = null;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = cmdType;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();

                    obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
            return obj;
        }

        /// <summary>
        /// 返回SqlDataReader对象
        /// </summary>
        /// <param name="sql">要执行的Sql语句</param>
        /// <param name="cmdType">要执行的命令类型</param>
        /// <param name="pms">传入的参数</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            SqlConnection con = new SqlConnection(conStr);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = cmdType;
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                try
                {
                    con.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception)
                {
                    con.Close();
                    con.Dispose();
                    throw;
                }
            }
        }

        /// <summary>
        /// 封装一个返回DataTable对象的方法
        /// </summary>
        /// <param name="sql">要执行的Sql语句</param>
        /// <param name="cmdType">要执行的命令类型</param>
        /// <param name="pms">传入的参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            try
            {
                DataSet dbs = new DataSet();
                using (SqlConnection SqlConn = new SqlConnection(conStr))
                {
                    using (SqlCommand command = new SqlCommand(sql, SqlConn))
                    {
                        command.CommandType = CommandType.Text;
                        if (pms != null)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddRange(pms);
                        }
                        SqlDataAdapter sda = new SqlDataAdapter();
                        sda.SelectCommand = command;
                        sda.Fill(dbs, "data");
                    }
                }
                return dbs.Tables[0];
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        /// <summary>
        /// 封装一个返回DataTable对象的方法
        /// </summary>
        /// <param name="sql">要执行的Sql语句</param>
        /// <param name="cmdType">要执行的命令类型</param>
        /// <param name="pms">传入的参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(SqlTransaction trans, string cmdText, CommandType cmdType, params SqlParameter[] pms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, pms);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "ds");
                cmd.Parameters.Clear();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前查询页</param>
        /// <param name="pagesize">每页记录集数量</param>
        /// <param name="fdname">用于定位记录的主键(惟一键)字段,可以是逗号分隔的多个字段</param>
        /// <param name="filedName">以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段</param>
        /// <param name="TableName">查询表名</param>
        /// <param name="WhereStr">条件语句(可为空)</param>
        /// <param name="OrderByStr">以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC用于指定排序顺序(可为空)</param>
        /// <param name="pageCount">输出总页数</param>
        /// <param name="recordcount">记录集总数</param>
        /// <returns></returns>
        public static DataTable pageSearch(int pageIndex, int pagesize, string fdname, string filedName, string TableName, string WhereStr, string OrderByStr, out int pageCount, out int recordcount)
        {
            DataSet dbs = new DataSet();
            using (SqlConnection SqlConn = new SqlConnection(conStr))
            {
                if (SqlConn.State != ConnectionState.Open) { SqlConn.Open(); }
                if (pageIndex <= 0)
                {
                    pageIndex = 1;
                }
                using (SqlCommand command = new SqlCommand("proc_ShowPages", SqlConn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@FieldKey", SqlDbType.VarChar, 8000));
                    command.Parameters.Add(new SqlParameter("@FieldShow", SqlDbType.VarChar, 8000));
                    command.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar, 8000));
                    if (!string.IsNullOrEmpty(WhereStr))
                    {
                        command.Parameters.Add(new SqlParameter("@Where", SqlDbType.VarChar, 8000));
                    } if (!string.IsNullOrEmpty(OrderByStr))
                    {
                        command.Parameters.Add(new SqlParameter("@FieldOrder", SqlDbType.VarChar, 8000));
                    }
                    command.Parameters.Add(new SqlParameter("@TotalCount", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@TotalPageCount", SqlDbType.Int));
                    command.UpdatedRowSource = UpdateRowSource.None;
                    command.Parameters["@PageSize"].Value = pagesize;
                    command.Parameters["@PageIndex"].Value = pageIndex;
                    command.Parameters["@FieldKey"].Value = fdname;
                    command.Parameters["@FieldShow"].Value = filedName;
                    command.Parameters["@TableName"].Value = TableName;
                    if (!string.IsNullOrEmpty(WhereStr))
                    {
                        command.Parameters["@Where"].Value = WhereStr;
                    } if (!string.IsNullOrEmpty(OrderByStr))
                    {
                        command.Parameters["@FieldOrder"].Value = OrderByStr;
                    }
                    command.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
                    command.Parameters["@TotalPageCount"].Direction = ParameterDirection.Output;
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = command;
                    sda.SelectCommand.ExecuteNonQuery();
                    recordcount = int.Parse(sda.SelectCommand.Parameters["@TotalCount"].Value.ToString());
                    pageCount = int.Parse(sda.SelectCommand.Parameters["@TotalPageCount"].Value.ToString());
                    sda.Fill(dbs, "data");

                }
            }
            return dbs.Tables[0];
        }

        #endregion
        #region 分页
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>记录总数</returns>
        public static int GetRecordCount(string TableName, string strWhere, SqlParameter[] parms4org)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select count(1) FROM " + TableName + " T");
            if (strWhere.Trim() != "")
            {
                sbSql.Append(" where 1=1" + strWhere);
            }

            object obj = ExecuteScalar(sbSql.ToString(), CommandType.Text, parms4org);

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="startIndex">起始行数</param>
        /// <param name="endIndex">结束行数</param>
        /// <returns>DataSet</returns>
        public static DataTable GetListByPage(string TableName, string strWhere, string orderby, int startIndex, int endIndex, bool IsPage, SqlParameter[] parms4org, out int RowCount, string FiledsName = "*")
        {
            RowCount = 0;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT * FROM ( ");
            sbSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                sbSql.Append("order by T." + orderby);
            }
            else
            {
                sbSql.Append("order by T.ID desc");
            }
            sbSql.Append(")AS rowNum, T." + FiledsName + "  from " + TableName + " T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                sbSql.Append(" WHERE 1=1 " + strWhere);
            }
            sbSql.Append(" ) TT");
            if (IsPage)
            {
                RowCount = GetRecordCount(TableName, strWhere, parms4org);
                sbSql.AppendFormat(" WHERE TT.rowNum between {0} and {1}", startIndex, endIndex);
            }
            return ExecuteDataTable(sbSql.ToString(), CommandType.Text, parms4org);
        }
        public static DataTable GetList(string TableName, SqlParameter[] parms4org, string Where = "")
        {
            return ExecuteDataTable(TableName, CommandType.Text, parms4org);
        }
        public static DataTable GetListrandom(string TableName, SqlParameter[] parms4org, string Where = "")
        {
            return ExecuteDataTable(TableName, CommandType.Text, parms4org);
        }
        public static DataTable GetListrandomone(string TableName, SqlParameter[] parms4org, string Where = "")
        {
            return ExecuteDataTable(TableName, CommandType.Text, parms4org);
        }

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static SqlTransaction BeginTransaction()
        {
            SqlConnection myConnection = new SqlConnection(conStr);
            myConnection.Open();
            SqlTransaction tran = myConnection.BeginTransaction();
            return tran;
        }

        /// <summary>
        /// 生成要执行的命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            // 如果存在参数，则表示用户是用参数形式的SQL语句，可以替换
            if (cmdParms != null && cmdParms.Length > 0)
                cmdText = cmdText.Replace("?", "@").Replace(":", "@");

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    // 如果存在参数，则表示用户是用参数形式的SQL语句，可以替换
                    parm.ParameterName = parm.ParameterName.Replace("?", "@").Replace(":", "@");
                    if (parm.Value == null)
                        parm.Value = DBNull.Value;
                    cmd.Parameters.Add(parm);
                }
            }
        }
        #endregion
    }
}
