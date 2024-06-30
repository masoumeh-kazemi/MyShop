using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace Common.Application
{
    public class OperationResult<TData>
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";

        public string Message { get; set; }
        public string Title { get; set; } = null;
        public OperationResultStatus Status { get; set; }
        public bool IsReload { get; set; } = false;

        public TData Data { get; set; }
        public static OperationResult<TData> Success(TData data, bool isReload = false)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Success,
                Message = SuccessMessage,
                Data = data,
                IsReload = isReload
                
            };
        }
        public static OperationResult<TData> NotFound()
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.NotFound,
                Title = "NotFound",
                Data = default,
            };
        }
        public static OperationResult<TData> Error(string message = ErrorMessage, bool isReload = false)
        {
            return new OperationResult<TData>()
            {
                Status = OperationResultStatus.Error,
                Title = "مشکلی در عملیات رخ داده",
                Data = default,
                Message = message,
                IsReload = isReload
            };
        }
    }
    public class OperationResult
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";
        public const string NotFoundMessage = "اطلاعات یافت نشد";
        public string Message { get; set; }
        public string Title { get; set; } = null;
        public bool IsReload { get; set; }
        public OperationResultStatus Status { get; set; }

        public static OperationResult Error(bool isReload = false)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = ErrorMessage,
                IsReload = isReload
            };
        }
        public static OperationResult NotFound(string message, bool isReload =false)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = message,
                IsReload = isReload
            };
        }
        public static OperationResult NotFound()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = NotFoundMessage,
            };
        }
        public static OperationResult Error(string message, bool isReload = false)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = message,
                IsReload = isReload
            };
        }
        public static OperationResult Error(string message, OperationResultStatus status, bool isReload = false)
        {
            return new OperationResult()
            {
                Status = status,
                Message = message,
                IsReload = isReload
            };
        }
        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = SuccessMessage,
            };
        }
        public static OperationResult Success(string message, bool isReload=false)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = message,
                IsReload = isReload
            };
        }
    }

    public enum OperationResultStatus
    {
        Error = 10,
        Success = 200,
        NotFound = 404
    }


}