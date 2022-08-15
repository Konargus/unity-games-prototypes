using com.konargus.scenes;
using com.konargus.ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrapThem
{
    public class GameScenes
    {
        private readonly IUIBuilder _uiBuilder;
        private readonly ISceneFactory _sceneFactory;
        private readonly IGameLevelFactory _gameLevelFactory;
        private IGameLevel _loadedGameLevel;

        internal GameScenes(IUIBuilder uiBuilder, ISceneFactory sceneFactory, IGameLevelFactory gameLevelFactory)
        {
            _uiBuilder = uiBuilder;
            _sceneFactory = sceneFactory;
            _gameLevelFactory = gameLevelFactory;
        }
        
        internal void LoadHomeScene()
        {
            var (menu, scene) = CreateSceneWithSimpleMenu("Home", true);
            var button = menu.AddButtonText();
            button.SetLabelText("Let's Play");
            
            button.OnClick += () =>
            {
                SceneManager.UnloadSceneAsync(scene);
                LoadGameScene();
            };
        }

        private void LoadGameScene()
        {
            var (canvas, gameView) = _uiBuilder.BuildGameView();
            var scene = _sceneFactory.CreateScene("Game", true, canvas.gameObject, gameView.GameObject);
            CreateGameLevel(gameView, scene);
        }

        private void CreateGameLevel(IGameView gameView, Scene scene)
        {
            var (level, isLast) = _gameLevelFactory.BuildNext();

            _loadedGameLevel?.Destroy();

            _loadedGameLevel = level;
            _loadedGameLevel.Initialize(gameView);
        
            _loadedGameLevel.OnWinConditionMet += () =>
            {
                if (isLast)
                {
                    SceneManager.UnloadSceneAsync(scene);
                    LoadEndScene("Success :)");
                }
                else
                {
                    CreateGameLevel(gameView, scene);
                }
            };
        
            _loadedGameLevel.OnLoseConditionMet += () =>
            {
                SceneManager.UnloadSceneAsync(scene);
                LoadEndScene("Failure :(");
            };
        }

        private void LoadEndScene(string labelText)
        {
            _loadedGameLevel = null;
            var (menu, scene) = CreateSceneWithSimpleMenu("End", true);
            var button = menu.AddButtonText();
            button.SetLabelText("Go Home");
            
            button.OnClick += () =>
            {
                SceneManager.UnloadSceneAsync(scene);
                LoadHomeScene();
            };
            
            var label = menu.AddLabel();
            label.SetText(labelText);
            label.SetOffsetPosition(new Vector3(0, -100, 0));
        }
        
        private (IMenuSimple, Scene) CreateSceneWithSimpleMenu(string sceneName, bool setActive)
        {
            var (canvas, menu) = _uiBuilder.BuildSimpleMenu();
            var scene = _sceneFactory.CreateScene(sceneName, setActive, canvas.gameObject, menu.GameObject);
            return (menu, scene);
        }
    }
}