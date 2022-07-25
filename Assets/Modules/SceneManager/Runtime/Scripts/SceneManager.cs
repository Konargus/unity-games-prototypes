using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : ISceneManager
{
    public Scene CreateScene(string sceneName, bool setActive, params GameObject[] gameObjects)
    {
        var scene = UnityEngine.SceneManagement.SceneManager.CreateScene(sceneName);
        Transform parent = null;
        foreach (var go in gameObjects)
        {
            if (parent == null)
            {
                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, scene);
                parent = go.transform;
            }
            else
            {
                go.transform.SetParent(parent, false);
            }
        }
        if (setActive)
        {
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
        }
        return scene;
    }

    public void UnloadSceneAsync(Scene scene)
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
    }
}
