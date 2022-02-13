using System;
using UnityEngine;

namespace YWR.Tools
{
    public static class Logger
    {
        private const char Period = '.';
        private const string OpenBrackets = "[";
        private const char CloseBrackets = ']';
        private const string OpenColor = ": <color=#{0}><b>";
        private const string CloseColor = "</b></color>";
        private static readonly Color black = Color.black;

        public static void Log<T>(object log)
        {
            object coloredText = BuildColor(log);
            object context = BuildContext<T>();
            PrintLog(context, coloredText, black);
        }

        public static void Log<T>(object log, Color color)
        {
            object coloredText = BuildColor(log);
            object context = BuildContext<T>();
            PrintLog(context, coloredText, color);
        }

        private static object BuildColor(object log)
        {
            return $"{OpenColor}{log}{CloseColor}";
        }

        private static object BuildContext<T>()
        {
            return $"{OpenBrackets}{GetTypeString(typeof(T))}{CloseBrackets}";
        }

        private static void PrintLog(object context, object log, Color color)
        {
            Debug.Log(string.Format($"{context}{log}", ColorUtility.ToHtmlStringRGBA(color)));
        }

        private static string GetTypeString(Type t)
        {
            string[] split = t.ToString().Split(Period);
            return split[split.Length - 1];
        }
    }
}