using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    
    GameObject player;
    PlayerAbility ability;
    [SerializeField] GameObject modal;
    [SerializeField] Image playerImg;
    [SerializeField] RectTransform hpBar;
    [SerializeField] RectTransform mpBar;
    [SerializeField] RectTransform expBar;
    [SerializeField] TMP_Text hpState;
    [SerializeField] TMP_Text mpState;
    [SerializeField] TMP_Text Level;
    //모달 스크립트로 나눠서 작성하면 좋겠지만 시간이 없는 관계로 패스
    [SerializeField] TMP_Text VictoryScore;
    [SerializeField] TMP_Text DefeatScore;
    [SerializeField] TMP_InputField UserName;
    [SerializeField] GameObject RankingBtn;
    [SerializeField] GameObject VictoryRankingBtn;
    [SerializeField] GameObject DefeatRankingBtn;
    [SerializeField] TMP_Text RankingUpdateInfoUITextVictory;
    [SerializeField] TMP_Text RankingUpdateInfoUITextDefeat;
    int Score;
    float hpBarSize;
    float mpBarSize;
    float expBarSize;
    float MpRecovery;
    float recovery = 0;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //해당 오브젝트 찾기는 게임 매니저가 캐릭터를 만들때 캐릭터 오브젝트 이름 받아오기
        player = GameObject.Find($"{Managers.GameManager.SelectedPlayerName}");
        //player = GameObject.Find("Paladin");
        ability = player.GetComponent<PlayerAbility>();
        MpRecovery = player.GetComponent<PlayerAbility>().SkillWaiting;
        //HP가 변경이 있을 때 나오는 메서드
        ability.UpdateHPBar += UpdateHPBar;
        ability.UpdateEXPBar += UpdateExpBar;
        ability.LevelUpUI += ModalSet;
        ability.LevelUp += LevelUI;
        ability.IsDeadUI += IsDeadUI;
        //체력바 마나바 크기
        hpBarSize = hpBar.sizeDelta.x;
        mpBarSize = mpBar.sizeDelta.x;
        expBarSize = expBar.sizeDelta.x;
        //체력바 마나바 초기화
        hpState.text = $"{ability.HP}/{ability.MaxHP}";
        mpState.text = $"{MpRecovery}/{MpRecovery}";
        FindIMG();
        UpdateExpBar(0,0);
        LevelUI(1);
    }



    //레벨업 텍스트 메서드
    private void LevelUI(int level)
    {
        Level.text = $"LV. {level}";
    }

    // 스킬 쿨타임
    public void SkillCool()
    {
        Vector2 newMpBar = mpBar.sizeDelta;
        newMpBar.x = 0;
        mpBar.sizeDelta = newMpBar;
        mpState.text = $"{0}/{MpRecovery}";
        recovery = 0;
    }
    //캐릭터 얼굴 이미지 찾기
    private void FindIMG()
    {
        Texture2D imgResources = Resources.Load<Texture2D>($"Prefabs/Head{player.name}");
        playerImg.sprite = Sprite.Create(imgResources, new Rect(0, 0, imgResources.width, imgResources.height), Vector2.zero);
    }

    // 현재 체력에 따른 체력바 업데이트
    private void UpdateHPBar(int hp, int maxHp)
    {
        Vector2 newhpBar = hpBar.sizeDelta;
        float currentHP = (float)hp / maxHp;
        newhpBar.x = hpBarSize * currentHP;
        hpBar.sizeDelta = newhpBar;
        hpState.text = $"{ability.HP}/{ability.MaxHP}";
    }

    //캐릭터 EXP획득시 UI업데이트 메서드
    private void UpdateExpBar(int exp, int nextExp)
    {
        Vector2 newExpBar = expBar.sizeDelta;
        float currentExp = (float)exp / nextExp == 0 ? 0 : (float)  exp / nextExp;
        newExpBar.x = expBarSize * currentExp;
        expBar.sizeDelta = newExpBar;
    }
    void Update()
    {
        if (player.GetComponent<PlayerSkillAction>()._isCoolDown && mpBar.sizeDelta.x < mpBarSize)
        {
            Vector2 newMpBar = mpBar.sizeDelta;
            newMpBar.x += mpBarSize/MpRecovery*Time.deltaTime;
            mpBar.sizeDelta = newMpBar;
            recovery += Time.deltaTime;
            mpState.text = $"{recovery.ToString("F2")}/{MpRecovery}";
        }
    }

    // 각 정보를 가지고 있는 모달 메서드
    public void ModalSet(int setNum)
    {
        if (setNum == -1)
        {
            modal.SetActive(false);
            Managers.GameManager.TimeSet(true);
            ability.PlayerSet(true);
            return;
        }

            modal.SetActive(true);
            Managers.GameManager.TimeSet(false);
            ability.PlayerSet(false);
        for (int i = 0; i < modal.transform.childCount; i++)
        {
            if (i == setNum)
            {
                //현재는 랜덤 나중에 게임매니저한테 점수를 받아오면 됨
                Score = new System.Random().Next(100, 1000);
                modal.transform.GetChild(setNum).gameObject.SetActive(true);
                if (i == 1) VictoryScore.text = $"점수 : {Score}";
                if (i == 2) DefeatScore.text = $"점수 : {Score}";
            }
            else modal.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    //스텟업 선택시 메서드
    public void OnStatesUP()
    {
        ModalSet(4);
    }
    public void UpStatesSelect(int select)
    {
        ability.AbilityUp(select);
        ModalSet(-1);
    }
    //랜덤 무기 선택시 메서드
    public void GoWeaponSelectPanel() { ModalSet(7); }

    public WeaponManager GetWeaponManager() { return player.GetComponentInChildren<WeaponManager>() ;}

    //게임 종료 메서드
    public void QuitGame()
    {
        Managers.GameManager.QuitGame();
    }
    // 죽었을 때 메서드
    private void IsDeadUI()
    {
        StartCoroutine(IsDead(0.5f));
    }
    IEnumerator IsDead(float delay)
    {
        // delay 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 메서드 실행
        if(Managers.GameManager.CurrentStage < 10)ModalSet(2);
        else ModalSet(1);
    }
    public void SoundBtn()
    {
        Managers.Resource.Instantiate("UI/SoundSettingUI", null);
    }
    // 홈버튼
    public void HomeBtn()
    {
        ability.PlayerSetClear();
        Managers.GameManager.TimeSet(true);
        Managers.Scenes.LoadScene(SceneNames.StartScene);
    }
    // 캐릭터 선택버튼
    public void PlayerSelectBtn()
    {
        ability.PlayerSetClear();
        Managers.GameManager.TimeSet(true);
        Managers.Scenes.LoadScene(SceneNames.SelectCharacterScene);
    }
    //랭킹 업데이트 UI버튼
    public void RankingUpdateUIBtn(bool set)
    {
        if(!set) UserName.text = "";
        modal.transform.GetChild(5).gameObject.SetActive(set);
        modal.transform.GetChild(6).gameObject.SetActive(set);
    }
    // 랭킹 업데이트
    public void RankingUpdateBtn()
    {
        if (!NameCheck(UserName.text))
        {
            UserName.text = "";
            return;
        }
        if (RankingManager.Instance.UpdateRanking(UserName.text, Score)) 
        {
            RankingUpdateInfoUITextVictory.text = "등록 성공";
            RankingUpdateInfoUITextDefeat.text = "등록 성공";
        }
        else
        {
            RankingUpdateInfoUITextVictory.text = "서버 오류";
            RankingUpdateInfoUITextDefeat.text = "서버 오류";
        }
        Destroy(VictoryRankingBtn);
        Destroy(DefeatRankingBtn);
        modal.transform.GetChild(5).gameObject.SetActive(false);
        modal.transform.GetChild(6).gameObject.SetActive(false);
    }

    // 정규표현식
    public bool NameCheck(string name)
    {

        // 정규 표현식 패턴
        string pattern = "^[가-힣]{1,3}$";

        // 정규 표현식과 비교
        return Regex.IsMatch(name, pattern);

    }
}

