﻿using Util.Helpers;
using Util.Ui.Configs;
using Util.Ui.NgZorro.Components.Forms.Helpers;
using Util.Ui.NgZorro.Components.Selects.Configs;

namespace Util.Ui.NgZorro.Components.Selects.Helpers {
    /// <summary>
    /// 选择器服务
    /// </summary>
    public class SelectService {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly SelectConfig _config;

        /// <summary>
        /// 初始化选择器服务
        /// </summary>
        /// <param name="config">配置</param>
        public SelectService( SelectConfig config ) {
            _config = config;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init() {
            InitExtendId();
            LoadExpression();
            InitValidationService();
            InitFormShareService();
            InitFormItemShareService();
        }

        /// <summary>
        /// 初始化扩展标识
        /// </summary>
        private void InitExtendId() {
            _config.ExtendId = $"x_{GetId()}";
        }

        /// <summary>
        /// 获取标识
        /// </summary>
        private string GetId() {
            var result = _config.GetValue( UiConst.Id );
            if ( result.IsEmpty() )
                return Id.Create();
            return result;
        }

        /// <summary>
        /// 加载表达式
        /// </summary>
        private void LoadExpression() {
            var loader = new SelectExpressionLoader();
            loader.Load( _config );
        }

        /// <summary>
        /// 初始化验证服务
        /// </summary>
        private void InitValidationService() {
            var service = new ValidationService( _config );
            service.Init();
        }

        /// <summary>
        /// 初始化表单共享服务
        /// </summary>
        private void InitFormShareService() {
            var service = new FormShareService( _config );
            service.Init();
        }

        /// <summary>
        /// 初始化表单项共享服务
        /// </summary>
        private void InitFormItemShareService() {
            var service = new FormItemShareService( _config );
            service.Init();
            service.InitId();
        }
    }
}
