using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instace;
    private static Managers Instance { get { Init(); return s_instace; } }

    private GameManager _gameManager = new GameManager();
    private ResourceManager _resource = new ResourceManager();
    private PoolManager _poolManager = new PoolManager();
    private ScenesManager _scenes = new ScenesManager();
    private SoundManager _sound = new SoundManager();

    public static GameManager GameManager { get { return Instance._gameManager; } }
    public static PoolManager PoolManager { get { return Instance._poolManager; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static ScenesManager Scenes { get { return Instance._scenes; } }
    public static SoundManager Sound { get { return Instance._sound; } }

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_instace == null) 
        {
            GameObject obj = GameObject.Find("Managers");
            if (obj == null) 
            {
                obj = new GameObject { name = "Managers" };
                obj.AddComponent<Managers>();
            }
            DontDestroyOnLoad(obj);
            s_instace = obj.GetComponent<Managers>();
            s_instace._sound.Init();
        }
    }

    public static void Clear()
    {
        Sound.Clear();
    }
}
