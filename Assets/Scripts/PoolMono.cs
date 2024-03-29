using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    private T _prefab { get; }

    private readonly bool _autoExpand;
    private readonly Transform _container;

    private List<T> _pool;

    public PoolMono(T prefab, int count, Transform container, bool autoExpand = true)
    {
        _prefab = prefab;
        _container = container;
        _autoExpand = autoExpand;

        CreatePool(count);
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var mono in _pool.Where(mono => mono.gameObject.activeInHierarchy == false))
        {
            element = mono;
            mono.gameObject.SetActive(true);
            return true;
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;

        if (_autoExpand)
            return CreateObject(true);

        throw new Exception($"There is no free element in pool of type {typeof(T)}");
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    public List<T> GetInactiveList() => 
        _pool.Where(element => element.isActiveAndEnabled == false).ToList();

    public List<T> GetAllElements() => 
        new(_pool);
}