using DCSoft.Apis.Base;
using DCSoft.Web.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using Util.Extensions;

namespace DCSoft.Apis.Admin.Commons
{
    /// <summary>
    /// 演示控制器
    /// </summary>
    [Router("commons", "demo")]
    public class DemoController : BaseController
    {
        #region 初始化控制器

        /// <summary>
        /// 初始化控制器
        /// </summary>
        public DemoController()
        {
        }

        #endregion

        #region 属性定义

        #endregion

        #region 演示图表数据

        /// <summary>
        /// 演示图表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic")]
        [Login]
        public IActionResult StatisticsAsync()
        {
            var random = new Random();
            var totalOnline = random.Next(9999);
            var totalOffline = random.Next(9999);
            var totalRent = random.Next(9999);
            var total = totalOnline + totalOffline + totalRent;

            var result = new
                { Total = total, TotalOnline = totalOnline, TotalOffline = totalOffline, TotalRent = totalRent };
            return Success(result);
        }

        #endregion

        #region 在线状态图表数据

        /// <summary>
        /// 在线状态图表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("online-status")]
        [Login]
        public IActionResult OnlineStatusAsync()
        {
            var random = new Random();
            IList<object> list = new List<object>();
            for (var i = 1; i < 13; i++)
            {
                list.Add(new
                {
                    Type = "在线",
                    Name = $"2021-06-{i.ToString().PadLeft(2, '0')}",
                    Value = random.Next(9999)
                });
                list.Add(new
                {
                    Type = "离线",
                    Name = $"2021-06-{i.ToString().PadLeft(2, '0')}",
                    Value = random.Next(9999)
                });
                list.Add(new
                {
                    Type = "静置",
                    Name = $"2021-06-{i.ToString().PadLeft(2, '0')}",
                    Value = random.Next(9999)
                });
                list.Add(new
                {
                    Type = "流控",
                    Name = $"2021-06-{i.ToString().PadLeft(2, '0')}",
                    Value = random.Next(9999)
                });
            }

            return Success(list);
        }

        #endregion

        #region 设备出租图表数据

        /// <summary>
        /// 设备出租图表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("device-rent")]
        [Login]
        public IActionResult DeviceRentAsync()
        {
            var random = new Random();
            IList<object> list = new List<object>();
            var value1 = random.Next(9999);
            var value2 = random.Next(9999);
            var value3 = value1 + value2;
            list.Add(new
            {
                Type = "已租赁",
                Name = "已租赁",
                Value = value1,
                Percent = ((decimal)value1 / (decimal)value3).ToString("F2").ToDecimal()
            });
            list.Add(new
            {
                Type = "未租赁",
                Name = "未租赁",
                Value = value2,
                Percent = ((decimal)value2 / (decimal)value3).ToString("F2").ToDecimal()
            });

            return Success(list);
        }

        #endregion

        #region 设备接入图表数据

        /// <summary>
        /// 设备接入图表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("device-access")]
        [Login]
        public IActionResult DeviceAccessAsync()
        {
            var random = new Random();
            IList<object> list = new List<object>();
            for (var i = 1; i <= DateTime.Now.Month; i++)
            {
                list.Add(new
                {
                    Type = $"{i}月",
                    Name = $"{i}月",
                    Value = random.Next(9999)
                });
            }

            return Success(list);
        }

        #endregion
    }
}