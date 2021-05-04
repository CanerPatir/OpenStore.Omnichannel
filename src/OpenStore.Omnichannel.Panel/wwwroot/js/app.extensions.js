window.appCulture = {
    set:(value) => {
        window.localStorage["culture"] = value;
    },
    get:() => {
        return window.localStorage["culture"];
    }
}

window.__loadScript = function (scriptPath, isAsync) {
    if (loadedScripts[scriptPath]) {
        return new this.Promise((resolve, reject) => {
            resolve();
        });
    }

    return new Promise((resolve, reject) => {
        const script = document.createElement("script");
        script.src = scriptPath;

        if (isAsync === true) {
            script.setAttribute("async", "");
        }
        script.onload = () => resolve(scriptPath);
        script.onerror = () => reject(scriptPath);
        loadedScripts[scriptPath] = true;

        document["body"].appendChild(script);
    });
}

window.__removeScript = scriptPath => {
    const theScript = document.querySelector("script[src='"+scriptPath+"']");
    theScript.parentNode.removeChild(theScript);
    delete loadedScripts[scriptPath];
};

window.__loadCss = function (cssPath) {
    if (loadedScripts[cssPath]) {
        return new this.Promise((resolve, reject) => {
            resolve();
        });
    }

    return new Promise((resolve, reject) => {
        const css = document.createElement("link");
        css.href = cssPath;
        css.type = "text/css";
        css.rel = "stylesheet";
        css.onload = () => resolve(cssPath);
        css.onerror = () => reject(cssPath);
        loadedScripts[cssPath] = true;

        document["head"].appendChild(css);
    });
}

window.__insertHeadScript = scriptContent => {
    const script = document.createElement("script");
    script.innerText = scriptContent;
    const head = document["head"];
    head.insertBefore(script, head.firstChild);
}

loadedScripts = {};

window.__showToast = (type, message, header) => {
    const options = {
        closeButton: true,
        debug: false,
        newestOnTop: true,
        progressBar: true,
        positionClass: "toast-top-center",
        preventDuplicates: false,
        onclick: null,
        showDuration: 300,
        hideDuration: 1000,
        timeOut: 10000,
        extendedTimeOut: 1000,
        showEasing: 'swing',
        hideEasing: 'linear'
    }
    toastr[type](message, header, options);
}
