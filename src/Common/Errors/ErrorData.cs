namespace Common.Errors
{
    public class ErrorData
    {
        public List<ErrorDataItem> errors { get; set; }

        public class ErrorDataItem
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public string Description { get; set; }
        }
    }
}
