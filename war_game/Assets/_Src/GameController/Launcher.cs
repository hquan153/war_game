using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Launcher : MonoBehaviour
{
    private static Process serverProcess;

    private void Awake()
    {
        //if (this.enabled) LaunchServer();
    }

    private void OnApplicationQuit()
    {
        KillServer();
    }

    public void LaunchServer()
    {
        if (serverProcess != null && !serverProcess.HasExited)
        {
            Debug.Log("[LAUNCHER]: Server is running...");
            Debug.Log("[LAUNCHER]: Restarting server...");
            KillServer();
        }

        string batPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../../run_server.bat"));

        try
        {
            ProcessStartInfo psi = new()
            {
                FileName = batPath,
                UseShellExecute = true,
                CreateNoWindow = false
            };

            serverProcess = Process.Start(psi);
            Debug.Log("[LAUNCHER]: Server started!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("[LAUNCHER]: Error opening server: " + e.Message);
        }
    }

    public void KillServer()
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
            Debug.Log("[LAUNCHER]: Successfully closed Server CMD!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("[LAUNCHER]: Error closing server: " + e.Message);
        }
    }
}