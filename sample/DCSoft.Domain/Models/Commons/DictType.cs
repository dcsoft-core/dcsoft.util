namespace DCSoft.Domain.Models.Commons
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public partial class DictType
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
    }
}