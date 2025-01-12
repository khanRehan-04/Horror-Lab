using UnityEngine;

public class Dbg : Singleton<Dbg>
{
    // A parameter to control if debug messages should be printed
    private static bool enableDebugLogs = true;

    // Public property to get or set the debug log setting
    public static bool EnableDebugLogs
    {
        get { return enableDebugLogs; }
        set { enableDebugLogs = value; }
    }

    // Default log method (will log as Debug.Log)
    public static void Log(string message)
    {
        if (enableDebugLogs)
        {
            Debug.Log(message);
        }
    }

    // LogWarning method (will log as Debug.LogWarning)
    public static void LogWarning(string message)
    {
        if (enableDebugLogs)
        {
            Debug.LogWarning(message);
        }
    }
}