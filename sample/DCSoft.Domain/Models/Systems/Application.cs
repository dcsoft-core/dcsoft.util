namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public partial class Application
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            InitName();
        }

        /// <summary>
        /// 初始化显示名称
        /// </summary>
        public void InitName()
        {
        }
    }
}