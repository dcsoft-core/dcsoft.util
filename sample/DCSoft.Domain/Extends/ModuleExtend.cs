﻿namespace DCSoft.Domain.Extends
{
    /// <summary>
    /// 模块扩展信息
    /// </summary>
    public class ModuleExtend
    {
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool? Expanded { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }
    }
}