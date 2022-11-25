// Copyright 2019 Zethian Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.Batch;
using Serilog.Sinks.Extensions;

namespace Serilog.Sinks.MySQL
{
    internal class MySqlSink : BatchProvider, ILogEventSink
    {
        private readonly string _connectionString;
        private readonly bool _storeTimestampInUtc;
        private readonly string _tableName;
        private readonly LogType _logType;

        public MySqlSink(
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

        private MySqlConnection GetSqlConnection()
        {
            try
            {
                var conn = new MySqlConnection(_connectionString);
                conn.Open();

                return conn;
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine(ex.Message);

                return null;
            }
        }

        private MySqlCommand GetInsertCommand(MySqlConnection sqlConnection)
        {
            var cmd = sqlConnection.CreateCommand();
            var tableCommandBuilder = new StringBuilder();

            if (_logType == LogType.Sys)
            {
                tableCommandBuilder.Append($"INSERT INTO  `{_tableName}` (");
                tableCommandBuilder.Append("`Timestamp`, `Level`, `Template`, `Message`, `Exception`, `Properties`) ");
                tableCommandBuilder.Append("VALUES (@ts, @level,@template, @msg, @ex, @prop)");

                cmd.CommandText = tableCommandBuilder.ToString();

                cmd.Parameters.Add(new MySqlParameter("@ts", MySqlDbType.VarChar));
                cmd.Parameters.Add(new MySqlParameter("@level", MySqlDbType.VarChar));
                cmd.Parameters.Add(new MySqlParameter("@template", MySqlDbType.VarChar));
                cmd.Parameters.Add(new MySqlParameter("@msg", MySqlDbType.VarChar));
                cmd.Parameters.Add(new MySqlParameter("@ex", MySqlDbType.VarChar));
                cmd.Parameters.Add(new MySqlParameter("@prop", MySqlDbType.VarChar));
            }
            else if (_logType == LogType.Operate)
            {
                tableCommandBuilder.Append($"INSERT INTO  `{_tableName}` (");
                tableCommandBuilder.Append("`LogId`, `Title`, `Type`, `HttpMethod`, `Method`, `Url`, `UrlType`, `IpAddress`, `Location`, `Params`, `Result`, `Status`, `ErrorMsg`, `OS`, " +
                                           " `Browser`, `CreationTime`, `CreatorId`, `Creator`, `LastModificationTime`,`LastModifierId`, `LastModifier`, `IsDeleted`, `Version`) ");
                tableCommandBuilder.Append("VALUES (@LogId, @Title, @Type, @HttpMethod, @Method, @Url, @UrlType, @IpAddress, @Location, @Params, @Result, @Status, @ErrorMsg, " +
                                           "@OS, @Browser, @CreationTime, @CreatorId, @Creator, @LastModificationTime,@LastModifierId, @LastModifier, @IsDeleted, @Version)");

                cmd.CommandText = tableCommandBuilder.ToString();

                cmd.Parameters.Add(new MySqlParameter("@LogId", MySqlDbType.VarChar, 36));
                cmd.Parameters.Add(new MySqlParameter("@Title", MySqlDbType.VarChar, 64));
                cmd.Parameters.Add(new MySqlParameter("@Type", MySqlDbType.Int32));
                cmd.Parameters.Add(new MySqlParameter("@HttpMethod", MySqlDbType.VarChar, 16));
                cmd.Parameters.Add(new MySqlParameter("@Method", MySqlDbType.VarChar, 128));
                cmd.Parameters.Add(new MySqlParameter("@Url", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@UrlType", MySqlDbType.Int32));
                cmd.Parameters.Add(new MySqlParameter("@IpAddress", MySqlDbType.VarChar, 32));
                cmd.Parameters.Add(new MySqlParameter("@Location", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@Params", MySqlDbType.LongText));
                cmd.Parameters.Add(new MySqlParameter("@Result", MySqlDbType.LongText));
                cmd.Parameters.Add(new MySqlParameter("@Status", MySqlDbType.Int32));
                cmd.Parameters.Add(new MySqlParameter("@ErrorMsg", MySqlDbType.LongText));
                cmd.Parameters.Add(new MySqlParameter("@OS", MySqlDbType.VarChar, 128));
                cmd.Parameters.Add(new MySqlParameter("@Browser", MySqlDbType.VarChar, 1024));
                cmd.Parameters.Add(new MySqlParameter("@CreationTime", MySqlDbType.DateTime));
                cmd.Parameters.Add(new MySqlParameter("@CreatorId", MySqlDbType.VarChar, 36));
                cmd.Parameters.Add(new MySqlParameter("@Creator", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@LastModificationTime", MySqlDbType.DateTime));
                cmd.Parameters.Add(new MySqlParameter("@LastModifierId", MySqlDbType.VarChar, 36));
                cmd.Parameters.Add(new MySqlParameter("@LastModifier", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@IsDeleted", MySqlDbType.Bit));
                cmd.Parameters.Add(new MySqlParameter("@Version", MySqlDbType.LongBlob));
            }
            else if (_logType == LogType.Login)
            {
                tableCommandBuilder.Append($"INSERT INTO `{_tableName}` (");
                tableCommandBuilder.Append("`LogId`, `LoginName`, `IpAddress`, `Location`, `OS`, `Status`, `PromptMsg`, `Browser`, `CreationTime`, `CreatorId`, " +
                                           " `Creator`, `LastModificationTime`,`LastModifierId`, `LastModifier`, `IsDeleted`, `Version`) ");
                tableCommandBuilder.Append("VALUES (@LogId, @LoginName, @IpAddress, @Location, @OS, @Status, @PromptMsg, @Browser, @CreationTime, @CreatorId, " +
                                           "@Creator, @LastModificationTime,@LastModifierId, @LastModifier, @IsDeleted, @Version)");

                cmd.CommandText = tableCommandBuilder.ToString();

                cmd.Parameters.Add(new MySqlParameter("@LogId", MySqlDbType.VarChar, 36));
                cmd.Parameters.Add(new MySqlParameter("@LoginName", MySqlDbType.VarChar, 64));
                cmd.Parameters.Add(new MySqlParameter("@IpAddress", MySqlDbType.VarChar, 32));
                cmd.Parameters.Add(new MySqlParameter("@Location", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@OS", MySqlDbType.VarChar, 128));
                cmd.Parameters.Add(new MySqlParameter("@Status", MySqlDbType.Int32));
                cmd.Parameters.Add(new MySqlParameter("@PromptMsg", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@Browser", MySqlDbType.VarChar, 1024));
                cmd.Parameters.Add(new MySqlParameter("@CreationTime", MySqlDbType.DateTime));
                cmd.Parameters.Add(new MySqlParameter("@CreatorId", MySqlDbType.VarChar, 36));
                cmd.Parameters.Add(new MySqlParameter("@Creator", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@LastModificationTime", MySqlDbType.DateTime));
                cmd.Parameters.Add(new MySqlParameter("@LastModifierId", MySqlDbType.VarChar, 36));
                cmd.Parameters.Add(new MySqlParameter("@LastModifier", MySqlDbType.VarChar, 256));
                cmd.Parameters.Add(new MySqlParameter("@IsDeleted", MySqlDbType.Bit));
                cmd.Parameters.Add(new MySqlParameter("@Version", MySqlDbType.LongBlob));
            }

            return cmd;
        }

        private void CreateTable(MySqlConnection sqlConnection)
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
                    tableCommandBuilder.Append("`LastModificationTime` datetime(6) null comment '最后修改时间',");
                    tableCommandBuilder.Append("`LastModifierId` char(36) charset ascii null comment '最后修改者标识',");
                    tableCommandBuilder.Append("`LastModifier` varchar(256) null comment '最后修改者',");
                    tableCommandBuilder.Append("`IsDeleted` tinyint(1) not null comment '是否删除',");
                    tableCommandBuilder.Append("`Version` longblob null comment '版本号'");
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
                    tableCommandBuilder.Append("`LastModificationTime` datetime(6) null comment '最后修改时间',");
                    tableCommandBuilder.Append("`LastModifierId` char(36) charset ascii null comment '最后修改者标识',");
                    tableCommandBuilder.Append("`LastModifier` varchar(256) null comment '最后修改者',");
                    tableCommandBuilder.Append("`IsDeleted` tinyint(1) not null comment '是否删除',");
                    tableCommandBuilder.Append("`Version` longblob null comment '版本号'");
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
                await using var tr = await sqlCon.BeginTransactionAsync().ConfigureAwait(false);
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
                        insertCommand.Parameters["@prop"].Value = logEvent.Properties.Count > 0 ? logEvent.Properties.Json() : string.Empty;
                    }
                    else if (_logType == LogType.Operate)
                    {
                        insertCommand.Parameters["@LogId"].Value = ((ScalarValue)logEvent.Properties["LogId"]).Value;
                        insertCommand.Parameters["@Title"].Value = ((ScalarValue)logEvent.Properties["Title"]).Value;
                        insertCommand.Parameters["@Type"].Value = ((ScalarValue)logEvent.Properties["Type"]).Value;
                        insertCommand.Parameters["@HttpMethod"].Value = ((ScalarValue)logEvent.Properties["HttpMethod"]).Value;
                        insertCommand.Parameters["@Method"].Value = ((ScalarValue)logEvent.Properties["Method"]).Value;
                        insertCommand.Parameters["@Url"].Value = ((ScalarValue)logEvent.Properties["Url"]).Value;
                        insertCommand.Parameters["@UrlType"].Value = ((ScalarValue)logEvent.Properties["UrlType"]).Value;
                        insertCommand.Parameters["@IpAddress"].Value = ((ScalarValue)logEvent.Properties["IpAddress"]).Value;
                        insertCommand.Parameters["@Location"].Value = ((ScalarValue)logEvent.Properties["Location"]).Value;
                        insertCommand.Parameters["@Params"].Value = ((ScalarValue)logEvent.Properties["Params"]).Value;
                        insertCommand.Parameters["@Result"].Value = ((ScalarValue)logEvent.Properties["Result"]).Value;
                        insertCommand.Parameters["@Status"].Value = ((ScalarValue)logEvent.Properties["Status"]).Value;
                        insertCommand.Parameters["@ErrorMsg"].Value = ((ScalarValue)logEvent.Properties["ErrorMsg"]).Value;
                        insertCommand.Parameters["@OS"].Value = ((ScalarValue)logEvent.Properties["OS"]).Value;
                        insertCommand.Parameters["@Browser"].Value = ((ScalarValue)logEvent.Properties["Browser"]).Value;
                        insertCommand.Parameters["@CreationTime"].Value = ((ScalarValue)logEvent.Properties["CreationTime"]).Value;
                        insertCommand.Parameters["@CreatorId"].Value = ((ScalarValue)logEvent.Properties["CreatorId"]).Value;
                        insertCommand.Parameters["@Creator"].Value = ((ScalarValue)logEvent.Properties["Creator"]).Value;
                        insertCommand.Parameters["@LastModificationTime"].Value = ((ScalarValue)logEvent.Properties["LastModificationTime"]).Value;
                        insertCommand.Parameters["@LastModifierId"].Value = ((ScalarValue)logEvent.Properties["LastModifierId"]).Value;
                        insertCommand.Parameters["@LastModifier"].Value = ((ScalarValue)logEvent.Properties["LastModifier"]).Value;
                        insertCommand.Parameters["@IsDeleted"].Value = ((ScalarValue)logEvent.Properties["IsDeleted"]).Value;
                        insertCommand.Parameters["@Version"].Value = ((ScalarValue)logEvent.Properties["Version"]).Value;
                    }
                    else if (_logType == LogType.Login)
                    {
                        insertCommand.Parameters["@LogId"].Value = ((ScalarValue)logEvent.Properties["LogId"]).Value;
                        insertCommand.Parameters["@LoginName"].Value = ((ScalarValue)logEvent.Properties["LoginName"]).Value;
                        insertCommand.Parameters["@IpAddress"].Value = ((ScalarValue)logEvent.Properties["IpAddress"]).Value;
                        insertCommand.Parameters["@Location"].Value = ((ScalarValue)logEvent.Properties["Location"]).Value;
                        insertCommand.Parameters["@OS"].Value = ((ScalarValue)logEvent.Properties["OS"]).Value;
                        insertCommand.Parameters["@Status"].Value = ((ScalarValue)logEvent.Properties["Status"]).Value;
                        insertCommand.Parameters["@PromptMsg"].Value = ((ScalarValue)logEvent.Properties["PromptMsg"]).Value;
                        insertCommand.Parameters["@Browser"].Value = ((ScalarValue)logEvent.Properties["Browser"]).Value;
                        insertCommand.Parameters["@CreationTime"].Value = ((ScalarValue)logEvent.Properties["CreationTime"]).Value;
                        insertCommand.Parameters["@CreatorId"].Value = ((ScalarValue)logEvent.Properties["CreatorId"]).Value;
                        insertCommand.Parameters["@Creator"].Value = ((ScalarValue)logEvent.Properties["Creator"]).Value;
                        insertCommand.Parameters["@LastModificationTime"].Value = ((ScalarValue)logEvent.Properties["LastModificationTime"]).Value;
                        insertCommand.Parameters["@LastModifierId"].Value = ((ScalarValue)logEvent.Properties["LastModifierId"]).Value;
                        insertCommand.Parameters["@LastModifier"].Value = ((ScalarValue)logEvent.Properties["LastModifier"]).Value;
                        insertCommand.Parameters["@IsDeleted"].Value = ((ScalarValue)logEvent.Properties["IsDeleted"]).Value;
                        insertCommand.Parameters["@Version"].Value = ((ScalarValue)logEvent.Properties["Version"]).Value;
                    }

                    await insertCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                tr.Commit();

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
