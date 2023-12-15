using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;

    private float spawnDelay = 20f;
    private int currentIndex = 0;


    public Transform outerBox;

    void Start()
    {
        StartCoroutine(SpawnMonsterDelay());
    }

    IEnumerator SpawnMonsterDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnMonster();
            if (currentIndex >= 4) break;
        }
    }

    private void SpawnMonster()
    {
        Managers.Sound.SoundPlay("SFX/SFX_Boss", SoundType.Effect);
        Vector2 spawnPosition;

        GameObject monsterPrefab = GetMonsterPrefab();

        spawnPosition.x = 0;
        spawnPosition.y = outerBox.localScale.y / 2 - monsterPrefab.transform.localScale.y / 2;


        //몬스터가 외부 상자 안에 있는지 확인 && 내부 상자에는 없는지 확인
        GameObject returnPrefab = Managers.Resource.Instantiate(monsterPrefab.name, transform);
        returnPrefab.transform.position = spawnPosition;
    }

    private GameObject GetMonsterPrefab()
    {
        GameObject monsterPrefab;

        monsterPrefab = monsterPrefabs[currentIndex];
        currentIndex++;

        return monsterPrefab;
    }
}
