namespace PhotoCloud.Model.Core
{
    /// <summary>
    /// Data Model for API Responses.
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// Response status code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Response data.
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// A description message for the response.
        /// </summary>
        public string? Message { get; set; }
    }
}
