using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core.Models;

namespace Util.Tools.Offices.Excel
{
    /// <summary>
    /// Excel工厂
    /// </summary>
    public interface IExcelFactory
    {
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <typeparam name="T">文件实体</typeparam>
        /// <returns></returns>
        Task<ImportResult<T>> Import<T>(string filePath) where T : class, new();

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="stream"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ImportResult<T>> Import<T>(Stream stream) where T : class, new();

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="dataItems">数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ExportFileInfo> Export<T>(string fileName, ICollection<T> dataItems) where T : class, new();

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dataItems">数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>文件二进制数组</returns>
        Task<byte[]> ExportAsByteArray<T>(ICollection<T> dataItems) where T : class, new();

        /// <summary>
        /// 生成导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<ExportFileInfo> GenerateImportTemplate<T>(string fileName) where T : class, new();

        /// <summary>
        /// 生成导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<byte[]> GenerateImportTemplate<T>() where T : class, new();
    }
}