using System;
using System.ComponentModel.DataAnnotations;
using Util.Applications.Dtos;

namespace DCSoft.Applications.Dtos.Logs
{
    /// <summary>
    /// 操作日志参数
    /// </summary>
    public class OperateDto : DtoBase
    {
        /// <summary>
        /// 模块标题
        ///</summary>
        [Display(Name = "模块标题")]
        [Required]
        [MaxLength(64)]
        public string Title { get; set; }

        /// <summary>
        /// 业务类型（0其它 1新增 2修改 3删除）
        ///</summary>
        [Display(Name = "业务类型（0其它 1新增 2修改 3删除）")]
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 请求方式
        ///</summary>
        [Display(Name = "请求方式")]
        [Required]
        [MaxLength(16)]
        public string HttpMethod { get; set; }

        /// <summary>
        /// 方法名称
        ///</summary>
        [Display(Name = "方法名称")]
        [Required]
        [MaxLength(128)]
        public string Method { get; set; }

        /// <summary>
        /// 请求URL
        ///</summary>
        [Display(Name = "请求URL")]
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 用户类型（0其它 1后台用户 2手机端用户）
        ///</summary>
        [Display(Name = "用户类型（0其它 1后台用户 2手机端用户）")]
        public int? UrlType { get; set; }

        /// <summary>
        /// 主机地址
        ///</summary>
        [Display(Name = "主机地址")]
        [MaxLength(32)]
        public string IpAddress { get; set; }

        /// <summary>
        /// 操作地点
        ///</summary>
        [Display(Name = "操作地点")]
        [MaxLength(256)]
        public string Location { get; set; }

        /// <summary>
        /// 请求参数
        ///</summary>
        [Display(Name = "请求参数")]
        public string Params { get; set; }

        /// <summary>
        /// 返回值
        ///</summary>
        [Display(Name = "返回值")]
        public string Result { get; set; }

        /// <summary>
        /// 操作状态（0正常 1异常）
        ///</summary>
        [Display(Name = "操作状态（0正常 1异常）")]
        public int? Status { get; set; }

        /// <summary>
        /// 错误信息
        ///</summary>
        [Display(Name = "错误信息")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// 操作系统
        ///</summary>
        [Display(Name = "操作系统")]
        [MaxLength(128)]
        public string OS { get; set; }

        /// <summary>
        /// 浏览器类型
        ///</summary>
        [Display(Name = "浏览器类型")]
        [MaxLength(1024)]
        public string Browser { get; set; }

        /// <summary>
        /// 创建时间
        ///</summary>
        [Display(Name = "创建时间")]
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 创建者标识
        ///</summary>
        [Display(Name = "创建者标识")]
        public Guid? CreatorId { get; set; }

        /// <summary>
        /// 创建者
        ///</summary>
        [Display(Name = "创建者")]
        [MaxLength(256)]
        public string Creator { get; set; }

        /// <summary>
        /// 最后修改时间
        ///</summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 最后修改者标识
        ///</summary>
        [Display(Name = "最后修改者标识")]
        public Guid? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改者
        ///</summary>
        [Display(Name = "最后修改者")]
        [MaxLength(256)]
        public string LastModifier { get; set; }

        /// <summary>
        /// 版本号
        ///</summary>
        [Display(Name = "版本号")]
        public byte[] Version { get; set; }
    }
}