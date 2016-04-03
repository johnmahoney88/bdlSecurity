using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bdlSecurity.ViewModels
{
    public class DataPager<T>
    {
        private int _currentPageNum;
        public int RowCount { get; private set; }
        public IEnumerable<T> PageData { get; private set; }
        
        public int TotalPages
        {
            get
            {
                return (RowCount / PageSize) + ((RowCount % PageSize > 0) ? 1 : 0);

            }
        }
        public int PageSize { get; private set; }
        public int CurrentPageNumber { get { return _currentPageNum; } }
        public int PreviousPageNumber {  get { return _currentPageNum > 1 ? _currentPageNum - 1 : 1; } }
        public int NextPageNumber { get { return (_currentPageNum == TotalPages) ? TotalPages  : _currentPageNum + 1; } }

        public DataPager(IEnumerable<T> data, int pagesize)
        {
            this.PageSize = pagesize;
            this.PageData = data;
            this.RowCount = data.Count();
            _currentPageNum = 1;
        }

        public IEnumerable<T> GetPage(int pagenum)
        {

            if (pagenum > TotalPages) pagenum = TotalPages;
            _currentPageNum = pagenum;
            return CurrentPage;
            
        }

        public void SetPageNum(int page)
        {
            _currentPageNum = page;
        }

        public IEnumerable<T> CurrentPage
        {
            get
            {

                return PageData.Skip((_currentPageNum-1) * PageSize).Take(PageSize);
            }
        }



    }
}
