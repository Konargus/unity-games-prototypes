using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.konargus.scenes
{
    public interface ISceneFactory
    {
        /// <summary>
        /// Creates scene.
        /// </summary>
        /// <param name="sceneName">Scene name.</param>
        /// <param name="setActive">Set scene active.</param>
        /// <param name="gameObjects">GameObjects to add as a tree.</param>
        /// <returns>UnityEngine.SceneManagement.Scene</returns>
        Scene CreateScene(string sceneName, bool setActive, params GameObject[] gameObjects);
    }
}
