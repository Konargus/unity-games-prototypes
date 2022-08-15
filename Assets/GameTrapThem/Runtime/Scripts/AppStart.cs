using com.konargus.camera;
using com.konargus.scenes;
using com.konargus.ui;
using UnityEngine;

namespace TrapThem
{
    public class AppStart : MonoBehaviour
    {
        [SerializeField] private GameLevel[] gameLevelPrefabs;
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            var gameLevels = new GameLevelsIterator();
            foreach (var prefab in gameLevelPrefabs)
            {
                gameLevels.Add(prefab);
            }
            var gameScenes = new GameScenes(new UIBuilder(), new SceneFactory(), new GameLevelFactory(gameLevels));
            gameScenes.LoadHomeScene();
            CustomCamera.Instance.LookAt(transform);
            Application.targetFrameRate = 60;
        }
    }
}
