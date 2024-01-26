using UnityEngine.SceneManagement;

namespace Base
{
    using UnityEngine;
    public class SceneSystem : Singleton<SceneSystem>
    {
        public void LoadScene(EScene scene)
        {
            LoadScene(scene, LoadSceneMode.Single);
        }
        
        public AsyncOperation LoadSceneAsync(EScene scene)
        {
            return LoadSceneAsync(scene, LoadSceneMode.Single);
        }
        
        public void LoadScene(EScene scene, LoadSceneMode mode)
        {
            var sceneName = scene.ToString();
            // 可以在这里添加切换场景前的逻辑，比如播放动画等
            SceneManager.LoadScene(sceneName, mode);
        }

        public AsyncOperation LoadSceneAsync(EScene scene, LoadSceneMode mode)
        {
            // 异步加载场景
            var sceneName = scene.ToString();
            return SceneManager.LoadSceneAsync(sceneName);
        }

        private void HandleSceneLoaded(string sceneName)
        {
            
        }

        private void HandleSceneUnloaded(string sceneName)
        {
            
        }
    }
}