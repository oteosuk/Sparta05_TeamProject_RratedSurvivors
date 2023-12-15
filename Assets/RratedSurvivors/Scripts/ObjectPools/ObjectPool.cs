using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    private class ObjectInfo
    {
        public GameObject perfab;
        public int count;
    }

    [SerializeField] private ObjectInfo[] objectInfos = null;

    private Dictionary<string, Queue<GameObject>> _objPoolDic;
    private Dictionary<string, ObjectInfo> _objectInfoDic;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _objectInfoDic = new Dictionary<string, ObjectInfo>();
        _objPoolDic = new Dictionary<string, Queue<GameObject>>();

        if (objectInfos != null)
        {
            for (int i = 0; i < objectInfos.Length; i++)
            {
                string name = objectInfos[i].perfab.name;
                _objPoolDic.Add(name, CreateObjQueue(objectInfos[i]));
                _objectInfoDic.Add(name, objectInfos[i]);
            }
        }

        Managers.PoolManager.SetPool(this.gameObject); 
    }

    private Queue<GameObject> CreateObjQueue(ObjectInfo info)
    {
        Queue<GameObject> objQueue = new Queue<GameObject>();

        for (int i = 0; i < info.count; ++i)
        {
            GameObject obj = Instantiate(info.perfab);
            obj.SetActive(false);
            obj.transform.SetParent(this.transform);
            obj.name = info.perfab.name;
            objQueue.Enqueue(obj);
        }

        return objQueue;
    }

    public GameObject GetObj(string name)
    {
        if (!_objPoolDic.ContainsKey(name))
        {
            Debug.Log($"Filed to load Pool : {name}");
            return null;
        }

        if (_objPoolDic[name].Count > 0)
        {
            GameObject obj = _objPoolDic[name].Dequeue();
            obj.SetActive(true);
            obj.transform.SetParent(null);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(_objectInfoDic[name].perfab);
            newObj.name = name;
            newObj.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public void ReturnObj(GameObject obj)
    {
        string name = obj.name;

        if (!_objPoolDic.ContainsKey(name))
        {
            Debug.Log($"Filed to Return Pool : {name}");
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(this.transform);
        _objPoolDic[name].Enqueue(obj);
    }

    public bool ContainsObj(string name) 
    {
        return _objPoolDic.ContainsKey(name);
    }

    private void OnDestroy()
    {
        Managers.PoolManager.DeletePool();
    }
}