using DCSoft.Domain.Models.Systems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCSoft.Data.MySql.EntityTypeConfigurations.Systems
{
    /// <summary>
    /// 用户类型配置
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigTable(builder);
            ConfigId(builder);
            ConfigProperties(builder);
        }

        /// <summary>
        /// 配置表
        /// </summary>
        private void ConfigTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("sys_user", t => t.HasComment("用户"));
        }

        /// <summary>
        /// 配置标识
        /// </summary>
        private void ConfigId(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.Id)
                .HasColumnName("UserId")
                .HasComment("用户标识");

            builder.HasMany(p => p.Roles).WithMany(p => p.Users).UsingEntity<UserRole>(
                u => u.HasOne(c => c.Role).WithMany(c => c.UserRoles).HasForeignKey(c => c.RoleId),
                u => u.HasOne(c => c.User).WithMany(c => c.UserRoles).HasForeignKey(c => c.UserId),
                u => { u.HasKey(c => new { c.UserId, c.RoleId }); });
        }

        /// <summary>
        /// 配置属性
        /// </summary>
        private void ConfigProperties(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.DepartmentId)
                .HasColumnName("DepartmentId")
                .HasComment("部门标识");
            builder.Property(t => t.UserType)
                .HasColumnName("UserType")
                .HasComment("用户类型");
            builder.Property(t => t.NickName)
                .HasColumnName("NickName")
                .HasComment("昵称");
            builder.Property(t => t.UserName)
                .HasColumnName("UserName")
                .HasComment("用户名");
            builder.Property(t => t.NormalizedUserName)
                .HasColumnName("NormalizedUserName")
                .HasComment("标准化用户名");
            builder.Property(t => t.Email)
                .HasColumnName("Email")
                .HasComment("安全邮箱");
            builder.Property(t => t.NormalizedEmail)
                .HasColumnName("NormalizedEmail")
                .HasComment("标准化邮箱");
            builder.Property(t => t.EmailConfirmed)
                .HasColumnName("EmailConfirmed")
                .HasComment("邮箱已确认");
            builder.Property(t => t.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasComment("安全手机");
            builder.Property(t => t.PhoneNumberConfirmed)
                .HasColumnName("PhoneNumberConfirmed")
                .HasComment("手机已确认");
            builder.Property(t => t.Password)
                .HasColumnName("Password")
                .HasComment("密码");
            builder.Property(t => t.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasComment("密码散列");
            builder.Property(t => t.SafePassword)
                .HasColumnName("SafePassword")
                .HasComment("安全码");
            builder.Property(t => t.SafePasswordHash)
                .HasColumnName("SafePasswordHash")
                .HasComment("安全码散列");
            builder.Property(t => t.TwoFactorEnabled)
                .HasColumnName("TwoFactorEnabled")
                .HasComment("启用两阶段认证");
            builder.Property(t => t.Enabled)
                .HasColumnName("Enabled")
                .HasComment("启用");
            builder.Property(t => t.DisabledTime)
                .HasColumnName("DisabledTime")
                .HasComment("冻结时间");
            builder.Property(t => t.LockoutEnabled)
                .HasColumnName("LockoutEnabled")
                .HasComment("启用锁定");
            builder.Property(t => t.LockoutEnd)
                .HasColumnName("LockoutEnd")
                .HasComment("锁定截止");
            builder.Property(t => t.AccessFailedCount)
                .HasColumnName("AccessFailedCount")
                .HasComment("登陆失败次数");
            builder.Property(t => t.LoginCount)
                .HasColumnName("LoginCount")
                .HasComment("登陆次数");
            builder.Property(t => t.RegisterIp)
                .HasColumnName("RegisterIp")
                .HasComment("注册Ip");
            builder.Property(t => t.LastLoginTime)
                .HasColumnName("LastLoginTime")
                .HasComment("上次登陆时间");
            builder.Property(t => t.LastLoginIp)
                .HasColumnName("LastLoginIp")
                .HasComment("上次登陆Ip");
            builder.Property(t => t.CurrentLoginTime)
                .HasColumnName("CurrentLoginTime")
                .HasComment("本次登陆时间");
            builder.Property(t => t.CurrentLoginIp)
                .HasColumnName("CurrentLoginIp")
                .HasComment("本次登陆Ip");
            builder.Property(t => t.SecurityStamp)
                .HasColumnName("SecurityStamp")
                .HasComment("安全戳");
            builder.Property(t => t.Avatar)
                .HasColumnName("Avatar")
                .HasComment("头像");
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