using System;
using UnityEngine;

public static class Logger
{
    //----------------------------------------------------------------------------------------------------------

    private const char Period = '.';
    private const string OpenColor = "]: <color={0}><b>";

    private const string CloseColor = "</b></color>";

    //----------------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Use "black", "red" or any other html code to set the color.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="log"></param>
    /// <param name="colorName"></param>
    /// <param name="context"></param>
    public static void Log<T>(object log, string colorName = "black", Type context = null)
    {
        var contextType = GetTypeName(typeof(T));
        log = string.Format("[" + context + OpenColor + log + CloseColor + contextType, colorName);
        Debug.Log(log);
    }

    //----------------------------------------------------------------------------------------------------------

    private static string GetTypeName(Type type)
    {
        if (type == null)
            return string.Empty;

        var split = type.ToString().Split(Period);
        var last = split.Length - 1;
        return last > 0 ? split[last] : string.Empty;
    }
}