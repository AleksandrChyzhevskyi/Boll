using System.Collections.Generic;
using UnityEngine;

public class Border
{
    private readonly Vector2Int _size;
    private readonly PoolCircles _pool;
    private readonly Dictionary<int, Color> _colors;

    public Border(Vector2Int size, PoolCircles pool)
    {
        _size = size;
        _pool = pool;
        _colors = new Dictionary<int, Color>
        {
            { 1, Color.black },
            { 2, Color.white },
            { 3, Color.red },
            { 4, Color.green },
        };
    }
        
    public void CreateBord()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Circle circle = _pool.GetFreeElement();
                circle.transform.position = new Vector3(x, y, 0);
                circle.SetColor(RandomColor());
            }
        }
    }

    public void ActiveCircle()
    {
        foreach (var element in _pool.GetInactive())
        {
            element.gameObject.SetActive(true);
            element.SetColor(RandomColor());
        }
    }

    private Color RandomColor() =>
        _colors[Random.Range(1, _colors.Count + 1)];
}