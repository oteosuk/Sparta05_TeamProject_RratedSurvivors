using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneNames
{
    StartScene,
    SelectCharacterScene,
    DungeonScene
}

public class ScenesManager
{
    public void LoadScene(SceneNames scene)
    {
        Managers.Clear();
        SceneManager.LoadScene(scene.ToString());
    }

    public void StartSceneLoad()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void SelectCharacterSceneLoad()
    {
        SceneManager.LoadScene("SelectCharacterScene");
    }
    public void MainGameSceneLoad()
    {
        SceneManager.LoadScene("DungeonScene");
    }
}
