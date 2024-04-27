using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PerformanceStats : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI cpuUsageText;
    public TextMeshProUGUI memoryUsageText;

    private float lastFrameTime;
    private float lastMemoryUsage;
    private float fpsUpdateInterval = 0.25f;

    private float fpsTimer = 0f;

    public void SetFPSTextActive(bool active)
    {
        fpsText.gameObject.SetActive(active);
    }

    public void SetCPUUsageTextActive(bool active)
    {
        cpuUsageText.gameObject.SetActive(active);
    }

    public void SetMemoryUsageTextActive(bool active)
    {
        memoryUsageText.gameObject.SetActive(active);
    }

    void Update()
    {
        fpsTimer += Time.deltaTime;

        if (fpsTimer >= fpsUpdateInterval)
        {
            UpdateFPS();
            UpdateCPUUsage();
            UpdateMemoryUsage();

            fpsTimer = 0f;
        }
    }

    void UpdateFPS()
    {
        if (fpsText.gameObject.activeSelf)
        {
            float fps = 1f / Time.deltaTime;
            fpsText.text = "FPS: " + Mathf.Round(fps / 10);
        }
    }

    void UpdateCPUUsage()
    {
        if (cpuUsageText.gameObject.activeSelf)
        {
            float cpuUsage = Mathf.Clamp01((Time.realtimeSinceStartup - lastFrameTime) / Time.deltaTime) * 100f;
            cpuUsageText.text = "CPU: " + Mathf.Round(cpuUsage / 10) + "%";
            lastFrameTime = Time.realtimeSinceStartup;
        }
    }

    void UpdateMemoryUsage()
    {
        if (memoryUsageText.gameObject.activeSelf)
        {
            float memoryUsage = Mathf.Round((System.GC.GetTotalMemory(false) - lastMemoryUsage) / (1024f * 1024f));
            memoryUsageText.text = "Memory: " + Mathf.Round(memoryUsage / 10) + " MB";
            lastMemoryUsage = System.GC.GetTotalMemory(false);
        }
    }
}
