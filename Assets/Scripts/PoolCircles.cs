using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCircles : MonoBehaviour
{
    [SerializeField] private Circle _prefab;
    [SerializeField] private int _count;
    
    private PoolMono<Circle> _pool;

    private void Awake() => 
        _pool = new PoolMono<Circle>(_prefab, _count, transform);

    public Circle GetFreeElement() => 
        _pool.GetFreeElement();

    public List<Circle> GetCirclesAllList() => 
        new(_pool.GetAllElements());

    public List<Circle> GetInactive() => 
        _pool.GetInactiveList();

    public Circle GetElement(Vector2Int position)
    {
        foreach (var element in _pool.GetAllElements())
            if (element.Position == new Vector3(position.x, position.y, 0))
                return element;

        return null;
    }
}