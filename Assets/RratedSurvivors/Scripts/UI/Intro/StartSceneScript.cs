using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pressKey;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject mainTitle;
    [SerializeField] private GameObject buttons;
    [SerializeField] GameObject rankingUI;

    private bool _anyKey = false;
    private float _titleTargetPosY = 95.0f;
    private float _moveDuration = 1.5f;
    private float _pressKeyFadeDuration = 1.5f;

    void Start()
    {
        pressKey.GetComponent<TextMeshProUGUI>().DOFade(0, _pressKeyFadeDuration).SetLoops(-1, LoopType.Yoyo);
        Managers.Sound.SoundPlay("Bgm/carnivalrides", SoundType.BGM);
    }

    void Update()
    {
        if((Input.anyKey ||
            Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1) ||
            Input.GetMouseButtonDown(2)) && !_anyKey) 
        {
            Transform transform = mainTitle.GetComponent<Transform>();
            transform.DOMoveY(transform.position.y + _titleTargetPosY, _moveDuration).OnComplete(() =>
            {
                buttons.SetActive(true);
            });
            pressKey.gameObject.SetActive(false);
            _anyKey = true;
            Managers.Sound.SoundPlay("SFX/DM-CGS-02", SoundType.Effect);
        }
    }

    public void StartButton()
    {
        Managers.Sound.SoundPlay("SFX/DM-CGS-02", SoundType.Effect);
        Managers.Scenes.LoadScene(SceneNames.SelectCharacterScene);
    }

    public void SettingButton()
    {
        Managers.Resource.Instantiate("UI/SoundSettingUI", null);
    }

    public void RankButton(bool set)
    {
        rankingUI.SetActive(set);
    }

    public void ExitButton()
    {
        Managers.GameManager.QuitGame();
    }
}
