using UnityEngine;

public class Circle : MonoBehaviour
{
    private readonly Vector3 _size = Vector3.one * 0.1f;
    private Renderer _renderer;
    private int active;
    public Vector3 Position => transform.position;
    public Color ElementColor => _renderer.material.color;

    private void Awake() =>
        _renderer = GetComponent<Renderer>();

    public void SetColor(Color color) =>
        _renderer.material.color = color;

    public void Select(bool activeElements)
    {
        if (active < 1)
        {
            transform.localScale += _size;
            active++;
        }
        else if (activeElements) 
            transform.localScale += _size + _size;
    }

    public void Deselect()
    {
        transform.localScale = Vector3.one;
        active = 0;
    }
}