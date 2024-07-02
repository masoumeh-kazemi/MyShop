using Common.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace Shop.RazorPage.Pages.Infrastructure.RazorUtil;

public class BaseRazorPage : PageModel
{
    public IActionResult RedirectAndShowAlert(OperationResult result, IActionResult redirectPath)
    {
        var model = JsonConvert.SerializeObject(result);
        HttpContext.Response.Cookies.Append("SystemAlert", model);
        if (result.Status == OperationResultStatus.Error)
        {
            return Page();
        }

        return redirectPath;
    }

    public IActionResult RedirectAndShowAlert(OperationResult result, IActionResult redirectPath, IActionResult errorRedirectPath)
    {
        var model = JsonConvert.SerializeObject(result);
        HttpContext.Response.Cookies.Append("SystemAlert", model);
        if (result.Status == OperationResultStatus.Error)
        {
            return errorRedirectPath;
        }

        return redirectPath;
    }

    protected string JoinErrors()
    {
        var errors = new Dictionary<string, List<string>>();

        if (!ModelState.IsValid)
        {
            if (ModelState.ErrorCount > 0)
            {
                for (int i = 0; i < ModelState.Values.Count(); i++)
                {
                    var key = ModelState.Keys.ElementAt(i);
                    var value = ModelState.Values.ElementAt(i);

                    if (value.ValidationState == ModelValidationState.Invalid)
                    {
                        errors.Add(key, value.Errors.Select(x => string.IsNullOrEmpty(x.ErrorMessage) ? x.Exception?.Message : x.ErrorMessage).ToList());
                    }
                }
            }
        }

        var error = string.Join("<br/>", errors.Select(x =>
        {
            return $"{string.Join(" - ", x.Value)}";
        }));
        return error;
    }

    public async Task<ContentResult> AjaxFunction(Func<int,Task<OperationResult>> func)
    {
        var result = OperationResult.Success();
        var content = JsonConvert.SerializeObject(result);
        return Content(content);
    }
    public async Task<ContentResult> AjaxTryCatch(
    Func<Task<OperationResult>> func,
       bool isSuccessReloadPage = true,
       bool isErrorReloadPage = false,
       bool checkModelState = true)
    {
        try
        {
            var isPost = PageContext.HttpContext.Request.Method == "POST";

            if (isPost && !ModelState.IsValid && checkModelState)
            {
                var errors = JoinErrors();
                var modelError = new OperationResult()
                {
                    Status = OperationResultStatus.Error,
                    Title = "عملیات ناموفق",
                    Message = errors,
                    IsReload = isErrorReloadPage,
                };
                var jsonResult = JsonConvert.SerializeObject(modelError);

                return Content(jsonResult);
            }

            var res = await func().ConfigureAwait(false);
            var model = new OperationResult()
            {
                Status = res.Status,
                Title = null,
                Message = res.Message
            };
            switch (res.Status)
            {
                case OperationResultStatus.Success:
                    {
                        model.IsReload = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case OperationResultStatus.Error:
                    {
                        model.IsReload = isErrorReloadPage;

                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case OperationResultStatus.NotFound:
                    {
                        model.IsReload = isErrorReloadPage;
                        model.Title ??= "نتیجه ای یافت نشد";
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                //case AppStatusCode.BadRequest:
                //    {
                //        model.IsReloadPage = isErrorReloadPage;
                //        model.Title ??= "اطلاعات نامعتبر است";
                //        var jsonResult = JsonConvert.SerializeObject(model);
                //        return Content(jsonResult);
                //    }
                default:
                    {
                        model.IsReload = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
            }
        }
        catch (Exception ex)
        {
            var res = OperationResult.Error(ex.Message);
            var model = new OperationResult()
            {
                Status = res.Status,
                Title = null,
                Message = res.Message,
                IsReload = isErrorReloadPage
            };
            var jsonResult = JsonConvert.SerializeObject(model);
            return Content(jsonResult);
        }
    }


    public async Task<ContentResult> AjaxTryCatch<T>(Func<Task<OperationResult<T>>> func,
        bool isSuccessReloadPage = false,
        bool isErrorReloadPage = false)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = JoinErrors();
                var modelError = new OperationResult<T>()
                {
                    Status = OperationResultStatus.Error,
                    Title = "عملیات ناموفق",
                    Message = errors,
                    IsReload = isErrorReloadPage,
                    Data = default(T)
                };
                var jsonResult = JsonConvert.SerializeObject(modelError);
                var contentResult  = Content(jsonResult);
                return contentResult;
            }

            var res = await func().ConfigureAwait(false);
            var model = new OperationResult<T>()
            {
                Status = res.Status,
                Title = null,
                IsReload = isSuccessReloadPage,
                Message = res.Message,
                Data = res.Data
            };

            switch (res.Status)
            {
                case OperationResultStatus.Success:
                    {
                        model.IsReload = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        var contentResult = Content(jsonResult);
                        return contentResult;
                    }
                case OperationResultStatus.Error:
                    {
                        model.IsReload = isErrorReloadPage;

                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
                case OperationResultStatus.NotFound:
                    {
                        model.IsReload = isErrorReloadPage;
                        model.Title ??= "نتیجه ای یافت نشد";
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }

                default:
                    {
                        model.IsReload = isSuccessReloadPage;
                        var jsonResult = JsonConvert.SerializeObject(model);
                        return Content(jsonResult);
                    }
            }
        }
        catch (Exception ex)
        {
            var res = OperationResult.Error(ex.Message);
            var model = new OperationResult<T>()
            {
                Status = res.Status,
                Title = null,
                Message = res.Message,
                IsReload = isErrorReloadPage
            };
            var jsonResult = JsonConvert.SerializeObject(model);
            return Content(jsonResult);
        }
    }
}