using System.Threading.Tasks;
using Microsoft.JSInterop;

// ReSharper disable CheckNamespace

namespace OpenStore.Omnichannel.Panel
{
    public static class JsRuntimeExtensions
    {
        public static ValueTask LoadScript(this IJSRuntime jsRuntime, string src, bool isAsync = false) => jsRuntime.InvokeVoidAsync("__loadScript", src, isAsync);
        public static void RemoveScript(this IJSRuntime jsRuntime, string src) => jsRuntime.InvokeVoidAsync("__removeScript", src);
        public static ValueTask LoadCss(this IJSRuntime jsRuntime, string src) => jsRuntime.InvokeVoidAsync("__loadCss", src);

        public static void ShowSuccess(this IJSInProcessRuntime jsRuntime, string message, string header = null)
            => jsRuntime.InvokeVoid("__showToast", "success", message, header);

        public static void ShowError(this IJSInProcessRuntime jsRuntime, string message, string header = null)
            => jsRuntime.InvokeVoid("__showToast", "warning", message, header);

        public static void ShowWarning(this IJSInProcessRuntime jsRuntime, string message, string header = null)
            => jsRuntime.InvokeVoid("__showToast", "warning", message, header);
    }
}