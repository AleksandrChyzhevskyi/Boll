using UnityEngine;

public class Movement
{
    private readonly PoolCircles _pool;
    private readonly Vector2Int _size;

    public Movement(PoolCircles pool, Vector2Int size)
    {
        _pool = pool;
        _size = size;
    }

    public void Move()
    {
        for (int x = _size.x; x >= 0; x--)
        {
            for (int y = _size.y; y >= 0; y--)
            {
                if (GetTrueElement(x, y, out var circle))
                    continue;

                if (circle.isActiveAndEnabled == false)
                    continue;

                if (GetTrueElement((int)circle.Position.x, (int)circle.Position.y - 1, out var target))
                    continue;

                if (target.isActiveAndEnabled == false)
                {
                    target.gameObject.SetActive(true);
                    target.SetColor(circle.ElementColor);
                    circle.SetColor(Color.blue);
                    circle.gameObject.SetActive(false);
                }
            }
        }
    }

    private bool GetTrueElement(int x, int y, out Circle circle)
    {
        circle = _pool.GetElement(new Vector2Int(x, y));
        return circle == null;
    }
}