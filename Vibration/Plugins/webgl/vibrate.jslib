mergeInto(LibraryManager.library,
{
    VibrateWebgl: function(duration)
    {
        if (typeof navigator !== "undefined" && typeof navigator.vibrate === "function") {
            navigator.vibrate(duration);
        }
    },
    HasVibratorWebgl: function()
    {
        return typeof navigator !== "undefined" && typeof navigator.vibrate === "function" ? 1 : 0;
    }
});