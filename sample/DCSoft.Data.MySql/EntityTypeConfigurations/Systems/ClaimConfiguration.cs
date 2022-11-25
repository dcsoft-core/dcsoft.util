using DCSoft.Domain.Models.Systems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Systems
{
    /// <summary>
    /// 声明类型配置
    /// </summary>
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<Claim> builder)
        {
            builder.ToTable("sys_claim").HasComment("声明");
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<Claim> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("ClaimId")
                .HasComment("声明标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<Claim> builder)
        {
            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasComment("声明名称");
            builder.Property(t => t.Enabled)
                .HasColumnName("Enabled")
                .HasComment("启用");
            builder.Property(t => t.SortId)
                .HasColumnName("SortId")
                .HasComment("排序号");
            builder.Property(t => t.Remark)
                .HasColumnName("Remark")
                .HasComment("备注");
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