namespace book_management_c_sharp.Status
{
    public enum PublishStatus
    {
        UnPublished = 0,
        Published = 1
    }

    public static class PublishStatusExtensions
    {
        private static readonly Dictionary<PublishStatus, string> _values = new()
    {
        { PublishStatus.UnPublished, "未出版" },
        { PublishStatus.Published, "出版済" }
    };

        public static string ToValue(this PublishStatus status)
        {
            return _values.TryGetValue(status, out var value) ? value : status.ToString();
        }

        public static string ToValue(string code)
        {
            return Enum.TryParse(typeof(PublishStatus), code, out var status)
                ? ((PublishStatus)status).ToValue()
                : code;
        }
    }

}
