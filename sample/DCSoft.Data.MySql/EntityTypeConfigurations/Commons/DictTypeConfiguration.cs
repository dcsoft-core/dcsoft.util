using DCSoft.Domain.Models.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Commons
{
    /// <summary>
    /// 字典类型类型配置
    /// </summary>
    public class DictTypeConfiguration : IEntityTypeConfiguration<DictType>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<DictType> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<DictType> builder)
        {
            builder.ToTable("com_dict_type", t => t.HasComment("字典类型"));
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<DictType> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("DictId")
                .HasComment("字典类型标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<DictType> builder)
        {
            builder.Property(t => t.Code)
                .HasColumnName("Code")
                .HasComment("编码");
            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasComment("名称");
            builder.Property(t => t.Enabled)
                .HasColumnName("Enabled")
                .HasComment("是否启用");
            builder.Property(t => t.PinYin)
                .HasColumnName("PinYin")
                .HasComment("拼音简码");
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