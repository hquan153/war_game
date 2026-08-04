using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Launcher : MonoBehaviour
{
    private static Process serverProcess;

    private void Awake()
    {
        if (serverProcess != null && !serverProcess.HasExited)
        {
            Debug.Log("Server is running!");
            return;
        }

        string batPath = "";

#if UNITY_EDITOR
        batPath = Path.Combine(Application.dataPath, "../../run_server.bat");
#else
        batPath = Path.Combine(Application.dataPath, "../run_server.bat");
#endif

        try
        {
            ProcessStartInfo psi = new()
            {
                FileName = Path.GetFullPath(batPath),
                UseShellExecute = true,
                CreateNoWindow = false
            };

            serverProcess = Process.Start(psi);
            Debug.Log("Called server!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error opening server: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        if (serverProcess == null || serverProcess.HasExited) return;

        try
        {
            ProcessStartInfo killPsi = new()
            {
                FileName = "taskkill",
                Arguments = $"/F /T /PID {serverProcess.Id}",
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process.Start(killPsi);
            Debug.Log("Successfully closed Server CMD!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error closing server: " + e.Message);
        }
    }
}