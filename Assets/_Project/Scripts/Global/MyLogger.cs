using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;

public static class MyLogger 
{
    private enum LogLevel
    {
        Full,
        OnlyErrors,
        None
    }
    private static LogLevel _logLevel;
    public static void Log(string message, Result result)
    {
        switch (_logLevel)
        {
            case LogLevel.None:
                return;
            case LogLevel.OnlyErrors:
            {
                if(result != Result.Success) Debug.Log(message);
                break;
            }
            case LogLevel.Full:
                Debug.Log(message);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
