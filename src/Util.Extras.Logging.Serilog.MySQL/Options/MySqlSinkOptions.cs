﻿/*
 * Copyright 2021 Teun van Schagen

 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at

 *     http://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace Serilog.Sinks.MySQL.Options
{
	/// <summary>
	/// Sink options for the MySQL sink.
	/// </summary>
	public class MySqlSinkOptions
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName { get; set; }
		/// <summary>
		/// 是否创建表
		/// </summary>
		public bool CreateTable { get; set; }
		/// <summary>
		/// 初始化选项
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="createTable"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public MySqlSinkOptions(
		  string tableName,
		  bool createTable = true)
		{
			TableName = tableName ?? throw new ArgumentNullException(nameof(tableName), "Table name must be specified.");
			CreateTable = createTable;
		}

		/// <summary>
		/// Returns the default options for the sink, which is the default table name of `Logs`.
		/// </summary>
		public static MySqlSinkOptions Default => new MySqlSinkOptions("Logs");

	}
}
