using System.Collections.Generic;

using UnityEngine;

public class ComponentPool<T> : IPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Stack<T> _inactive = new();

    public ComponentPool(T prefab, Transform parent, int initialSize = 1)
    {
        _prefab = prefab;
        _parent = parent;
        for (int i = 0; i < initialSize; i++)
            _inactive.Push(CreateInstance());
    }

    private T CreateInstance()
    {
        if (_prefab == null) return null;
        T instance = Object.Instantiate(_prefab, _parent);
        instance.gameObject.SetActive(false);
        return instance;
    }

    public T Get()
    {
        T instance = _inactive.Count > 0 ? _inactive.Pop() : CreateInstance();
        if (instance == null) return null;
        instance.transform.SetAsLastSibling();
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void Return(T instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.SetAsLastSibling();
        _inactive.Push(instance);
    }
}