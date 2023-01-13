using DCSoft.Domain.Models.Systems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Systems
{
    /// <summary>
    /// 权限类型配置
    /// </summary>
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("sys_permission", t => t.HasComment("权限"));
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("PermissionId")
                .HasComment("权限标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(t => t.RoleId)
                .HasColumnName("RoleId")
                .HasComment("角色标识");
            builder.Property(t => t.ResourceId)
                .HasColumnName("ResourceId")
                .HasComment("资源标识");
            builder.Property(t => t.IsDeny)
                .HasColumnName("IsDeny")
                .HasComment("拒绝");
            builder.Property(t => t.Sign)
                .HasColumnName("Sign")
                .HasComment("签名");
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