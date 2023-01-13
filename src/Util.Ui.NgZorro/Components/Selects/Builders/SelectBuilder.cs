﻿using Util.Ui.Angular.Configs;
using Util.Ui.Angular.Extensions;
using Util.Ui.Configs;
using Util.Ui.NgZorro.Components.Base;
using Util.Ui.NgZorro.Components.Containers.Builders;
using Util.Ui.NgZorro.Components.Selects.Configs;
using Util.Ui.NgZorro.Enums;

namespace Util.Ui.NgZorro.Components.Selects.Builders {
    /// <summary>
    /// 选择器标签生成器
    /// </summary>
    public class SelectBuilder : FormControlBuilderBase<SelectBuilder> {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly SelectConfig _config;

        /// <summary>
        /// 初始化选择器标签生成器
        /// </summary>
        /// <param name="config">配置</param>
        public SelectBuilder( SelectConfig config ) : base( config, "nz-select" ) {
            _config = config;
        }

        /// <summary>
        /// 配置比较算法函数
        /// </summary>
        public SelectBuilder CompareWith() {
            AttributeIfNotEmpty( "[compareWith]", _config.GetValue( UiConst.CompareWith ) );
            return this;
        }

        /// <summary>
        /// 配置清除搜索值
        /// </summary>
        public SelectBuilder AutoClearSearchValue() {
            AttributeIfNotEmpty( "[nzAutoClearSearchValue]", _config.GetBoolValue( UiConst.AutoClearSearchValue ) );
            AttributeIfNotEmpty( "[nzAutoClearSearchValue]", _config.GetValue( AngularConst.BindAutoClearSearchValue ) );
            return this;
        }

        /// <summary>
        /// 配置允许清除
        /// </summary>
        public SelectBuilder AllowClear() {
            AttributeIfNotEmpty( "[nzAllowClear]", _config.GetBoolValue( UiConst.AllowClear ) );
            AttributeIfNotEmpty( "[nzAllowClear]", _config.GetValue( AngularConst.BindAllowClear ) );
            return this;
        }

        /// <summary>
        /// 配置移除边框
        /// </summary>
        public SelectBuilder Borderless() {
            AttributeIfNotEmpty( "[nzBorderless]", _config.GetBoolValue( UiConst.Borderless ) );
            AttributeIfNotEmpty( "[nzBorderless]", _config.GetValue( AngularConst.BindBorderless ) );
            return this;
        }

        /// <summary>
        /// 配置打开下拉菜单
        /// </summary>
        public SelectBuilder Open() {
            AttributeIfNotEmpty( "[nzOpen]", _config.GetBoolValue( UiConst.Open ) );
            AttributeIfNotEmpty( "[nzOpen]", _config.GetValue( AngularConst.BindOpen ) );
            AttributeIfNotEmpty( "[(nzOpen)]", _config.GetValue( AngularConst.BindonOpen ) );
            return this;
        }

        /// <summary>
        /// 配置自动聚焦
        /// </summary>
        public SelectBuilder AutoFocus() {
            AttributeIfNotEmpty( "[nzAutoFocus]", _config.GetBoolValue( UiConst.AutoFocus ) );
            AttributeIfNotEmpty( "[nzAutoFocus]", _config.GetValue( AngularConst.BindAutoFocus ) );
            return this;
        }

        /// <summary>
        /// 配置禁用
        /// </summary>
        public SelectBuilder Disabled() {
            AttributeIfNotEmpty( "[nzDisabled]", _config.GetBoolValue( UiConst.Disabled ) );
            AttributeIfNotEmpty( "[nzDisabled]", _config.GetValue( AngularConst.BindDisabled ) );
            return this;
        }

        /// <summary>
        /// 配置下拉菜单样式类名
        /// </summary>
        public SelectBuilder DropdownClassName() {
            AttributeIfNotEmpty( "nzDropdownClassName", _config.GetValue( UiConst.DropdownClassName ) );
            AttributeIfNotEmpty( "[nzDropdownClassName]", _config.GetValue( AngularConst.BindDropdownClassName ) );
            return this;
        }

        /// <summary>
        /// 配置下拉菜单样式
        /// </summary>
        public SelectBuilder DropdownStyle() {
            AttributeIfNotEmpty( "[nzDropdownStyle]", _config.GetValue( UiConst.DropdownStyle ) );
            return this;
        }

        /// <summary>
        /// 配置下拉菜单和选择器同宽
        /// </summary>
        public SelectBuilder DropdownMatchSelectWidth() {
            AttributeIfNotEmpty( "[nzDropdownMatchSelectWidth]", _config.GetBoolValue( UiConst.DropdownMatchSelectWidth ) );
            AttributeIfNotEmpty( "[nzDropdownMatchSelectWidth]", _config.GetValue( AngularConst.BindDropdownMatchSelectWidth ) );
            return this;
        }

        /// <summary>
        /// 配置自定义模板
        /// </summary>
        public SelectBuilder CustomTemplate() {
            AttributeIfNotEmpty( "[nzCustomTemplate]", _config.GetValue( UiConst.CustomTemplate ) );
            return this;
        }

        /// <summary>
        /// 配置服务端搜索
        /// </summary>
        public SelectBuilder ServerSearch() {
            AttributeIfNotEmpty( "[nzServerSearch]", _config.GetBoolValue( UiConst.ServerSearch ) );
            AttributeIfNotEmpty( "[nzServerSearch]", _config.GetValue( AngularConst.BindServerSearch ) );
            return this;
        }

        /// <summary>
        /// 配置过滤项函数
        /// </summary>
        public SelectBuilder FilterOption() {
            AttributeIfNotEmpty( "[nzFilterOption]", _config.GetValue( UiConst.FilterOption ) );
            return this;
        }

        /// <summary>
        /// 配置最大多选数量
        /// </summary>
        public SelectBuilder MaxMultipleCount() {
            AttributeIfNotEmpty( "nzMaxMultipleCount", _config.GetValue( UiConst.MaxMultipleCount ) );
            AttributeIfNotEmpty( "[nzMaxMultipleCount]", _config.GetValue( AngularConst.BindMaxMultipleCount ) );
            return this;
        }

        /// <summary>
        /// 配置选择模式
        /// </summary>
        public SelectBuilder Mode() {
            AttributeIfNotEmpty( "nzMode", _config.GetValue<SelectMode?>( UiConst.Mode )?.Description() );
            AttributeIfNotEmpty( "[nzMode]", _config.GetValue( AngularConst.BindMode ) );
            return this;
        }

        /// <summary>
        /// 配置空列表默认内容
        /// </summary>
        public SelectBuilder NotFoundContent() {
            AttributeIfNotEmpty( "nzNotFoundContent", _config.GetValue( UiConst.NotFoundContent ) );
            AttributeIfNotEmpty( "[nzNotFoundContent]", _config.GetValue( AngularConst.BindNotFoundContent ) );
            return this;
        }

        /// <summary>
        /// 配置占位提示
        /// </summary>
        public SelectBuilder Placeholder() {
            AttributeIfNotEmpty( "nzPlaceHolder", _config.GetValue( UiConst.Placeholder ) );
            AttributeIfNotEmpty( "[nzPlaceHolder]", _config.GetValue( AngularConst.BindPlaceholder ) );
            return this;
        }

        /// <summary>
        /// 配置显示箭头
        /// </summary>
        public SelectBuilder ShowArrow() {
            AttributeIfNotEmpty( "[nzShowArrow]", _config.GetBoolValue( UiConst.ShowArrow ) );
            AttributeIfNotEmpty( "[nzShowArrow]", _config.GetValue( AngularConst.BindShowArrow ) );
            return this;
        }

        /// <summary>
        /// 配置显示搜索
        /// </summary>
        public SelectBuilder ShowSearch() {
            AttributeIfNotEmpty( "[nzShowSearch]", _config.GetBoolValue( UiConst.ShowSearch ) );
            AttributeIfNotEmpty( "[nzShowSearch]", _config.GetValue( AngularConst.BindShowSearch ) );
            return this;
        }

        /// <summary>
        /// 配置选择框大小
        /// </summary>
        public SelectBuilder Size() {
            AttributeIfNotEmpty( "nzSize", _config.GetValue<InputSize?>( UiConst.Size )?.Description() );
            AttributeIfNotEmpty( "[nzSize]", _config.GetValue( AngularConst.BindSize ) );
            return this;
        }

        /// <summary>
        /// 配置后缀图标
        /// </summary>
        public SelectBuilder SuffixIcon() {
            AttributeIfNotEmpty( "nzSuffixIcon", _config.GetValue<AntDesignIcon?>( UiConst.SuffixIcon )?.Description() );
            AttributeIfNotEmpty( "[nzSuffixIcon]", _config.GetValue( AngularConst.BindSuffixIcon ) );
            return this;
        }

        /// <summary>
        /// 配置清除图标
        /// </summary>
        public SelectBuilder RemoveIcon() {
            AttributeIfNotEmpty( "[nzRemoveIcon]", _config.GetValue( UiConst.RemoveIcon ) );
            return this;
        }

        /// <summary>
        /// 配置清空图标
        /// </summary>
        public SelectBuilder ClearIcon() {
            AttributeIfNotEmpty( "[nzClearIcon]", _config.GetValue( UiConst.ClearIcon ) );
            return this;
        }

        /// <summary>
        /// 配置选中项图标
        /// </summary>
        public SelectBuilder MenuItemSelectedIcon() {
            AttributeIfNotEmpty( "[nzMenuItemSelectedIcon]", _config.GetValue( UiConst.MenuItemSelectedIcon ) );
            return this;
        }

        /// <summary>
        /// 配置自动分词分隔符
        /// </summary>
        public SelectBuilder TokenSeparators() {
            AttributeIfNotEmpty( "[nzTokenSeparators]", _config.GetValue( UiConst.TokenSeparators ) );
            return this;
        }

        /// <summary>
        /// 配置加载状态
        /// </summary>
        public SelectBuilder Loading() {
            return Loading( _config.GetValue( UiConst.Loading ) );
        }

        /// <summary>
        /// 配置加载状态
        /// </summary>
        public SelectBuilder Loading( string loading ) {
            AttributeIfNotEmpty( "[nzLoading]", loading );
            return this;
        }

        /// <summary>
        /// 配置最大标签数量
        /// </summary>
        public SelectBuilder MaxTagCount() {
            AttributeIfNotEmpty( "nzMaxTagCount", _config.GetValue( UiConst.MaxTagCount ) );
            AttributeIfNotEmpty( "[nzMaxTagCount]", _config.GetValue( AngularConst.BindMaxTagCount ) );
            return this;
        }

        /// <summary>
        /// 配置最大标签占位符
        /// </summary>
        public SelectBuilder MaxTagPlaceholder() {
            AttributeIfNotEmpty( "[nzMaxTagPlaceholder]", _config.GetValue( UiConst.MaxTagPlaceholder ) );
            return this;
        }

        /// <summary>
        /// 配置选项列表
        /// </summary>
        public SelectBuilder Options() {
            AttributeIfNotEmpty( "[nzOptions]", _config.GetValue( UiConst.Options ) );
            return this;
        }

        /// <summary>
        /// 配置选项高度
        /// </summary>
        public SelectBuilder OptionHeightPx() {
            AttributeIfNotEmpty( "nzOptionHeightPx", _config.GetValue( UiConst.OptionHeightPx ) );
            AttributeIfNotEmpty( "[nzOptionHeightPx]", _config.GetValue( AngularConst.BindOptionHeightPx ) );
            return this;
        }

        /// <summary>
        /// 配置最大显示选项数量
        /// </summary>
        public SelectBuilder OptionOverflowSize() {
            AttributeIfNotEmpty( "nzOptionOverflowSize", _config.GetValue( UiConst.OptionOverflowSize ) );
            AttributeIfNotEmpty( "[nzOptionOverflowSize]", _config.GetValue( AngularConst.BindOptionOverflowSize ) );
            return this;
        }

        /// <summary>
        /// 配置自动加载
        /// </summary>
        private SelectBuilder AutoLoad() {
            AttributeIfNotEmpty( "[autoLoad]", _config.GetBoolValue( UiConst.AutoLoad ) );
            return this;
        }

        /// <summary>
        /// 配置查询参数
        /// </summary>
        private SelectBuilder QueryParam() {
            AttributeIfNotEmpty( "[(queryParam)]", _config.GetValue( UiConst.QueryParam ) );
            return this;
        }

        /// <summary>
        /// 配置排序条件
        /// </summary>
        private SelectBuilder Sort() {
            AttributeIfNotEmpty( "order", _config.GetValue( UiConst.Sort ) );
            AttributeIfNotEmpty( "[order]", _config.GetValue( AngularConst.BindSort ) );
            return this;
        }

        /// <summary>
        /// 配置Api地址
        /// </summary>
        private SelectBuilder Url() {
            AttributeIfNotEmpty( "url", _config.GetValue( UiConst.Url ) );
            AttributeIfNotEmpty( "[url]", _config.GetValue( AngularConst.BindUrl ) );
            return this;
        }

        /// <summary>
        /// 配置数据源
        /// </summary>
        private SelectBuilder Data() {
            AttributeIfNotEmpty( "[data]", _config.GetValue( UiConst.Data ) );
            return this;
        }

        /// <summary>
        /// 配置事件
        /// </summary>
        public SelectBuilder Events() {
            AttributeIfNotEmpty( "(nzOpenChange)", _config.GetValue( UiConst.OnOpenChange ) );
            AttributeIfNotEmpty( "(nzScrollToBottom)", _config.GetValue( UiConst.OnScrollToBottom ) );
            AttributeIfNotEmpty( "(nzOnSearch)", _config.GetValue( UiConst.OnSearch ) );
            AttributeIfNotEmpty( "(nzFocus)", _config.GetValue( UiConst.OnFocus ) );
            AttributeIfNotEmpty( "(nzBlur)", _config.GetValue( UiConst.OnBlur ) );
            return this;
        }

        /// <summary>
        /// 配置
        /// </summary>
        public override void Config() {
            base.Config();
            ConfigForm().Name().CompareWith().AutoClearSearchValue().AllowClear()
                .Borderless().Open().AutoFocus().Disabled()
                .DropdownClassName().DropdownStyle().DropdownMatchSelectWidth()
                .CustomTemplate().ServerSearch().FilterOption().MaxMultipleCount()
                .Mode().NotFoundContent().Placeholder()
                .ShowArrow().ShowSearch().Size()
                .SuffixIcon().RemoveIcon().ClearIcon().MenuItemSelectedIcon()
                .TokenSeparators().Loading().MaxTagCount().MaxTagPlaceholder()
                .Options().OptionHeightPx().OptionOverflowSize()
                .AutoLoad().QueryParam().Sort().Url().Data()
                .Events();
            ConfigDefaultOptionText();
            EnableExtend();
        }

        /// <summary>
        /// 配置默认项文本
        /// </summary>
        private void ConfigDefaultOptionText() {
            var value = _config.AllAttributes[UiConst.DefaultOptionText]?.Value?.ToString();
            if ( string.IsNullOrEmpty( value ) )
                return;
            var optionBuilder = new OptionBuilder( _config );
            optionBuilder.Label( value );
            AppendContent( optionBuilder );
        }

        /// <summary>
        /// 启用扩展
        /// </summary>
        public SelectBuilder EnableExtend() {
            if ( IsEnableExtend() == false )
                return this;
            Attribute( $"#{_config.ExtendId}", "xSelectExtend" );
            Attribute( "x-select-extend" );
            EnableLoading();
            ConfigOption();
            ConfigOptionGroup();
            return this;
        }

        /// <summary>
        /// 是否启用基础扩展
        /// </summary>
        public bool IsEnableExtend() {
            if ( GetEnableExtend() == false ) {
                return false;
            }
            if ( GetEnableExtend() == true ||
                 GetUrl().IsEmpty() == false ||
                 GetBindUrl().IsEmpty() == false ||
                 GetData().IsEmpty() == false ) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取启用扩展属性
        /// </summary>
        private bool? GetEnableExtend() {
            return _config.GetValue<bool?>( UiConst.EnableExtend );
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        private string GetUrl() {
            return _config.GetValue( UiConst.Url );
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        private string GetBindUrl() {
            return _config.GetValue( AngularConst.BindUrl );
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        private string GetData() {
            return _config.GetValue( UiConst.Data );
        }

        /// <summary>
        /// 启用加载状态
        /// </summary>
        private void EnableLoading() {
            if ( GetUrl().IsEmpty() && GetBindUrl().IsEmpty() )
                return;
            Loading( $"{_config.ExtendId}.loading" );
        }

        /// <summary>
        /// 配置选项
        /// </summary>
        private void ConfigOption() {
            var containerBuilder = new ContainerBuilder( _config );
            containerBuilder.NgIf( $"!{_config.ExtendId}.isGroup" );
            var optionBuilder = new OptionBuilder( _config );
            containerBuilder.AppendContent( optionBuilder );
            optionBuilder.NgFor( $"let item of {_config.ExtendId}.options" );
            optionBuilder.BindLabel( "item.text" );
            optionBuilder.BindValue( "item.value" );
            optionBuilder.Disabled( "item.disabled" );
            AppendContent( containerBuilder );
        }

        /// <summary>
        /// 配置选项组
        /// </summary>
        private void ConfigOptionGroup() {
            var containerBuilder = new ContainerBuilder( _config );
            containerBuilder.NgIf( $"{_config.ExtendId}.isGroup" );
            var groupBuilder = new OptionGroupBuilder( _config );
            containerBuilder.AppendContent( groupBuilder );
            groupBuilder.NgFor( $"let group of {_config.ExtendId}.optionGroups" );
            groupBuilder.BindLabel( "group.text" );
            var optionBuilder = new OptionBuilder( _config );
            groupBuilder.AppendContent( optionBuilder );
            optionBuilder.NgFor( "let item of group.value" );
            optionBuilder.BindLabel( "item.text" );
            optionBuilder.BindValue( "item.value" );
            optionBuilder.Disabled( "item.disabled" );
            AppendContent( containerBuilder );
        }
    }
}