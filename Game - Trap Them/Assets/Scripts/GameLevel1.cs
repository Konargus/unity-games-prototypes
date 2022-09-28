using System;
using com.konargus.vfx;
using com.konargus.camera;
using com.konargus.persona;
using com.konargus.traps;
using com.konargus.ui;
using UnityEngine;

namespace TrapThem
{
    public class GameLevel1 : GameLevel
    {
        [Header("Player")]
        [SerializeField] private Persona playerPrefab;
        
        [Header("Enemy")]
        [SerializeField] private PersonaWithToxicSpots enemyPrefab;
        
        [Header("Traps")]
        [SerializeField] private Trap trapPrefab;
        
        private Animator _playerAnimator;
        
        private IPersonaFactory _playerFactory;
        private IPersonaFactory _enemyFactory;
        private ITrapFactory _killTrapFactory;
        private IVisualEffectFactory _vfxFactorySimpleDeath;
        private IPersona _playerPersona;
        private Transform LevelTransform => transform;

        public override event Action OnLoseConditionMet = delegate { };

        public override void Initialize(IGameView gameView)
        {
            gameView.Joystick.SetRadius(75);
            _playerFactory = new PersonaFactory<IsometricMovement>(playerPrefab, LevelTransform);

            _playerPersona = _playerFactory.CreatePersona();
            _playerPersona.SetPosition(Vector3.zero);

            _playerPersona.OnHealthDecrease += PlayerOnHealthDecrease;
            _playerPersona.OnStateChange += PlayerOnStateChangeHandler;
            
            gameView.Joystick.OnJoystickDrag += delta =>
            {
                _playerPersona.Move(delta, 6f);
            };
            
            gameView.Joystick.OnJoystickDragEnd += () =>
            {
                _playerPersona.Stop();
            };
            
            CustomCamera.Instance.Follow(_playerPersona.Transform, new Vector3(0, 5, 10));
            CustomCamera.Instance.LookAt(_playerPersona.Transform, new Vector3(0, 5, 10));

            SetWinCondition(5);
            
            _enemyFactory = new PersonaFactory<IsometricMovement>(enemyPrefab, LevelTransform);
            SpawnEnemies(_enemyFactory);
            
            _killTrapFactory = new InstantKillTrapFactory(trapPrefab, LevelTransform);
            
            OnEnemyCreated += enemy =>
            {
                enemy.OnStateChange += state =>
                {
                    switch (state)
                    {
                        case PersonaState.Idle:
                            enemy.Animator.SetBool(DogAnimatorData.BoolHashes.Run, false);
                            enemy.Animator.Play(DogAnimatorData.StateHashes.Idle);
                            break;
                        case PersonaState.IsMoving:
                            enemy.Animator.SetBool(DogAnimatorData.BoolHashes.Run, true);
                            break;
                        case PersonaState.Dead:
                            enemy.Animator.SetTrigger(DogAnimatorData.TriggerHashes.Dead);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(state), state, null);
                    }
                };
                
                if (enemy is IPersonaWithToxicSpots toxicEnemy)
                {
                    toxicEnemy.VulnerableTargets = new [] { _playerPersona };
                    toxicEnemy.OnToxicSpotCollision += (colliderObj, damage) =>
                    {
                        var persona = colliderObj.GetComponentInParent<IPersona>();
                        if (!toxicEnemy.VulnerableTargets.Contains(persona))
                            return;

                        persona.TakeDamage(damage);
                    };
                }
                
                enemy.OnHealthDecrease += healthLeft =>
                {
                    if (healthLeft > 0)
                        return;

                    enemy.Stop();
                    enemy.Die();
                    
                    EnemyDied(enemiesLeft =>
                    {
                        if (enemiesLeft > 0)
                        {
                            SpawnEnemies(_enemyFactory);
                            CreateTrap();
                        }
                    });
                };
                
                enemy.Follow(_playerPersona.Transform, 4f);
            };

            OnEnemyRespawned += enemy =>
            {
                enemy.Follow(_playerPersona.Transform, 4f);
            };
            
            CreateTrap();
        }
        
        private void PlayerOnStateChangeHandler(PersonaState state)
        {
            switch (state)
            {
                case PersonaState.Idle:
                    _playerPersona.Animator.SetBool(CatAnimatorData.BoolHashes.Moving, false);
                    break;
                case PersonaState.IsMoving:
                    _playerPersona.Animator.SetBool(CatAnimatorData.BoolHashes.Moving, true);
                    break;
                case PersonaState.Dead:
                    _playerPersona.Animator.SetBool(CatAnimatorData.BoolHashes.Dead, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void PlayerOnHealthDecrease(int healthLeft)
        {
            _playerPersona.Animator.SetTrigger(CatAnimatorData.TriggerHashes.Hit);
            if (healthLeft > 0)
            {
                return;
            }
                
            _playerPersona.Stop();
            _playerPersona.Die();
            OnLoseConditionMet.Invoke();
        }

        private void OnDestroy()
        {
            _playerPersona.OnStateChange -= PlayerOnStateChangeHandler;
            _playerPersona.OnHealthDecrease -= PlayerOnHealthDecrease;
        }

        private void CreateTrap()
        {
            var trap = SpawnTrap(_killTrapFactory);
            trap.OnCollision += (col, damage) =>
            {
                col.GetComponent<IPersona>().TakeDamage(damage);
                trap.Destroy();
            };
        }
    }
}