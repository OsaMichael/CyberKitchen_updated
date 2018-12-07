using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cyber_Kitchen.Interface.Utils
{
    public interface IExcelProcessor
    {
        Stream Generate<T>(Dictionary<string, IEnumerable<T>> Sheets);
        IEnumerable<T> Load<T>(Stream excelStream, string sheet = null) where T : class, new();
        Stream Generate(IEnumerable<DataTable> Sheets);
        Stream GenerateResult(DataTable Tables);
        IEnumerable<DataTable> Load(Stream excelStream);
        Task<IEnumerable<DataTable>> LoadAsync(Stream excelStream);
        MemoryStream Generate<T>(IEnumerable<T> varlist, string sheetName);
    }
}