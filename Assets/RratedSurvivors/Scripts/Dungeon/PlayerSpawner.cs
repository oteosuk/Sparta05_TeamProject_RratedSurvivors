using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerObjects;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CreatePlayer(Managers.GameManager.SelectedPlayerName);
    }
    public void CreatePlayer(string characterName)
    {
        foreach (GameObject item in playerObjects)
        {
            GameObject gameObject = item.gameObject;
            if (gameObject.name == characterName)
            {
                GameObject playerPrefab = Managers.Resource.Instantiate(characterName, transform);
                print(characterName);
                playerPrefab.GetComponentInChildren<WeaponManager>().Name = characterName;
                Managers.GameManager.player = playerPrefab;
            }
        }
    }
}
