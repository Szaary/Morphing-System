using UnityEngine;

public class FPS : MonoBehaviour
{
    
    public TextMesh fpsText;
    public TextMesh graphicText;

    private float _deltaTime;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update ()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        SetFPS();
        graphicText.text = Screen.currentResolution.ToString();
    }

    private void SetFPS()
    {
        var msec = _deltaTime * 1000.0f;
        var fps = 1.0f / _deltaTime;
        fpsText.text = $"FPS: {fps:00.} ({msec:00.0} ms)";
    }
}