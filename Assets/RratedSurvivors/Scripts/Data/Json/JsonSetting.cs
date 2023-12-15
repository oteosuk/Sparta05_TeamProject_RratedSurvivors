using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using static UnityEngine.InputSystem.InputSettings;
using UnityEngine.SocialPlatforms.Impl;


//해당 스크립트는 매니저에 달아두면 좋을것 같아요!
public class JobSettings
{
    //레벨
    public int Level;
    //체력
    public int HP;
    //최대체력
    public int MaxHP;
    //공격력
    public int AD;
    //스피드
    public float Speed;
    //방어력 일단은 더미데이터 나중에 틱당 데미지를 입을 때 사용
    public int DF;
    //현재 경험치
    public int Exp;
    //공격 속도
    public int AS;
    //레벨업에 필요한 경험치
    public int NextExp;
    //스킬 쿨타임
    public int SkillWaiting;
}

[System.Serializable]
public class JobContainer
{
    public Dictionary<string, JobSettings> Jobs = new Dictionary<string, JobSettings>();

}

public class JobAbility
{
    //최대체력
    public int MaxHP;
    //공격력
    public int AD;
    //스피드
    public float Speed;
    //방어력 일단은 더미데이터 나중에 틱당 데미지를 입을 때 사용
    public int DF;
    //공격 속도
    public int AS;
    //레벨업에 필요한 경험치
    public int NextExp;
}

[System.Serializable]
public class AbilityContainer
{
    public Dictionary<string, JobAbility> Ability = new Dictionary<string, JobAbility>();

}

public class JsonSetting : MonoBehaviour
{
    private static JsonSetting instance;

    
    // 싱글톤화
    public static JsonSetting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JsonSetting>();

                if (instance == null)
                {
                    GameObject container = new GameObject("JsonSettings");
                    instance = container.AddComponent<JsonSetting>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (gameObject.name == "PlayerSelectCanvas")
        {
            return;
        }

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        
        DontDestroyOnLoad(gameObject);

        
        instance = this;
    }


    //오브젝트의 이름으로 데이터 얻어오기
    public JobSettings AbilitySettings(string jobName)
    {

        // "(Clone)"이 포함되어 있으면 제외, 없으면 그대로
        string baseName = jobName.Contains("(Clone)") ? jobName.Replace("(Clone)", "").TrimEnd() : jobName;
        JobContainer jobContainer = new JobContainer();

        // 파일 경로 설정
        //string filePath = Path.Combine(Application.dataPath, "Resources/Json/CharacterDate.json");
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CharacterDate");
        // 파일이 존재하는지 확인
        //if (File.Exists(filePath))
        if(jsonFile != null)
        {
            // 파일 내용 읽기
            //string jsonString = File.ReadAllText(filePath);

            // JSON 문자열을 객체로 역직렬화 (Newtonsoft.Json 사용)
            //jobContainer = JsonConvert.DeserializeObject<JobContainer>(jsonString);
            jobContainer = JsonConvert.DeserializeObject<JobContainer>(jsonFile.text);
            // 특정 플레이어 가져오기
            string targetJobSettingsKey = baseName;
            if (jobContainer.Jobs.ContainsKey(targetJobSettingsKey))
            {
                //받은 오브젝트 이름을 키값으로 해당 값이 들어있는 json객체를 JobSettings으로 포맷하여 넣어줌
                JobSettings targetJobSettings = jobContainer.Jobs[targetJobSettingsKey];
                //성공시 데이터 리턴
                return targetJobSettings;
            }
            else
            {
                Debug.LogError("Job not found: " + targetJobSettingsKey);
            }
        }
        else
        {
            Debug.LogError("File not found: Json/CharacterDate");
        }
        //실패시 null 리턴
        return null;
    }


    public JobAbility AbilityUP(string jobName)
    {
        // "(Clone)"이 포함되어 있으면 제외, 없으면 그대로
        string baseName = jobName.Contains("(Clone)") ? jobName.Replace("(Clone)", "").TrimEnd() : jobName;
        AbilityContainer abilityContainer = new AbilityContainer();

        // 파일 경로 설정
        //string filePath = Path.Combine(Application.dataPath, "Resources/Json/CharacterDate.json");
        TextAsset jsonFile = Resources.Load<TextAsset>("Json/CharacterDate");
        // 파일이 존재하는지 확인
        //if (File.Exists(filePath))
        if (jsonFile != null)
        {
            // 파일 내용 읽기
            //string jsonString = File.ReadAllText(filePath);

            // JSON 문자열을 객체로 역직렬화 (Newtonsoft.Json 사용)
            abilityContainer = JsonConvert.DeserializeObject<AbilityContainer>(jsonFile.text);

            // 특정 플레이어 가져오기
            string targetJobSettingsKey = baseName;
            if (abilityContainer.Ability.ContainsKey(targetJobSettingsKey))
            {
                //받은 오브젝트 이름을 키값으로 해당 값이 들어있는 json객체를 JobSettings으로 포맷하여 넣어줌
                JobAbility targetJobAbility = abilityContainer.Ability[targetJobSettingsKey];
                //성공시 데이터 리턴
                return targetJobAbility;
            }
            else
            {
                Debug.LogError("Job not found: " + targetJobSettingsKey);
            }
        }
        else
        {
            Debug.LogError("File not found: Json/CharacterDate");
        }
        //실패시 null 리턴
        return null;
    }
}
