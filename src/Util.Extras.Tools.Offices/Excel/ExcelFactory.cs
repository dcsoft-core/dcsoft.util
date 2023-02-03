using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core.Models;
using Magicodes.ExporterAndImporter.Excel;

namespace Util.Extras.Tools.Offices.Excel
{
    /// <summary>
    /// Excel工厂
    /// </summary>
    public class ExcelFactory : IExcelFactory
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ExcelFactory()
        {
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <typeparam name="T">导入文件类型</typeparam>
        /// <returns></returns>
        public async Task<ImportResult<T>> Import<T>(string filePath) where T : class, new()
        {
            IExcelImporter importer = new ExcelImporter();
            var importResult = await importer.Import<T>(filePath);
            return importResult;
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <typeparam name="T">导入文件类型</typeparam>
        /// <returns></returns>
        public async Task<ImportResult<T>> Import<T>(Stream stream) where T : class, new()
        {
            IExcelImporter importer = new ExcelImporter();
            var importResult = await importer.Import<T>(stream);
            return importResult;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="dataItems">数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ExportFileInfo> Export<T>(string fileName, ICollection<T> dataItems) where T : class, new()
        {
            IExcelExporter exporter = new ExcelExporter();
            var exportResult = await exporter.Export(fileName, dataItems);
            return exportResult;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="dataItems">数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>文件二进制数据流</returns>
        public async Task<byte[]> ExportAsByteArray<T>(ICollection<T> dataItems) where T : class, new()
        {
            IExcelExporter exporter = new ExcelExporter();
            var exportResult = await exporter.ExportAsByteArray(dataItems);
            return exportResult;
        }

        /// <summary>
        /// 生成导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<byte[]> GenerateImportTemplate<T>() where T : class, new()
        {
            IExcelImporter importer = new ExcelImporter();
            return await importer.GenerateTemplateBytes<T>();
        }

        /// <summary>
        /// 生成导入模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<ExportFileInfo> GenerateImportTemplate<T>(string fileName) where T : class, new()
        {
            IExcelImporter importer = new ExcelImporter();
            return await importer.GenerateTemplate<T>(fileName);
        }
    }
}