using System;
using Util.Configs;

namespace Util.Caching.CSRedisCore {
    /// <summary>
    /// CSRedisCore缓存操作扩展
    /// </summary>
    public static class OptionsExtensions {
        /// <summary>
        /// 配置CSRedisCore缓存操作
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="connectString">CSRedisCore缓存配置操作</param>
        public static Options UseCSRedisCore( this Options options, string connectString ) {
            options.AddExtension( new CSRedisCoreOptionsExtension(connectString) );
            return options;
        }
    }
}
