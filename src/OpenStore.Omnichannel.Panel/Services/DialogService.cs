using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace OpenStore.Omnichannel.Panel.Services;

public class DialogService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IStringLocalizer<App> _sharedLocalizer;

    public DialogService(IJSRuntime jsRuntime, IStringLocalizer<App> sharedLocalizer)
    {
        _jsRuntime = jsRuntime;
        _sharedLocalizer = sharedLocalizer;
    }

    private IJSInProcessRuntime JsRuntimeSync => (IJSInProcessRuntime)_jsRuntime;

    public void BlockUi() => JsRuntimeSync.InvokeVoid("__blockUI");

    public void UnblockUi() => JsRuntimeSync.InvokeVoid("__unblockUI");

    public void Alert(object message) => JsRuntimeSync.InvokeVoid("alert", message);

    public bool Confirm(string message) => JsRuntimeSync.Invoke<bool>("confirm", message);

    public bool DeleteConfirm() => JsRuntimeSync.Invoke<bool>("confirm", _sharedLocalizer["GenericDeleteConfirm"].ToString());

    public bool ConsoleLog(object message) => JsRuntimeSync.Invoke<bool>("console.log", message);

    public bool ConsoleError(object message) => JsRuntimeSync.Invoke<bool>("console.error", message);

    // toast
    public void ShowSuccess(string message, string title = null) => JsRuntimeSync.ShowSuccess(message, title ?? _sharedLocalizer["Success.Title"].ToString());

    public void ShowCreatedSuccess() => ShowSuccess(_sharedLocalizer["Success.GenericCreated"].ToString());

    public void ShowError(string message, string title = null) => JsRuntimeSync.ShowError(message, title ?? _sharedLocalizer["Error.Title"].ToString());

    public void ShowWarning(string message, string title = null) => JsRuntimeSync.ShowWarning(message, title ?? _sharedLocalizer["Warning.Title"].ToString());
}