using System.Collections.Generic;
using CoApp.Packaging.Common;

namespace CoApp.VisualStudio.Dialog.Providers
{
    internal class LoadPageResult
    {
        public LoadPageResult(IEnumerable<IPackage> packages, int pageNumber, int totalCount)
        {
            Packages = packages;
            PageNumber = pageNumber;
            TotalCount = totalCount;
        }

        public IEnumerable<IPackage> Packages
        {
            get;
            private set;
        }

        public int TotalCount
        {
            get;
            private set;
        }

        public int PageNumber
        {
            get;
            private set;
        }
    }
}
