using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private const float MinAlpha = 0f;

    private Renderer _renderer;

    private Color _initialColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        _initialColor = _renderer.material.color;
    }

    public void Change(Color newColor)
    {
        _renderer.material.color = newColor;
    }

    public Color GetIntermediateAlpha(float elapsedTime, float duration)
    {
        float newAlpha = Mathf.Lerp(_initialColor.a, MinAlpha, elapsedTime / duration);

        return new Color(_initialColor.r, _initialColor.g, _initialColor.b, newAlpha);
    }
}
