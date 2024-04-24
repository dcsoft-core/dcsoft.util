/*
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
using System.Collections.Generic;

namespace Serilog.Sinks.MySQL.Options
{
	/// <summary>
	/// 列选项
	/// </summary>
	public class MySqlColumnOptions
	{
		/// <summary>
		/// Id列选项
		/// </summary>
		public IdColumnOptions IdColumnOptions { get; set; }

		/// <summary>
		/// 时间列选项
		/// </summary>
		public TimeStampColumnOptions TimeStampColumnOptions { get; set; }
		
		/// <summary>
		/// 日志事件列选项
		/// </summary>
		public LogEventColumnOptions LogEventColumnOptions { get; set; }
		
		/// <summary>
		/// 消息列选项
		/// </summary>
		public MessageColumnOptions MessageColumnOptions { get; set; }
		
		/// <summary>
		///	消息模板列选项
		/// </summary>
		public MessageTemplateColumnOptions MessageTemplateColumnOptions { get; set; }
		
		/// <summary>
		///	级别列选项
		/// </summary>
		public LevelColumnOptions LevelColumnOptions { get; set; }
		
		/// <summary>
		/// 异常列选项
		/// </summary>
		public ExceptionColumnOptions ExceptionColumnOptions { get; set; }
		
		/// <summary>
		/// 附加列选项
		/// </summary>
		public IList<IColumnOptions> AdditionalColumns { get; set; } = new List<IColumnOptions>();
		
		/// <summary>
		/// 全部
		/// </summary>
		public IList<IColumnOptions> All
		{
			get
			{
				var list = new List<IColumnOptions>
				{
					IdColumnOptions,
					TimeStampColumnOptions,
					LogEventColumnOptions,
					MessageColumnOptions,
					MessageTemplateColumnOptions,
					LevelColumnOptions,
					ExceptionColumnOptions
				};
				list.AddRange(AdditionalColumns);
				return list;
			}
		}

		/// <summary>
		/// Returns the default set of columns that will be used for logging to.
		///		Id
		///		TimeStamp
		///		LogEvent
		///		Message
		///		MessageTemplate
		///		Level
		///		Exception
		/// </summary>
		public static MySqlColumnOptions Default => new MySqlColumnOptions
		{
			IdColumnOptions = new IdColumnOptions { Name = "Id" },
			TimeStampColumnOptions = new TimeStampColumnOptions { Name = "TimeStamp" },
			LogEventColumnOptions = new LogEventColumnOptions { Name = "Event" },
			MessageColumnOptions = new MessageColumnOptions { Name = "Message" },
			MessageTemplateColumnOptions = new MessageTemplateColumnOptions { Name = "Template" },
			LevelColumnOptions = new LevelColumnOptions { Name = "Level" },
			ExceptionColumnOptions = new ExceptionColumnOptions { Name = "Exception" }
		};

		/// <summary>
		/// Specifies properties for a single column. 
		/// </summary>
		/// <param name="columnOptions">The column options to be used.</param>
		/// <returns>The options object with the new configuration, or an exception when no name is specified.</returns>
		public MySqlColumnOptions With(IColumnOptions columnOptions)
		{
			if (string.IsNullOrWhiteSpace(columnOptions?.Name))
			{
				throw new InvalidOperationException("When specifying a column, a name must be included.");
			}

			if (columnOptions is CustomColumnOptions)
			{
				AdditionalColumns.Add(columnOptions);
				return this;
			}
			var type = columnOptions.GetType().Name;
			GetType().GetProperty(type)?.SetValue(this, columnOptions);
			return this;
		}

		/// <summary>
		/// Excludes a column that is enabled by <see cref="MySqlColumnOptions.Default"/>.
		/// </summary>
		/// <param name="columnOptions">An empty column options object of the to-be excluded type.</param>
		/// <returns>The options with the new configuration, or an exception if the name is specified.</returns>
		public MySqlColumnOptions Exclude(IColumnOptions columnOptions)
		{
			if (columnOptions == null || columnOptions.Name != null)
			{
				throw new InvalidOperationException("When excluding a column, an empty column object is expected (with no name).");
			}
			var type = columnOptions.GetType().Name;
			GetType().GetProperty(type)?.SetValue(this, columnOptions);
			return this;
		}

	}
	/// <summary>
	/// 列选项
	/// </summary>
	public interface IColumnOptions
	{
		/// <summary>
		/// 数据类型
		/// </summary>
		DataType DataType { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		string Name { get; set; }
	}
	/// <summary>
	/// Id列选项
	/// </summary>
	public class IdColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 数据类型
		/// </summary>
		public DataType DataType { get; set; } = new DataType(Kind.AutoIncrementInt);
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 级别列选项
	/// </summary>
	public class LevelColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 默认列长度
		/// </summary>
		public static int DefaultColumnLength => 16;
		/// <summary>
		/// 数据类型
		/// </summary>
		public DataType DataType { get; set; } = new DataType(Kind.Varchar) { Length = DefaultColumnLength };
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 异常列选项
	/// </summary>
	public class ExceptionColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 数据类型
		/// </summary>
		public DataType DataType { get; set; } = new DataType(Kind.Text);
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 消息列选项
	/// </summary>
	public class MessageColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 数据类型
		/// </summary>
		public DataType DataType { get; set; } = new DataType(Kind.Text);
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 消息模板列选项
	/// </summary>
	public class MessageTemplateColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 数据类
		/// </summary>
		public DataType DataType { get; set; } = new DataType(Kind.Text);
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 时间列选项
	/// </summary>
	public class TimeStampColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 是否使用Utc
		/// </summary>
		public bool UseUtc { get; set; }
		/// <summary>
		/// 数据类型
		/// </summary>
		public DataType DataType { get; set; } = new DataType(Kind.TimeStamp);
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
	}
	/// <summary>
	/// 自定义列选项
	/// </summary>
	public class CustomColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 类型
		/// </summary>
		public DataType DataType { get; set; }
		/// <summary>
		/// 值
		/// </summary>
		public object Value { get; set; }
	}
	
	/// <summary>
	/// 数据类型
	/// </summary>
	public class DataType
	{
		/// <summary>
		/// 默认列长度
		/// </summary>
		public static int DefaultColumnLength => 65535;
		/// <summary>
		/// 类型构造
		/// </summary>
		/// <param name="type"></param>
		/// <param name="length"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public DataType(Kind type, int length = 0)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), "Data length cannot be negative.");
			}

			Type = type;
			// as the default parameter must be a compile-time constant,
			// we set it to zero, and check for zero to assign the default
			Length = length == 0
				? DefaultColumnLength 
				: length;
		}
		/// <summary>
		/// 类型
		/// </summary>
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public Kind Type { get; set; }
		/// <summary>
		/// 长度
		/// </summary>
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public int Length { get; set; }
	}
	
	/// <summary>
	/// 种类
	/// </summary>
	public enum Kind
	{
		/// <summary>
		/// 文本
		/// </summary>
		Text,
		/// <summary>
		/// 字符型
		/// </summary>
		Varchar,
		/// <summary>
		/// 日期型
		/// </summary>
		DateTime,
		/// <summary>
		/// 时间戳
		/// </summary>
		TimeStamp,
		/// <summary>
		/// Unix时间
		/// </summary>
		UnixTime,
		/// <summary>
		/// Guid
		/// </summary>
		Guid,
		/// <summary>
		/// 自增长
		/// </summary>
		AutoIncrementInt
	}

	/// <summary>
	/// 日志事件列选项
	/// </summary>
	public class LogEventColumnOptions : IColumnOptions
	{
		/// <summary>
		/// 事件序列化程序
		/// </summary>
		public EventSerializer EventSerializer { get; set; } = EventSerializer.Json;
		/// <summary>
		/// 数据类型
		/// </summary>
		public DataType DataType { get; set; } = new DataType(Kind.Text);
		/// <summary>
		/// 名称
		/// </summary>
		public string Name { get; set; }

	}

	/// <summary>
	/// Only Json is not supported at this time.
	/// </summary>
	public enum EventSerializer
	{
		/// <summary>
		/// JSON
		/// </summary>
		Json
	}

}
