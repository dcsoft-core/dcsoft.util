using Util;

namespace DCSoft.Domain.Models.Systems
{
    /// <summary>
    /// 角色
    /// </summary>
    public partial class Role
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            InitType();
            InitPinYin();
            InitNormalizedName();
        }

        /// <summary>
        /// 初始化类型
        /// </summary>
        public void InitType()
        {
            if (Type.IsEmpty())
                Type = "Role";
        }

        /// <summary>
        /// 初始化拼音简码
        /// </summary>
        public void InitPinYin()
        {
            PinYin = Util.Helpers.String.PinYin(Name);
        }

        /// <summary>
        /// 初始化正常名称
        /// </summary>
        public void InitNormalizedName()
        {
            NormalizedName = Name.ToUpper();
        }
    }
}