using System.Collections.Generic;

namespace Payscrow.DisputeResolution.Application.Queries
{
    public class BaseDataQueryResult<T>
    {
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}