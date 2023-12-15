using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager {
    private static GameObject _player;
    private static string _selectedPlayerName;
    private static int _killNumberOfCurrentStage;
    
    private static int _currentStage = 1;
    private static int _numberOfTotalMonsterKill;
    private static float _totalGamePlayTime;


    public int CurrentStage
    {
        get { return _currentStage; }
        set
        {
            _currentStage = value;
        }
    }
    public int NumberOfTotalMonsterKill
    {
        get { return _numberOfTotalMonsterKill; }
        set { _numberOfTotalMonsterKill = value; }
    }
    public float TotalGamePlayTime
    {
        get { return _totalGamePlayTime; }
        set { _totalGamePlayTime = value; }
    }
    public string SelectedPlayerName
    {
        get { return _selectedPlayerName; }
        set { _selectedPlayerName = value; }
    }

    public GameObject player
    {
        get { return _player; }
        set { _player = value; }
    }

    public int KillNumberOfCurrentStage
    {
        get { return _killNumberOfCurrentStage; }
        set { _killNumberOfCurrentStage = value; }
    }

    public void QuitGame()
    {
        // 어플리케이션 종료
        Application.Quit();

        // 에디터에서는 종료되지 않으므로 주의
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void TimeSet(bool set)
    {
        if (set) Time.timeScale = 1.0f;
        else Time.timeScale = 0.0f;
    }
}
 