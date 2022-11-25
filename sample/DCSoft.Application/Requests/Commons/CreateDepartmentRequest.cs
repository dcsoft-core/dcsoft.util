using System;
using System.ComponentModel.DataAnnotations;

namespace DCSoft.Applications.Requests.Commons
{
    /// <summary>
    /// 创建部门参数
    /// </summary>
    public class CreateDepartmentRequest
    {
        /// <summary>
        /// 父标识
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required(ErrorMessage = "部门名称不能为空")]
        [StringLength(200)]
        [Display(Name = "部门名称")]
        public string Name { get; set; }

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