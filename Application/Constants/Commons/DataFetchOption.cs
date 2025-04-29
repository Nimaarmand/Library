using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants.Commons
{
    public enum DataFetchOption
    {
        [Description("همه")]
        All = 0,
        [Description("فعال")]
        Active = 1,
        [Description("غیرفعال")]
        DeActive = 2,
    }
}
