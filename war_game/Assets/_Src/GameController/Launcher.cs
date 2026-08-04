using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private void Awake()
    {
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
                FileName = batPath,
                UseShellExecute = true, // open terminal window
                CreateNoWindow = false, // keep the window open to see npm start log
                WindowStyle = ProcessWindowStyle.Minimized
            };

            Process.Start(psi);
            UnityEngine.Debug.Log("Called run_server.bat successfully!");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Lỗi chạy bat: " + e.Message);
        }
    }
}