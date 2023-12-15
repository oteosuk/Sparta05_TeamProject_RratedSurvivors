using System.Collections;
using UnityEngine;

public class MiddleBossMonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Transform outerBox;
    public Transform innerBox;

    private Bounds outerBounds;
    private Bounds innerBounds;
    //스테이지간 몬스터가 스폰되는 딜레이 차이
    private float gapSpawnDelayBetweenStages = 5f;
    private float initSpawnDelay = 10f;
    private float spawnDelay;

    private void Awake()
    {
        outerBounds = new Bounds(outerBox.position, outerBox.localScale);
        innerBounds = new Bounds(innerBox.position, innerBox.localScale);
    }

    void Start()
    {
        StartCoroutine(SpawnMonsterDelay());
    }

    IEnumerator SpawnMonsterDelay()
    {
        while (true)
        {
            if (CheckSpawnPosition())
            {
                spawnDelay = initSpawnDelay - gapSpawnDelayBetweenStages * Managers.GameManager.CurrentStage;
                yield return new WaitForSeconds(spawnDelay > 5f ? spawnDelay : 5f);
            }
            else
            {
                yield return null;
            }
        }
    }

    private bool CheckSpawnPosition()
    {
        //if (Managers.GameManager.CurrentStage < 3) return false;
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

        int randomValue = Random.Range(0, monsterPrefabs.Length);

        monsterPrefab = monsterPrefabs[randomValue];


        return monsterPrefab;
    }
}
