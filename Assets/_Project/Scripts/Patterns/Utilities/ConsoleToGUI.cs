using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConsoleToGUI : MonoBehaviour
{
    string myLog = "*begin log";
    string filenameFull = "";
    string filename = "";
    bool doShow;
    int kChars = 700;

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            doShow = !doShow;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        // for onscreen...
        myLog = myLog + "\n" + logString;
        if (myLog.Length > kChars)
        {
            myLog = myLog.Substring(myLog.Length - kChars);
        }

        // for the file ...
        if (filename == "")
        {
            string d = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
            
            System.IO.Directory.CreateDirectory(d);
            
            string r = Random.Range(1000, 9999).ToString();
            filename = d + "/log-" + r + ".txt";
        }
        if (filenameFull == "")
        {
            string d = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
            
            System.IO.Directory.CreateDirectory(d);
            
            string r = Random.Range(1000, 9999).ToString();
            filenameFull = d + "/log-" + r + "-FULL.txt";
        }
        

        try
        {
            System.IO.File.AppendAllText(filename, logString + "\n" + "\n");
            System.IO.File.AppendAllText(filenameFull, logString + "\n" + stackTrace + "\n");
        }
        catch
        {
        }
    }

    void OnGUI()
    {
        if (!doShow)
        {
            return;
        }

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,
            new Vector3(Screen.width / 1200.0f, Screen.height / 800.0f, 1.0f));
        GUI.TextArea(new Rect(300, 10, 540, 200), myLog);
    }
}