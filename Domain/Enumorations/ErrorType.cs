using System.ComponentModel;

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
        BookNotAvailable = 1005,
        [Description("کاربر با این شناسه وجود ندارد")]
        UserNotFound = 1006,
        [Description("ورودی ها معتبر نیستند دوباره چک کنید")]
        InvalidInput = 1006,
        [Description("شناسه رزرو پیدا نشد")]
        ReservationIdNotFound = 1007,
        [Description("کتاب در دسترس نمی باشد")]
        IsAvailable=1011,
        RoleHasAssociatedUsers = 1004,
        [Description("شماره تلفن تکراری است")]
        PhoneNumberAlreadyExists = 1008,
        [Description("شماره تلفن نادرست است")]
        InvalidPhoneNumber = 1009,
        [Description("حساب شما فعال نیست")]
        AccountInactive = 1010,
        [Description("نام کاربری یا رمز عبور اشتباه است")]
        InvalidCredentials = 1012,
        [Description("حساب کاربری شما قفل شده است")]
        AccountLocked = 1013,

        AccountNotAllowed = 1014
    }
}
