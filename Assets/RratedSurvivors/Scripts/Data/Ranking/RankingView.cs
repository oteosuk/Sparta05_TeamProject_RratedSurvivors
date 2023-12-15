using DBConfig;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingView : MonoBehaviour
{
    [SerializeField] GameObject rankingUserUI;
    [SerializeField] GameObject infoMSG;
    List<GameObject> rankingUsers = new List<GameObject>();
    int pageNum = 0;
    int pageCount = 0;
    // Start is called before the first frame update
    private void Start()
    {
        PageCalculation();
    }
    public void PageCalculation()
    {
        (bool, string) ck = RankingManager.Instance.DBConnectTest();
        if (ck.Item1)
        {

            pageCount = RankingManager.Instance.RankingUserCount();
            if (pageCount == 0)
            {
                infoMSG.GetComponent<TMP_Text>().text = "정보 없음";
                infoMSG.SetActive(true);
                return;
            }
            else infoMSG.SetActive(false);

            if (pageCount % 10 == 0) pageCount = pageCount / 10;
            else pageCount = pageCount / 10 + 1;

        }
        else
        {

            infoMSG.GetComponent<TMP_Text>().text = ck.Item2;
            infoMSG.SetActive(true);
        }
    }
    public void SetRankingView(bool set)
    {
        if (set)
        {
            if(pageNum == pageCount) pageNum = 0;
            pageNum++;
        }
        else if (!set)
        {
            if (pageNum == 1) pageNum = pageCount + 1;
            pageNum--;
        }
        else if (!RankingManager.Instance.DBConnectTest().Item1) return;
        
        if(rankingUsers.Count != 0)
        {
            DestroyUserUI();
        }

        List<UserInfo> user = RankingManager.Instance.RankingView(pageNum);


        for(int i = 0; i < user.Count; i++)
        {
            rankingUserUI.transform.GetChild(0).GetComponent<TMP_Text>().text = $"{i + 1+((pageNum-1)*10)}";
            rankingUserUI.transform.GetChild(1).GetComponent<TMP_Text>().text = $"{user[i].User_Name}";
            rankingUserUI.transform.GetChild(2).GetComponent<TMP_Text>().text = $"{user[i].User_Score}";

            GameObject uiObject = Instantiate(rankingUserUI, transform.position, Quaternion.identity);
            rankingUsers.Add(uiObject);
            RectTransform rect = uiObject.GetComponent<RectTransform>();
            rect.SetParent(gameObject.transform);
            rect.localPosition = new Vector3(0f, 125+(-30f * i), 0f);
        }
    }
    void DestroyUserUI()
    {
        for(int i = 0;i < rankingUsers.Count; i++)
        {
            Destroy(rankingUsers[i]);
        }
    }
    public void CancleBtn()
    {
        pageNum = 0;
    }
}
