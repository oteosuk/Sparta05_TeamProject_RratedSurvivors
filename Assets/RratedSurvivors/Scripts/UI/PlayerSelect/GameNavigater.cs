using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameNavigater : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Canvas playerSelectCanvas;

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Sound.SoundPlay("SFX/DM-CGS-03", SoundType.Effect);
    }
    public void StartGame()
    {
        Managers.Scenes.MainGameSceneLoad();
    }

    public void BackGame()
    {
        Managers.Scenes.StartSceneLoad();
    }
}
