using System.Collections.Generic;

namespace Payscrow.WebUI.Areas.Business.Common
{
    public class BaseDataTableResult<T>
    {
        public BaseDataTableResult(int draw, int recordsTotal, int recordsFiltered, IEnumerable<T> data)
        {
            Draw = draw;
            RecordsTotal = recordsTotal;
            RecordsFiltered = recordsFiltered;
            Data = data;
        }

        public int Draw { get; }
        public int RecordsTotal { get; }
        public int RecordsFiltered { get; }
        public IEnumerable<T> Data { get; }
    }
}