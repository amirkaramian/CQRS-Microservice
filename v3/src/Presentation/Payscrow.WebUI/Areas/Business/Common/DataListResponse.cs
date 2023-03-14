using System.Collections.Generic;

namespace Payscrow.WebUI.Areas.Business.Common
{
    public class DataListResponse<T>
    {
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}