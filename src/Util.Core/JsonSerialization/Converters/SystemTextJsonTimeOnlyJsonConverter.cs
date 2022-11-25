
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable once CheckNamespace
namespace Util.JsonSerialization;

/// <summary>
/// TimeOnly 类型序列化
/// </summary>
public class SystemTextJsonTimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SystemTextJsonTimeOnlyJsonConverter()
    {
        Format ??= "HH:mm:ss";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public SystemTextJsonTimeOnlyJsonConverter(string format)
    {
        Format = format;
    }

    /// <summary>
    /// 时间格式化格式
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.Parse(reader.GetString());
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}

/// <summary>
/// TimeOnly? 类型序列化
/// </summary>
public class SystemTextJsonNullableTimeOnlyJsonConverter : JsonConverter<TimeOnly?>
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public SystemTextJsonNullableTimeOnlyJsonConverter()
    {
        Format ??= "HH:mm:ss";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="format"></param>
    public SystemTextJsonNullableTimeOnlyJsonConverter(string format)
    {
        Format = format;
    }

    /// <summary>
    /// 时间格式化格式
    /// </summary>
    public string Format { get; private set; }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.TryParse(reader.GetString(), out TimeOnly time) ? time : null;
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
    {
        if (value == null) writer.WriteNullValue();
        else writer.WriteStringValue(value.Value.ToString(Format));
    }
}