using System;

namespace Finances.WebApp.Models
{
    public class ErrorViewModel
    {
        public bool CustomError { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}