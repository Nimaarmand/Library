using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enumorations
{
    public enum ErrorType
    {
        [Description("خطا در برقراری ارتباط")]
        ConnectionFailed = 1001,

        [Description("اطلاعات یافت نشد")]
        ContactNotFound = 1002,
    }
}
