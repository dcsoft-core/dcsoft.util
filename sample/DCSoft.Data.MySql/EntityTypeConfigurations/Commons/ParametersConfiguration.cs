using DCSoft.Domain.Models.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Commons
{
    /// <summary>
    /// 公共参数类型配置
    /// </summary>
    public class ParametersConfiguration : IEntityTypeConfiguration<Parameters>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<Parameters> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<Parameters> builder)
        {
            builder.ToTable("com_parameters", t => t.HasComment("公共参数"));
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<Parameters> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("ParamId")
                .HasComment("参数标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<Parameters> builder)
        {
            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasComment("参数名称");
            builder.Property(t => t.Title)
                .HasColumnName("Title")
                .HasComment("参数标题");
            builder.Property(t => t.Value)
                .HasColumnName("Value")
                .HasComment("参数内容");
            builder.Property(t => t.Type)
                .HasColumnName("Type")
                .HasComment("参数类型");
            builder.Property(t => t.SortId)
                .HasColumnName("SortId")
                .HasComment("排序");
            builder.Property(t => t.IsEdit)
                .HasColumnName("IsEdit")
                .HasComment("是否可编辑");
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