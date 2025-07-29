mergeInto(LibraryManager.library {
    SendEventToGoogleSheet: function(eventPtr, detailsPtr) {
        var eventName = UTF8ToString(eventPtr);
        var details = UTF8ToString(detailsPtr);

        if (typeof window.sendToGoogleSheet === "function") {
            window.sendToGoogleSheet(eventName, details);
        }
        else {
            console.warn("sendToGoogleSheet is not defined in window.");
        }
    }
});