using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ResourceManager
{
    public T Load<T> (string path) where T : Object 
    {
        return Resources.Load<T> (path);
    }

    public GameObject Instantiate(string path, Transform parent) 
    {
        // 생성해야할 프리팹 오브젝트가 풀로 관리되고 있을 경우 오브젝트 풀을 통해서 만들어준다.
        if (Managers.PoolManager.ContainsObj(path))
        {
            return Managers.PoolManager.GetObj(path);
        }

        // 풀로 관리되고 있지 않은 프리팹인 경우 직접 만들어 준다.
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if(prefab == null) 
        {
            Debug.Log($"Filed to load prefab : {path}");
            return null;
        }
        GameObject obj = Object.Instantiate(prefab, parent);
        obj.name = obj.name.Replace("(Clone)", "").Trim();
        return obj;
    }

    public void Destroy (GameObject obj) 
    {
        if (obj == null)
        {
            return;
        }

        //오브젝트 풀로 관리 되고 있는 오브젝트의 경우 오브젝트 풀로 돌려보낸다.
        if (Managers.PoolManager.ContainsObj(obj.name))
        {
            Managers.PoolManager.ReturnObj(obj);
            return;
        }

        //아닌 경우 삭제
        Object.Destroy(obj);
    }
}
