
using System.Collections;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public AnimationCurve FadeCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));

    private float _alpha = 1;
    private Texture2D _texture;
    private bool _done = true;
    private float _time;

    public float zoomAmount = 0.5f;
    public float zoomDuration = .4f;

    private float originalSize;

    void Start()
    {
        originalSize = Camera.main.orthographicSize;
    }
    public void ZoomInOut()
    {
        StartCoroutine(ZoomCoroutine());
    }

    private IEnumerator ZoomCoroutine()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < zoomDuration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(originalSize, originalSize * zoomAmount, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.orthographicSize = originalSize;
    }


    public void Reset()
    {
        _done = false;
        _alpha = 1;
        _time = 0;
    }

    [RuntimeInitializeOnLoadMethod]
    public void RedoFade()
    {
        Reset();
    }

    public void OnGUI()
    {
        if (_done) return;
        if (_texture == null) _texture = new Texture2D(1, 1);

        _texture.SetPixel(0, 0, new Color(0, 0, 0, _alpha));
        _texture.Apply();

        _time += Time.deltaTime;
        _alpha = FadeCurve.Evaluate(_time);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

        if (_alpha <= 0) _done = true;
    }
}