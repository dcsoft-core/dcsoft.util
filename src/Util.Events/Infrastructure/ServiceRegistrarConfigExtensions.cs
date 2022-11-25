﻿using Util.Infrastructure;

namespace Util.Events.Infrastructure {
    /// <summary>
    /// 本地事件总线服务注册器配置扩展
    /// </summary>
    public static class ServiceRegistrarConfigExtensions {
        /// <summary>
        /// 启用本地事件总线服务注册器
        /// </summary>
        /// <param name="config">服务注册器配置</param>
        public static ServiceRegistrarConfig EnableLocalEventBusServiceRegistrar( this ServiceRegistrarConfig config ) {
            config.Enable( LocalEventBusServiceRegistrar.GetId() );
            return config;
        }

        /// <summary>
        ///禁用本地事件总线服务注册器
        /// </summary>
        /// <param name="config">服务注册器配置</param>
        public static ServiceRegistrarConfig DisableLocalEventBusServiceRegistrar( this ServiceRegistrarConfig config ) {
            config.Disable( LocalEventBusServiceRegistrar.GetId() );
            return config;
        }
    }
}
