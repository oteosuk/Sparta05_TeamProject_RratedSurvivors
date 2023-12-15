using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public List<GameObject> monsterPrefabList = new List<GameObject>();
    private List<GameObject> magicPrefabList = new List<GameObject>();
    public Transform outerBox;
    public Transform innerBox;

    private Bounds outerBounds;
    private Bounds innerBounds;
    //스테이지간 몬스터가 스폰되는 딜레이 차이
    private float gapSpawnDelayBetweenStages = 0.05f;
    private float initSpawnDelay = 1f;
    private float spawnDelay;

    private float gapSpawnDelayBetweenStagesMagic = 0.05f;
    private float initSpawnDelayMagic = 2f;
    private float spawnDelayMagic;

    private ChangeStageController controller;

    private void Awake()
    {
        outerBounds = new Bounds(outerBox.position, outerBox.localScale);
        innerBounds = new Bounds(innerBox.position, innerBox.localScale);
        controller = GetComponent<ChangeStageController>();
    }

    void Start()
    {
        StartCoroutine(SpawnMonsterDelay());
        StartCoroutine(SpawnMagicMonsterDelay());
    }

    private void OnEnable()
    {
        controller.ChangeStageEvent += AddMonsterPrefab;
    }

    private void OnDisable()
    {
        controller.ChangeStageEvent -= AddMonsterPrefab;
    }

    IEnumerator SpawnMonsterDelay()
    {
        while (true)
        {
            if (CheckAndSpawnMonster())
            {
                spawnDelay = initSpawnDelay - gapSpawnDelayBetweenStages * Managers.GameManager.CurrentStage;
                yield return new WaitForSeconds(spawnDelay > 0.1f ? spawnDelay : 0.1f);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator SpawnMagicMonsterDelay()
    {
        while (true)
        {
            if (CheckAndSpawnMagicMonster())
            {
                spawnDelayMagic = initSpawnDelayMagic - gapSpawnDelayBetweenStagesMagic * Managers.GameManager.CurrentStage;
                yield return new WaitForSeconds(spawnDelayMagic > 0.2f ? spawnDelayMagic : 0.2f);
            }
            else
            {
                yield return null;
            }
        }
    }

    private bool CheckAndSpawnMonster()
    {
        Vector2 spawnPosition;

        GameObject monsterPrefab = GetMonsterPrefab();

        float randomXPos = outerBox.localScale.x / 2 - monsterPrefab.transform.localScale.x / 2;
        float randomYPos = outerBox.localScale.y / 2 - monsterPrefab.transform.localScale.y / 2;

        spawnPosition.x = Random.Range(randomXPos * -1, randomXPos);
        spawnPosition.y = Random.Range(randomYPos * -1, randomYPos);


        //몬스터가 외부 상자 안에 있는지 확인 && 내부 상자에는 없는지 확인
        if (outerBounds.Contains(spawnPosition) && !innerBounds.Contains(spawnPosition))
        {
            GameObject returnPrefab = Managers.Resource.Instantiate(monsterPrefab.name, transform);
            returnPrefab.transform.position = spawnPosition;
            Enemy enemy = returnPrefab.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.EnablePoolAction();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckAndSpawnMagicMonster()
    {
        if (magicPrefabList.Count <= 0) return false;
        Vector2 spawnPosition;

        GameObject monsterPrefab = monsterPrefabs[2];

        float randomXPos = outerBox.localScale.x / 2 - monsterPrefab.transform.localScale.x / 2;
        float randomYPos = outerBox.localScale.y / 2 - monsterPrefab.transform.localScale.y / 2;

        spawnPosition.x = Random.Range(randomXPos * -1, randomXPos);
        spawnPosition.y = Random.Range(randomYPos * -1, randomYPos);


        //몬스터가 외부 상자 안에 있는지 확인 && 내부 상자에는 없는지 확인
        if (outerBounds.Contains(spawnPosition) && !innerBounds.Contains(spawnPosition))
        {
            GameObject returnPrefab = Managers.Resource.Instantiate(monsterPrefab.name, transform);
            returnPrefab.transform.position = spawnPosition;
            Enemy enemy = returnPrefab.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.EnablePoolAction();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private GameObject GetMonsterPrefab()
    {
        GameObject monsterPrefab;

        int randomValue = Random.Range(0, monsterPrefabList.Count);

        monsterPrefab = monsterPrefabList[randomValue];

        return monsterPrefab;
    }

    private void AddMonsterPrefab(int stage)
    {
        if (stage == 2)
        {
            GameObject addPrefab;
            addPrefab = monsterPrefabs.FirstOrDefault(monsterObj => monsterObj.name == "EnemyMagic");
            magicPrefabList.Add(addPrefab);
        }

        if (stage == 3)
        {
            GameObject addPrefab;
            addPrefab = monsterPrefabs.FirstOrDefault(monsterObj => monsterObj.name == "EnemyFast");
            monsterPrefabList.Add(addPrefab);
        }
    }
}