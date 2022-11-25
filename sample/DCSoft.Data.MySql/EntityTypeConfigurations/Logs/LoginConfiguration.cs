using DCSoft.Domain.Models.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Logs
{
    /// <summary>
    /// 登录日志类型配置
    /// </summary>
    public class LoginConfiguration : IEntityTypeConfiguration<Login>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<Login> builder)
        {
            builder.ToTable("log_login").HasComment("日志");
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<Login> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("LogId")
                .HasComment("日志标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<Login> builder)
        {
            builder.Property(t => t.LoginName)
                .HasColumnName("LoginName")
                .HasComment("登录帐号");
            builder.Property(t => t.IpAddress)
                .HasColumnName("IpAddress")
                .HasComment("登录IP地址");
            builder.Property(t => t.Location)
                .HasColumnName("Location")
                .HasComment("登录地点");
            builder.Property(t => t.OS)
                .HasColumnName("OS")
                .HasComment("操作系统");
            builder.Property(t => t.Status)
                .HasColumnName("Status")
                .HasComment("登录状态（0成功 1失败）");
            builder.Property(t => t.PromptMsg)
                .HasColumnName("PromptMsg")
                .HasComment("提示消息");
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