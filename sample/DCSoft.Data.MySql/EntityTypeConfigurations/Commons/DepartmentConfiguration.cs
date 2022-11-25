using DCSoft.Domain.Models.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Commons
{
    /// <summary>
    /// 组织机构类型配置
    /// </summary>
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("com_department").HasComment("组织机构");
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<Department> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("DepartmentId")
                .HasComment("组织机构标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<Department> builder)
        {
            builder.Property(t => t.Code)
                .HasColumnName("Code")
                .HasComment("部门编码");
            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasComment("部门名称");
            builder.Property(t => t.PinYin)
                .HasColumnName("PinYin")
                .HasComment("拼音简码");
            builder.Property(t => t.Remark)
                .HasColumnName("Remark")
                .HasComment("备注");
            builder.Property(t => t.Extend)
                .HasColumnName("Extend")
                .HasComment("扩展");
            builder.Property(t => t.ParentId)
                .HasColumnName("ParentId")
                .HasComment("父标识");
            builder.Property(t => t.Path)
                .HasColumnName("Path")
                .HasComment("路径");
            builder.Property(t => t.Level)
                .HasColumnName("Level")
                .HasComment("层级");
            builder.Property(t => t.SortId)
                .HasColumnName("SortId")
                .HasComment("排序号");
            builder.Property(t => t.Enabled)
                .HasColumnName("Enabled")
                .HasComment("启用");
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