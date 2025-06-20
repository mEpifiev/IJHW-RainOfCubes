using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _color;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Change() => _renderer.material.color = _color;
}
