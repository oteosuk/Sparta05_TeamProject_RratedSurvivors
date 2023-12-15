using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    private GameObject _pool;

    public GameObject GetObj(string name)
    {
        if(_pool == null)
        {
            Debug.Log($"Not Found Pool");
            return null;
        }
        ObjectPool pool = _pool.GetComponent<ObjectPool>();
        return pool.GetObj(name);
    }

    public void ReturnObj(GameObject obj)
    {
        if(_pool == null)
        {
            Debug.Log($"Not Found Pool");
            return;
        }
        ObjectPool pool = _pool.GetComponent<ObjectPool>();
        pool.ReturnObj(obj);
    }

    public bool ContainsObj(string name)
    {
        if (_pool == null)
            return false;

        return _pool.GetComponent<ObjectPool>().ContainsObj(name);
    }

    public void SetPool(GameObject obj)
    {
        if(_pool != null)
        {
            Debug.Log($"Pools Overlap");
            return;
        }
        _pool = obj;
    }

    public void DeletePool()
    {
        _pool = null;
    }
}
