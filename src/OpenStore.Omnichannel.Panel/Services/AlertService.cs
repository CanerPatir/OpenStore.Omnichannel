using Microsoft.JSInterop;

namespace OpenStore.Omnichannel.Panel.Services
{
    public class AlertService
    {
        private readonly IJSRuntime _jsRuntime;

        public AlertService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private IJSInProcessRuntime JsRuntimeSync => (IJSInProcessRuntime) _jsRuntime;

        public void Alert(string message) => JsRuntimeSync.InvokeVoid("alert", message);

        public bool Confirm(string message) => JsRuntimeSync.Invoke<bool>("confirm", message);

        public bool ConsoleLog(string message) => JsRuntimeSync.Invoke<bool>("console.log", message);

        public bool ConsoleError(string message) => JsRuntimeSync.Invoke<bool>("console.error", message);

        // toast
        public void ShowSuccess(string message, string title = null) => JsRuntimeSync.ShowSuccess(message, title);

        public void ShowError(string message, string title = null) => JsRuntimeSync.ShowError(message, title);

        public void ShowWarning(string message, string title = null) => JsRuntimeSync.ShowWarning(message, title);
    }
}