using DCSoft.Domain.Models.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Commons
{
    /// <summary>
    /// 公共附件类型配置
    /// </summary>
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("com_attachment", t => t.HasComment("公共附件"));
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("FileId")
                .HasComment("文件标识");
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(t => t.ObjectId)
                .HasColumnName("ObjectId")
                .HasComment("关联对象标识");
            builder.Property(t => t.ObjectType)
                .HasColumnName("ObjectType")
                .HasComment("关联对象类型");
            builder.Property(t => t.TypeCode)
                .HasColumnName("TypeCode")
                .HasComment("类型代码");
            builder.Property(t => t.TypeName)
                .HasColumnName("TypeName")
                .HasComment("类型名称");
            builder.Property(t => t.ActualName)
                .HasColumnName("ActualName")
                .HasComment("附件名称");
            builder.Property(t => t.FileName)
                .HasColumnName("FileName")
                .HasComment("文件名称");
            builder.Property(t => t.MimeType)
                .HasColumnName("MimeType")
                .HasComment("MIME类型");
            builder.Property(t => t.FileSize)
                .HasColumnName("FileSize")
                .HasComment("文件大小");
            builder.Property(t => t.ExtensionName)
                .HasColumnName("ExtensionName")
                .HasComment("扩展名");
            builder.Property(t => t.FilePath)
                .HasColumnName("FilePath")
                .HasComment("附件路径");
            builder.Property(t => t.RequestPath)
                .HasColumnName("RequestPath")
                .HasComment("请求路径");
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