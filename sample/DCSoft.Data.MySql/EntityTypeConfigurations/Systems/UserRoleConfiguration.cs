using DCSoft.Domain.Models.Systems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Systems
{
    /// <summary>
    /// 用户角色类型配置
    /// </summary>
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("sys_user_role", t => t.HasComment("用户角色"));
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey("UserId", "RoleId");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(t => t.UserId)
                .HasColumnName("UserId")
                .HasComment("用户标识");
            builder.Property(t => t.RoleId)
                .HasColumnName("RoleId")
                .HasComment("角色标识");
        }
    }
}