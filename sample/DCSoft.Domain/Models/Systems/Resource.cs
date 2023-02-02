using Util;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 资源
    /// </summary>
    public partial class Resource
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            InitPinYin();
        }

        /// <summary>
        /// 初始化拼音简码
        /// </summary>
        public void InitPinYin()
        {
            PinYin = Util.Helpers.String.PinYin(Name);
        }

        /// <summary>
        /// 是否外部地址
        /// </summary>
        public bool IsExternalUrl()
        {
            if (Uri.IsEmpty())
                return false;
            if (Uri.StartsWith("http"))
                return true;
            return false;
        }
    }
}