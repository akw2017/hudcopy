using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.PageNavigationControl
{
    public partial class PageNavigation : IDataErrorInfo
    {
        class PageNavigationMetadata
        {
            [Range(0, Int32.MaxValue, ErrorMessage = "请输入大于0的整数。")]
            public int CurrentPage { get; set; }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                return this.ValidateProperty<PageNavigationMetadata>(columnName);
            }
        }
    }
}
