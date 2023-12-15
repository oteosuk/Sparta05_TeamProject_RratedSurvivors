using UnityEngine;
using DBConfig;
using System.Collections.Generic;
using TMPro;

public class RankingManager : MonoBehaviour
{
    private static RankingManager instance;


    
    RankingSystem rankingSystem = new RankingSystem();

    // 싱글톤화
    public static RankingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RankingManager>();

                if (instance == null)
                {
                    GameObject container = new GameObject("RankingManager");
                    instance = container.AddComponent<RankingManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);


        instance = this;
    }
    public (bool,string) DBConnectTest()
    {
        return rankingSystem.ConnectionTest();
    }
    public bool UpdateRanking(string name, int score)
    {
        bool Success = false;
        (bool, string) insert = rankingSystem.InsertRanking(name, score);
        if (DBConnectTest().Item1) Success = insert.Item1;
        return Success;
    }
    public List<UserInfo> RankingView(int pageNum)
    {
        List<UserInfo> users = null;
        if (DBConnectTest().Item1) users = rankingSystem.RankList(pageNum);
        return users;
    }
    public int RankingUserCount()
    {
        return rankingSystem.RankingUserCount();
    }
}
