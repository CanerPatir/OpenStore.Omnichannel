using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace OpenStore.Omnichannel.Panel.Services
{
    public class AlertService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly IStringLocalizer<App> _sharedLocalizer;

        public AlertService(IJSRuntime jsRuntime, IStringLocalizer<App> sharedLocalizer)
        {
            _jsRuntime = jsRuntime;
            _sharedLocalizer = sharedLocalizer;
        }

        private IJSInProcessRuntime JsRuntimeSync => (IJSInProcessRuntime) _jsRuntime;

        public void Alert(string message) => JsRuntimeSync.InvokeVoid("alert", message);

        public bool Confirm(string message) => JsRuntimeSync.Invoke<bool>("confirm", message);

        public bool ConsoleLog(string message) => JsRuntimeSync.Invoke<bool>("console.log", message);

        public bool ConsoleError(string message) => JsRuntimeSync.Invoke<bool>("console.error", message);

        // toast
        public void ShowSuccess(string message, string title = null) => JsRuntimeSync.ShowSuccess(message, title ?? _sharedLocalizer["Success.Title"]);
     
        public void ShowCreatedSuccess() => ShowSuccess(_sharedLocalizer["Success.GenericCreated"]);

        public void ShowError(string message, string title = null) => JsRuntimeSync.ShowError(message, title ?? _sharedLocalizer["Error.Title"]);

        public void ShowWarning(string message, string title = null) => JsRuntimeSync.ShowWarning(message, title ?? _sharedLocalizer["Warning.Title"]);
    }
}