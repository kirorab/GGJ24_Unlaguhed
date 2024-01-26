using UnityEngine.SceneManagement;

namespace Base
{
    using UnityEngine;
    public class SceneSystem : Singleton<SceneSystem>
    {
        public void LoadScene(EScene scene)
        {
            // 可以在这里添加切换场景前的逻辑，比如播放动画等
            SceneManager.LoadScene(scene.ToString());
        }

        public AsyncOperation LoadSceneAsync(EScene scene)
        {
            // 异步加载场景
            return SceneManager.LoadSceneAsync(scene.ToString());
        }
    }
}