using System;
using System.ComponentModel.DataAnnotations;

namespace DCSoft.Applications.Requests.Commons
{
    /// <summary>
    /// 创建字典参数
    /// </summary>
    public class CreateDictDataRequest
    {
        /// <summary>
        /// 父标识
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "编码不能为空")]
        [StringLength(200)]
        [Display(Name = "编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(128)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "类型不能为空")]
        [StringLength(128)]
        [Display(Name = "类型")]
        public string Type { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        [Display(Name = "备注")]
        public string Remark { get; set; }
    }
}