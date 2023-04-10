using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.Batch;
using Serilog.Sinks.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Serilog.Sinks.SqlServer
{
    internal class SqlServerSink : BatchProvider, ILogEventSink
    {
        private readonly string _connectionString;
        private readonly bool _storeTimestampInUtc;
        private readonly string _tableName;
        private readonly LogType _logType;

        public SqlServerSink(
            LogType type,
            string connectionString,
            string tableName = "log.operate",
            bool storeTimestampInUtc = false,
            uint batchSize = 100) : base(type, (int)batchSize)
        {
            _logType = type;
            _connectionString = connectionString;
            _tableName = tableName;
            _storeTimestampInUtc = storeTimestampInUtc;

            var sqlConnection = GetSqlConnection();
            CreateTable(sqlConnection);
        }

        public void Emit(LogEvent logEvent)
        {
            PushEvent(logEvent);
        }

        private SqlConnection GetSqlConnection()
        {
            try
            {
                var conn = new SqlConnection(_connectionString);
                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine(ex.Message);

                return null;
            }
        }

        private SqlCommand GetInsertCommand(SqlConnection sqlConnection)
        {
            var cmd = sqlConnection.CreateCommand();
            var tableCommandBuilder = new StringBuilder();

            if (_logType == LogType.Sys)
            {
                tableCommandBuilder.Append($"INSERT INTO  `{_tableName}` (");
                tableCommandBuilder.Append("`Timestamp`, `Level`, `Template`, `Message`, `Exception`, `Properties`) ");
                tableCommandBuilder.Append("VALUES (@ts, @level,@template, @msg, @ex, @prop)");

                cmd.CommandText = tableCommandBuilder.ToString();

                cmd.Parameters.Add(new SqlParameter("@ts", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@level", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@template", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@msg", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@ex", SqlDbType.VarChar));
                cmd.Parameters.Add(new SqlParameter("@prop", SqlDbType.VarChar));
            }
            else if (_logType == LogType.Operate)
            {
                tableCommandBuilder.Append($"INSERT INTO  `{_tableName}` (");
                tableCommandBuilder.Append(
                    "`LogId`, `Title`, `Type`, `HttpMethod`, `Method`, `Url`, `UrlType`, `IpAddress`, `Location`, `Params`, `Result`, `Status`, `ErrorMsg`, `OS`, " +
                    " `Browser`, `CreationTime`, `CreatorId`, `Creator`, `IsDeleted`)");
                tableCommandBuilder.Append(
                    "VALUES (@LogId, @Title, @Type, @HttpMethod, @Method, @Url, @UrlType, @IpAddress, @Location, @Params, @Result, @Status, @ErrorMsg, " +
                    "@OS, @Browser, @CreationTime, @CreatorId, @Creator, @IsDeleted)");

                cmd.CommandText = tableCommandBuilder.ToString();

                cmd.Parameters.Add(new SqlParameter("@LogId", SqlDbType.VarChar, 36));
                cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 64));
                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@HttpMethod", SqlDbType.VarChar, 16));
                cmd.Parameters.Add(new SqlParameter("@Method", SqlDbType.VarChar, 128));
                cmd.Parameters.Add(new SqlParameter("@Url", SqlDbType.VarChar, 256));
                cmd.Parameters.Add(new SqlParameter("@UrlType", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@IpAddress", SqlDbType.VarChar, 32));
                cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar, 256));
                cmd.Parameters.Add(new SqlParameter("@Params", SqlDbType.Text));
                cmd.Parameters.Add(new SqlParameter("@Result", SqlDbType.Text));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@ErrorMsg", SqlDbType.Text));
                cmd.Parameters.Add(new SqlParameter("@OS", SqlDbType.VarChar, 128));
                cmd.Parameters.Add(new SqlParameter("@Browser", SqlDbType.VarChar, 1024));
                cmd.Parameters.Add(new SqlParameter("@CreationTime", SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@CreatorId", SqlDbType.VarChar, 36));
                cmd.Parameters.Add(new SqlParameter("@Creator", SqlDbType.VarChar, 256));
                cmd.Parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit));
            }
            else if (_logType == LogType.Login)
            {
                tableCommandBuilder.Append($"INSERT INTO `{_tableName}` (");
                tableCommandBuilder.Append(
                    "`LogId`, `LoginName`, `IpAddress`, `Location`, `OS`, `Status`, `PromptMsg`, `Browser`, `CreationTime`, `CreatorId`, " +
                    " `Creator`, `IsDeleted`) ");
                tableCommandBuilder.Append(
                    "VALUES (@LogId, @LoginName, @IpAddress, @Location, @OS, @Status, @PromptMsg, @Browser, @CreationTime, @CreatorId, " +
                    "@Creator, @IsDeleted)");

                cmd.CommandText = tableCommandBuilder.ToString();

                cmd.Parameters.Add(new SqlParameter("@LogId", SqlDbType.VarChar, 36));
                cmd.Parameters.Add(new SqlParameter("@LoginName", SqlDbType.VarChar, 64));
                cmd.Parameters.Add(new SqlParameter("@IpAddress", SqlDbType.VarChar, 32));
                cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar, 256));
                cmd.Parameters.Add(new SqlParameter("@OS", SqlDbType.VarChar, 128));
                cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@PromptMsg", SqlDbType.VarChar, 256));
                cmd.Parameters.Add(new SqlParameter("@Browser", SqlDbType.VarChar, 1024));
                cmd.Parameters.Add(new SqlParameter("@CreationTime", SqlDbType.DateTime));
                cmd.Parameters.Add(new SqlParameter("@CreatorId", SqlDbType.VarChar, 36));
                cmd.Parameters.Add(new SqlParameter("@Creator", SqlDbType.VarChar, 256));
                cmd.Parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit));
            }

            return cmd;
        }

        private void CreateTable(SqlConnection sqlConnection)
        {
            try
            {
                var tableCommandBuilder = new StringBuilder();
                tableCommandBuilder.Append($"CREATE TABLE IF NOT EXISTS `{_tableName}` (");

                if (_logType == LogType.Sys)
                {
                    tableCommandBuilder.Append("`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,");
                    tableCommandBuilder.Append("`Timestamp` VARCHAR(100),");
                    tableCommandBuilder.Append("`Level` VARCHAR(15),");
                    tableCommandBuilder.Append("`Template` TEXT,");
                    tableCommandBuilder.Append("`Message` TEXT,");
                    tableCommandBuilder.Append("`Exception` TEXT,");
                    tableCommandBuilder.Append("`Properties` TEXT,");
                    tableCommandBuilder.Append("`_ts` TIMESTAMP DEFAULT CURRENT_TIMESTAMP");
                }
                else if (_logType == LogType.Operate)
                {
                    tableCommandBuilder.Append("`LogId` char(36) charset ascii not null comment '日志标识' primary key,");
                    tableCommandBuilder.Append("`Title` varchar(64) not null comment '模块标题',");
                    tableCommandBuilder.Append("`Type` int not null comment '业务类型（0其它 1新增 2修改 3删除）',");
                    tableCommandBuilder.Append("`HttpMethod` varchar(16) not null comment '请求方式',");
                    tableCommandBuilder.Append("`Method` varchar(128) not null comment '方法名称',");
                    tableCommandBuilder.Append("`Url` varchar(256) null comment '请求URL',");
                    tableCommandBuilder.Append("`UrlType` int null comment '用户类型（0其它 1后台用户 2手机端用户）',");
                    tableCommandBuilder.Append("`IpAddress` varchar(32) null comment '主机地址',");
                    tableCommandBuilder.Append("`Location` varchar(256) null comment '操作地点',");
                    tableCommandBuilder.Append("`Params` longtext null comment '请求参数',");
                    tableCommandBuilder.Append("`Result` longtext null comment '返回值',");
                    tableCommandBuilder.Append("`Status` int null comment '操作状态（0正常 1异常）',");
                    tableCommandBuilder.Append("`ErrorMsg` longtext null comment '错误信息',");
                    tableCommandBuilder.Append("`OS` varchar(128) null comment '操作系统',");
                    tableCommandBuilder.Append("`Browser` varchar(1024) null comment '浏览器类型',");
                    tableCommandBuilder.Append("`CreationTime` datetime(6) null comment '创建时间',");
                    tableCommandBuilder.Append("`CreatorId` char(36) charset ascii null comment '创建者标识',");
                    tableCommandBuilder.Append("`Creator` varchar(256) null comment '创建者',");
                    tableCommandBuilder.Append("`IsDeleted` tinyint(1) not null comment '是否删除'");
                }
                else if (_logType == LogType.Login)
                {
                    tableCommandBuilder.Append("`LogId` char(36) charset ascii not null comment '日志标识' primary key,");
                    tableCommandBuilder.Append("`LoginName` varchar(64) not null comment '登录帐号',");
                    tableCommandBuilder.Append("`IpAddress` varchar(32) not null comment '登录IP地址',");
                    tableCommandBuilder.Append("`Location` varchar(256) not null comment '登录地点',");
                    tableCommandBuilder.Append("`OS` varchar(64) null comment '操作系统',");
                    tableCommandBuilder.Append("`Status` int null comment '登录状态（0成功 1失败）',");
                    tableCommandBuilder.Append("`PromptMsg` varchar(256) null comment '提示消息',");
                    tableCommandBuilder.Append("`Browser` varchar(1024) null comment '浏览器类型',");
                    tableCommandBuilder.Append("`CreationTime` datetime(6) null comment '创建时间',");
                    tableCommandBuilder.Append("`CreatorId` char(36) charset ascii null comment '创建者标识',");
                    tableCommandBuilder.Append("`Creator` varchar(256) null comment '创建者',");
                    tableCommandBuilder.Append("`IsDeleted` tinyint(1) not null comment '是否删除'");
                }

                tableCommandBuilder.Append(" ) comment '日志' charset = utf8mb4");

                var cmd = sqlConnection.CreateCommand();
                cmd.CommandText = tableCommandBuilder.ToString();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine(ex.Message);
            }
        }

        protected override async Task<bool> WriteLogEventAsync(ICollection<LogEvent> logEventsBatch)
        {
            try
            {
                await using var sqlCon = GetSqlConnection();
                await using var tr = sqlCon.BeginTransaction();
                var insertCommand = GetInsertCommand(sqlCon);
                insertCommand.Transaction = tr;

                foreach (var logEvent in logEventsBatch)
                {
                    var logMessageString = new StringWriter(new StringBuilder());
                    logEvent.RenderMessage(logMessageString);

                    if (_logType == LogType.Sys)
                    {
                        insertCommand.Parameters["@ts"].Value = _storeTimestampInUtc
                            ? logEvent.Timestamp.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fffzzz")
                            : logEvent.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fffzzz");
                        insertCommand.Parameters["@level"].Value = logEvent.Level.ToString();
                        insertCommand.Parameters["@template"].Value = logEvent.MessageTemplate.ToString();
                        insertCommand.Parameters["@msg"].Value = logMessageString;
                        insertCommand.Parameters["@ex"].Value = logEvent.Exception?.ToString();
                        insertCommand.Parameters["@prop"].Value =
                            logEvent.Properties.Count > 0 ? logEvent.Properties.Json() : string.Empty;
                    }
                    else if (_logType == LogType.Operate)
                    {
                        insertCommand.Parameters["@LogId"].Value = ((ScalarValue)logEvent.Properties["LogId"]).Value;
                        insertCommand.Parameters["@Title"].Value = ((ScalarValue)logEvent.Properties["Title"]).Value;
                        insertCommand.Parameters["@Type"].Value = ((ScalarValue)logEvent.Properties["Type"]).Value;
                        insertCommand.Parameters["@HttpMethod"].Value =
                            ((ScalarValue)logEvent.Properties["HttpMethod"]).Value;
                        insertCommand.Parameters["@Method"].Value = ((ScalarValue)logEvent.Properties["Method"]).Value;
                        insertCommand.Parameters["@Url"].Value = ((ScalarValue)logEvent.Properties["Url"]).Value;
                        insertCommand.Parameters["@UrlType"].Value =
                            ((ScalarValue)logEvent.Properties["UrlType"]).Value;
                        insertCommand.Parameters["@IpAddress"].Value =
                            ((ScalarValue)logEvent.Properties["IpAddress"]).Value;
                        insertCommand.Parameters["@Location"].Value =
                            ((ScalarValue)logEvent.Properties["Location"]).Value;
                        insertCommand.Parameters["@Params"].Value = ((ScalarValue)logEvent.Properties["Params"]).Value;
                        insertCommand.Parameters["@Result"].Value = ((ScalarValue)logEvent.Properties["Result"]).Value;
                        insertCommand.Parameters["@Status"].Value = ((ScalarValue)logEvent.Properties["Status"]).Value;
                        insertCommand.Parameters["@ErrorMsg"].Value =
                            ((ScalarValue)logEvent.Properties["ErrorMsg"]).Value;
                        insertCommand.Parameters["@OS"].Value = ((ScalarValue)logEvent.Properties["OS"]).Value;
                        insertCommand.Parameters["@Browser"].Value =
                            ((ScalarValue)logEvent.Properties["Browser"]).Value;
                        insertCommand.Parameters["@CreationTime"].Value =
                            ((ScalarValue)logEvent.Properties["CreationTime"]).Value;
                        insertCommand.Parameters["@CreatorId"].Value =
                            ((ScalarValue)logEvent.Properties["CreatorId"]).Value;
                        insertCommand.Parameters["@Creator"].Value =
                            ((ScalarValue)logEvent.Properties["Creator"]).Value;
                        insertCommand.Parameters["@IsDeleted"].Value =
                            ((ScalarValue)logEvent.Properties["IsDeleted"]).Value;
                    }
                    else if (_logType == LogType.Login)
                    {
                        insertCommand.Parameters["@LogId"].Value = ((ScalarValue)logEvent.Properties["LogId"]).Value;
                        insertCommand.Parameters["@LoginName"].Value =
                            ((ScalarValue)logEvent.Properties["LoginName"]).Value;
                        insertCommand.Parameters["@IpAddress"].Value =
                            ((ScalarValue)logEvent.Properties["IpAddress"]).Value;
                        insertCommand.Parameters["@Location"].Value =
                            ((ScalarValue)logEvent.Properties["Location"]).Value;
                        insertCommand.Parameters["@OS"].Value = ((ScalarValue)logEvent.Properties["OS"]).Value;
                        insertCommand.Parameters["@Status"].Value = ((ScalarValue)logEvent.Properties["Status"]).Value;
                        insertCommand.Parameters["@PromptMsg"].Value =
                            ((ScalarValue)logEvent.Properties["PromptMsg"]).Value;
                        insertCommand.Parameters["@Browser"].Value =
                            ((ScalarValue)logEvent.Properties["Browser"]).Value;
                        insertCommand.Parameters["@CreationTime"].Value =
                            ((ScalarValue)logEvent.Properties["CreationTime"]).Value;
                        insertCommand.Parameters["@CreatorId"].Value =
                            ((ScalarValue)logEvent.Properties["CreatorId"]).Value;
                        insertCommand.Parameters["@Creator"].Value =
                            ((ScalarValue)logEvent.Properties["Creator"]).Value;
                        insertCommand.Parameters["@IsDeleted"].Value =
                            ((ScalarValue)logEvent.Properties["IsDeleted"]).Value;
                    }

                    await insertCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                await tr.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine(ex.Message);

                return false;
            }
        }
    }
}