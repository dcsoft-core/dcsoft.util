namespace DCSoft.Domain.Models.Commons
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public partial class Department
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