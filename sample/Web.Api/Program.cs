using DCSoft.Data;
using DCSoft.Data.MySql;
using DCSoft.Integration.Upload;
using DCSoft.Web.Core.Handlers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Util;
using Util.Aop;
using Util.Data.EntityFrameworkCore;
using Util.Data.Sql;
using Util.Extras.Applications.Middles;
using Util.Logging.Serilog;

//����WebӦ�ó���������
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);

//���÷���
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultBufferSize = 10_0000;//���ؽϴ������������л�ʱ��ضϣ�ԭ��Ĭ�ϻ�������С�����ֽ�Ϊ��λ��Ϊ16384��
    options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters("yyyy-MM-dd HH:mm:ss");
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // ����ѭ������ ��.NET 6֧��
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ����ϴ�����
builder.Services.AddUploadConfig();

//����Util
builder.Host.AddUtil(options => options
    .UseAop()
    .UseSerilog(t => t.AddExceptionless())
    .UseMySqlQuery(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseMySqlExecutor(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseMySqlUnitOfWork<IDataUnitOfWork, DataUnitOfWork>(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//����WebӦ�ó���
var app = builder.Build();

//��������ܵ�
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

// ʹ���ϴ�����
app.UseUploadConfig();

//����Ӧ��
app.Run();