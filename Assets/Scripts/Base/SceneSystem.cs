using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    public void LoadScene(EScene scene)
    {
        EventSystem.Instance.Invoke(EEvent.BeforeLoadScene);
        string sceneName = scene.ToString();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}