using UnityEngine;
using TMPro;

public class Volume : MonoBehaviour
{
    private TMP_Text volumeText;

    private void Awake()
    {
        volumeText = transform.GetComponentInChildren<TMP_Text>();
    }

    public void UpdateVolumeText(float volumePercent)
    {
        volumeText.text = $"Volume: {volumePercent}%";
    }
}
