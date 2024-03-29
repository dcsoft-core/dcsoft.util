using DCSoft.Data;
using DCSoft.Data.MySql;
using DCSoft.Integration.Upload;
using DCSoft.Web.Core.Handlers;
using System.Text.Json.Serialization;
using Util;
using Util.Aop;
using Util.Caching.EasyCaching;
using Util.Data.Dapper.Sql;
using Util.Data.EntityFrameworkCore;
using Util.Extras.Applications.Middles;
using Util.Logging.Serilog;

//创建Web应用程序生成器
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);

//配置服务
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultBufferSize = 10_0000; //返回较大数据数据序列化时会截断，原因：默认缓冲区大小（以字节为单位）为16384。
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // 忽略循环引用 仅.NET 6支持
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 添加上传配置
builder.Services.AddUploadConfig();

//配置Util
builder.Host
    .AsBuild()
    .AddAop()
    .AddRedisCache(builder.Configuration, "Redis")
    .AddSerilog()
    .AddMySqlQuery(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddMySqlExecutor(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddMySqlUnitOfWork<IDataUnitOfWork, DataUnitOfWork>(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddUtil();

//构建Web应用程序
var app = builder.Build();

//配置请求管道
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseEnableRequestRewind();
app.UseUnifyResultStatusCodes();
app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// 使用上传配置
app.UseUploadConfig();

//运行应用
app.Run();