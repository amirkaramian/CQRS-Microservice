using System.Collections.Generic;

namespace Payscrow.WebUI.Areas.Business.Common
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<Column> Columns { get; set; }
        public Search Search { get; set; }
        public List<Order> Order { get; set; }

        public int PageSize => Length;

        private int _pageIndex;

        public int PageIndex
        {
            get
            {
                if (_pageIndex == 0)
                {
                    _pageIndex = Start == 0 ? 1 : (Start / PageSize) + 1;
                }
                return _pageIndex;
            }
        }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}