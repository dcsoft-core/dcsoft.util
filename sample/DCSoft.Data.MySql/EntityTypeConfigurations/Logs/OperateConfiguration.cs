using DCSoft.Domain.Models.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Logs
{
    /// <summary>
    /// 操作日志类型配置
    /// </summary>
    public class OperateConfiguration : IEntityTypeConfiguration<Operate>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<Operate> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<Operate> builder)
        {
            builder.ToTable("log_operate", t => t.HasComment("日志"));
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<Operate> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("LogId")
                .HasComment("日志标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<Operate> builder)
        {
            builder.Property(t => t.Title)
                .HasColumnName("Title")
                .HasComment("模块标题");
            builder.Property(t => t.Type)
                .HasColumnName("Type")
                .HasComment("业务类型（0其它 1新增 2修改 3删除）");
            builder.Property(t => t.HttpMethod)
                .HasColumnName("HttpMethod")
                .HasComment("请求方式");
            builder.Property(t => t.Method)
                .HasColumnName("Method")
                .HasComment("方法名称");
            builder.Property(t => t.Url)
                .HasColumnName("Url")
                .HasComment("请求URL");
            builder.Property(t => t.UrlType)
                .HasColumnName("UrlType")
                .HasComment("用户类型（0其它 1后台用户 2手机端用户）");
            builder.Property(t => t.IpAddress)
                .HasColumnName("IpAddress")
                .HasComment("主机地址");
            builder.Property(t => t.Location)
                .HasColumnName("Location")
                .HasComment("操作地点");
            builder.Property(t => t.Params)
                .HasColumnName("Params")
                .HasComment("请求参数");
            builder.Property(t => t.Result)
                .HasColumnName("Result")
                .HasComment("返回值");
            builder.Property(t => t.Status)
                .HasColumnName("Status")
                .HasComment("操作状态（0正常 1异常）");
            builder.Property(t => t.ErrorMsg)
                .HasColumnName("ErrorMsg")
                .HasComment("错误信息");
            builder.Property(t => t.OS)
                .HasColumnName("OS")
                .HasComment("操作系统");
            builder.Property(t => t.Browser)
                .HasColumnName("Browser")
                .HasComment("浏览器类型");
            builder.Property(t => t.CreationTime)
                .HasColumnName("CreationTime")
                .HasComment("创建时间");
            builder.Property(t => t.CreatorId)
                .HasColumnName("CreatorId")
                .HasComment("创建者标识");
            builder.Property(t => t.Creator)
                .HasColumnName("Creator")
                .HasComment("创建者");
            builder.Property(t => t.LastModificationTime)
                .HasColumnName("LastModificationTime")
                .HasComment("最后修改时间");
            builder.Property(t => t.LastModifierId)
                .HasColumnName("LastModifierId")
                .HasComment("最后修改者标识");
            builder.Property(t => t.LastModifier)
                .HasColumnName("LastModifier")
                .HasComment("最后修改者");
        }
    }
}