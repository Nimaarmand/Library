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
        [Description("آیدی دسته بندی کتاب خالی می باشد")]
        IdNotFound = 1003,
        [Description("کتابی با این شناسه وجود ندارد")]
        BookIdNotFound = 1004,
        [Description("کتاب رزرو شده است")]
        BookNotAvailable =1005,
        [Description("کاربر با این شناسه وجود ندارد")]
        UserNotFound =1006,
        [Description("ورودی ها معتبر نیستند دوباره چک کنید")]
        InvalidInput =1006,
        [Description("شناسه رزرو پیدا نشد")]
        ReservationIdNotFound = 1007,
        RoleHasAssociatedUsers = 1004,
    }
}
