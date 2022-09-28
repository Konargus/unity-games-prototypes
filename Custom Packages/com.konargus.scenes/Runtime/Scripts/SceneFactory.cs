using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.konargus.scenes
{
    public class SceneFactory : ISceneFactory
    {
        public Scene CreateScene(string sceneName, bool setActive, params GameObject[] gameObjects)
        {
            var scene = SceneManager.CreateScene(sceneName);
            Transform parent = null;
            foreach (var go in gameObjects)
            {
                if (parent == null)
                {
                    SceneManager.MoveGameObjectToScene(go, scene);
                    parent = go.transform;
                }
                else
                {
                    go.transform.SetParent(parent, false);
                }
            }
            if (setActive)
            {
                SceneManager.SetActiveScene(scene);
            }
            return scene;
        }
    }
}
