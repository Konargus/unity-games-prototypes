using System;
using UnityEngine;
using JoystickMobile;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrapThem
{
    public class AppStart : MonoBehaviour
    {
        [SerializeField] private MeleeWeapon knifePrefab;
        [SerializeField] private GameLevel level1Prefab;
        [SerializeField] private Character playerCharacter;
        [SerializeField] private Character enemyCharacter;
        [SerializeField] private Trap trapPrefab;
        [SerializeField] private Menus menus;
        [SerializeField] private Joystick joystickPrefab;
        [SerializeField] private Sprite background;
        [SerializeField] private Sprite knob;

        private ISceneManager _sceneManager;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _sceneManager = new SceneManager();
            Camera.Instance.LookAt(transform);
            LoadHomeScene();
            Application.targetFrameRate = 60;
        }

        private void LoadHomeScene()
        {
            var (menu, scene) = CreateMenuScene<MenuSimple>("Home", true);
            var button = ((IMenu) menu).AddButtonText();
            button.SetLabelText("Let's Play");
            
            button.OnClick += () =>
            {
                _sceneManager.UnloadSceneAsync(scene);
                LoadGameScene(level1Prefab);
            };
        }

        private void LoadGameScene(GameLevel gameLevelPrefab)
        {
            var (menu, scene) = CreateMenuScene<MenuSimple>("Game", true);

            var gameLevel = Instantiate(gameLevelPrefab);
            
            var joystick = Instantiate(joystickPrefab, menu.transform) as IJoystick;
            joystick.SetRadius(75);
            joystick.SetSpriteBackground(background);
            joystick.SetSpriteKnob(knob);

            var characterGo = Instantiate(playerCharacter).gameObject;
            var movementType = characterGo.AddComponent<IsometricMovement>();
            
            var character = characterGo.GetComponent<ICharacter>();
            character.SetMovementType(movementType);

            var mainCamera = Camera.Instance as ICamera;
            
            mainCamera.LookAt(character.Transform);
            mainCamera.Follow(character.Transform, new Vector3(20, 30, 0));

            joystick.OnJoystickDrag += delta =>
            {
                character.Move(delta, 0.1f);
            };
            
            joystick.OnJoystickDragEnd += () =>
            {
                character.Stop();
            };

            IGameLevel level = gameLevel;
            level.SetWinCondition(5);
            
            level.OnWinConditionMet += () =>
            {
                _sceneManager.UnloadSceneAsync(scene);
                LoadEndScene("You Won! :D");
            };
            
            level.SpawnEnemiesOverTime(5, enemyCharacter);
            
            level.OnEnemySpawn += enemy =>
            {
                var weapon = Instantiate(knifePrefab);
                weapon.OnCollidedWithPlayer += (damage) =>
                {
                    character.TakeDamage(damage);
                };
                enemy.EquipWeapon(weapon);
                enemy.Follow(character.Transform, 4f);
                enemy.OnHealthDecrease += healthLeft =>
                {
                    if (healthLeft > 0)
                        return;

                    enemy.Die();
                    level.EnemyDied((enemiesLeft) =>
                    {
                        if (enemiesLeft > 0)
                        {
                            SpawnTrap(level);
                        }
                    });
                };
            };
            
            SpawnTrap(level);
            
            character.OnHealthDecrease += healthLeft =>
            {
                if (healthLeft > 0)
                    return;
                
                _sceneManager.UnloadSceneAsync(scene);
                LoadEndScene("You Lost :(");
            };
        }
        
        private void LoadEndScene(string labelText)
        {
            var (menu, scene) = CreateMenuScene<MenuSimple>("End", true);
            var button = ((IMenu) menu).AddButtonText();
            button.SetLabelText("Go Home");
            
            button.OnClick += () =>
            {
                _sceneManager.UnloadSceneAsync(scene);
                LoadHomeScene();
            };
            
            var label = ((IMenu) menu).AddLabel();
            label.SetText(labelText);
            label.SetOffsetPosition(new Vector3(0, -100, 0));
        }

        private void SpawnTrap(IGameLevel level)
        {
            var trap = level.SpawnTrap(trapPrefab);
            trap.OnCollidedWithCharacter += (col, damage) =>
            {
                col.GetComponentInParent<ICharacter>().TakeDamage(damage);
                trap.Destroy();
            };
        }
        
        private (T, Scene) CreateMenuScene<T>(string sceneName, bool setActive) where T : Menu
        {
            var canvas = new GameObject("Canvas", typeof(GraphicRaycaster)).GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
            
            var canvasScaler = canvas.gameObject.AddComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = 0.5f;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            Menu menuToInstantiate;
            
            if (typeof(T) == typeof(MenuSimple))
            {
                menuToInstantiate = Instantiate(menus.menuSimple);
            }
            else
            {
                throw new Exception("Unknown menu type");
            }

            var scene = _sceneManager.CreateScene(sceneName, setActive, canvas.gameObject, menuToInstantiate.gameObject);
            ((IMenu) menuToInstantiate).Stretch();
            return (menuToInstantiate as T, scene);
        }
    }
}
