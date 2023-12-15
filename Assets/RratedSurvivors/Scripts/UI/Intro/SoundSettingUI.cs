using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingUI : MonoBehaviour
{
    [SerializeField] Scrollbar bgm;
    [SerializeField] Scrollbar effect;
    [SerializeField] Scrollbar ui;

    private void Start()
    {
        bgm.value = Managers.Sound.GetVolume(SoundType.BGM);
        effect.value = Managers.Sound.GetVolume(SoundType.Effect);
        ui.value = Managers.Sound.GetVolume(SoundType.UI);
    }

    public void BgmBar()
    {
        Managers.Sound.SetVolume(SoundType.BGM, bgm.value);
    }

    public void EffectBar()
    {
        Managers.Sound.SetVolume(SoundType.Effect, effect.value);
    }

    public void UIBar()
    {
        Managers.Sound.SetVolume(SoundType.UI, ui.value);
    }

    public void ExitBtn()
    {
        Transform currentTransform = transform;

        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }

        Managers.Resource.Destroy(currentTransform.gameObject);
    }
}
