using System.ComponentModel;

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
