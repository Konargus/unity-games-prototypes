using UnityEngine;
using UnityEngine.SceneManagement;

public interface ISceneManager
{
    /// <summary>
    /// Creates scene.
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    /// <param name="setActive">Set scene active.</param>
    /// <param name="gameObjects">GameObjects to add as a tree.</param>
    /// <returns>UnityEngine.SceneManagement.Scene</returns>
    Scene CreateScene(string sceneName, bool setActive, params GameObject[] gameObjects);

    /// <summary>
    /// Unloads active scene (Async).
    /// </summary>
    void UnloadSceneAsync(Scene scene);
}
