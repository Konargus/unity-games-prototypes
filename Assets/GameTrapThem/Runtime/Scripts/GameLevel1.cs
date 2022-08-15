using System;
using com.konargus.sfx;
using com.konargus.camera;
using com.konargus.persona_equipment;
using com.konargus.persona;
using com.konargus.traps;
using com.konargus.ui;
using UnityEngine;

namespace TrapThem
{
    public class GameLevel1 : GameLevel
    {
        [SerializeField] private Persona playerPrefab;
        [SerializeField] private Persona enemyPrefab;
        [SerializeField] private Knife knifePrefab;
        [SerializeField] private Trap trapPrefab;
        
        private IPersonaFactory _playerFactory;
        private IEquipmentFactory<MeleeWeapon> _knifeFactory;
        private IPersonaFactory _enemyFactory;
        private ITrapFactory _killTrapFactory;
        private ISpecialEffectFactory _sfxFactorySimpleBoom;
        private Transform LevelTransform => transform;


        public override event Action OnLoseConditionMet = delegate { };

        public override void Initialize(IGameView gameView)
        {
            gameView.Joystick.SetRadius(75);
            _playerFactory = new PersonaFactory<IsometricMovement>(playerPrefab, LevelTransform);

            var playerPersona = _playerFactory.CreatePersona();
            playerPersona.SetPosition(Vector3.zero);

            _knifeFactory = new EquipmentFactory<Knife>(knifePrefab);
            
            playerPersona.OnHealthDecrease += healthLeft =>
            {
                if (healthLeft > 0)
                    return;
                
                OnLoseConditionMet.Invoke();
            };
            
            gameView.Joystick.OnJoystickDrag += delta =>
            {
                playerPersona.Move(delta, 0.1f);
            };
            
            gameView.Joystick.OnJoystickDragEnd += () =>
            {
                playerPersona.Stop();
            };

            CustomCamera.Instance.LookAt(playerPersona.Transform);
            CustomCamera.Instance.Follow(playerPersona.Transform, new Vector3(20, 30, 0));

            SetWinCondition(5);
            
            _enemyFactory = new PersonaFactory<IsometricMovement>(enemyPrefab, LevelTransform);

            SpawnEnemiesOverTime(5, _enemyFactory);
            
            _killTrapFactory = new KillTrapFactory(trapPrefab, LevelTransform);
            _sfxFactorySimpleBoom = new SpecialEffectFactory(SpecialEffectPrefabs.SimpleDeath, LevelTransform);
            
            OnEnemySpawn += enemy =>
            {
                enemy.Follow(playerPersona.Transform, 4f);
                var meleeWeapon = _knifeFactory.CreateEquipment();
                meleeWeapon.Targets = new []{ playerPersona };
                enemy.EquipWeapon(meleeWeapon.transform);
                
                meleeWeapon.OnCollision += (colliderObj, damage) =>
                {
                    var persona = colliderObj.GetComponentInParent<IPersona>();
                    if (!meleeWeapon.Targets.Contains(persona))
                        return;
                    
                    persona.TakeDamage(damage);
                };
                
                enemy.OnHealthDecrease += healthLeft =>
                {
                    if (healthLeft > 0)
                        return;

                    enemy.Stop();
                    
                    var dieAnimation = _sfxFactorySimpleBoom.CreateSpecialEffect();
                    dieAnimation.SetPosition(enemy.Transform.position);
                    dieAnimation.SetChild(enemy.Transform);
                    
                    dieAnimation.PlayAnimation(() =>
                    {
                        enemy.Die();
                        EnemyDied(enemiesLeft =>
                        {
                            if (enemiesLeft > 0)
                            {
                                CreateTrap();
                            }
                        });
                    });
                };
            };
            
            CreateTrap();
        }
        
        private void CreateTrap()
        {
            var trap = SpawnTrap(_killTrapFactory);
            trap.OnCollision += (col, damage) =>
            {
                col.GetComponentInParent<IPersona>()?.TakeDamage(damage);
                trap.Destroy();
            };
        }
    }
}