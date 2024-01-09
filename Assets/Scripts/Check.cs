using System.Collections.Generic;
using UnityEngine;

public class Check
{
    private PoolCircles _pool;
    private List<Circle> _circles;
    private List<Circle> sameElements;
    private Circle Circle;

    public void Initialization(PoolCircles poolCircles)
    {
        _pool = poolCircles;
        _circles = _pool.GetCirclesAllList();

        CheckHorizontal();
    }


    public void CheckHorizontal()
    {
        sameElements = new List<Circle>();

        foreach (var element in _circles)
        {
            sameElements.Add(element);
            Horizontal(element, sameElements);
            InactiveElements(sameElements);
        }
    }

    public void CheckVertical()
    {
        sameElements = new List<Circle>();

        foreach (var element in _circles)
        {
            sameElements.Add(element);
            Vertical(element, sameElements);
            InactiveElements(sameElements);
        }
    }

    private void Vertical(Circle element, List<Circle> sameElements)
    {
        for (var y = element.Position.y + 1; y <= element.Position.y + 2; y++)
        {
            Circle target = _pool.GetElement(new Vector2Int((int)element.Position.x, (int)y));
            CheckColor(target, element, sameElements);
        }
    }

    private void Horizontal(Circle element, ICollection<Circle> sameElements)
    {
        for (var x = element.Position.x + 1; x <= element.Position.x + 2; x++)
        {
            Circle target = _pool.GetElement(new Vector2Int((int)x, (int)element.Position.y));
            CheckColor(target, element, sameElements);
        }
    }

    private void CheckColor(Circle target, Circle element, ICollection<Circle> sameElements)
    {
        if (target == null)
            return;

        if (element.ElementColor == target.ElementColor)
            sameElements.Add(target);
    }

    private void InactiveElements(List<Circle> sameElements)
    {
        if (sameElements.Count == 3)
        {
            foreach (var sElement in sameElements)
                sElement.gameObject.SetActive(false);

            sameElements.Clear();
        }
        else
            sameElements.Clear();
    }
}