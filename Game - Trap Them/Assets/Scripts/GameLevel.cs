using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.konargus.persona;
using com.konargus.traps;
using com.konargus.ui;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TrapThem
{
    public abstract class GameLevel : MonoBehaviour, IGameLevel
    {
        [SerializeField] private Transform[] enemySpawnPoints;
        [SerializeField] private Transform[] trapsSpawnPoints;
        [SerializeField] private int maxNumberOfEnemies = 5;

        public virtual event Action<IPersona> OnEnemyCreated = delegate {  };
        public virtual event Action<IPersona> OnEnemyRespawned = delegate {  };
        public virtual event Action OnWinConditionMet = delegate {  };
        public virtual event Action OnLoseConditionMet = delegate {  };

        private readonly IList<EnemyStructure> _enemiesPool = new List<EnemyStructure>();
        private int _enemiesActive;
        private int _enemiesToKill;
        
        protected virtual void SetWinCondition(int enemiesToKill)
        {
            _enemiesToKill = enemiesToKill;
        }

        protected virtual void EnemyDied(Action<int> enemiesLeft)
        {
            _enemiesActive--;
            _enemiesToKill--;
            if (_enemiesToKill == 0)
            {
                OnWinConditionMet.Invoke();
            }
            else
            {
                enemiesLeft.Invoke(_enemiesToKill);
            }
        }

        protected virtual void SpawnEnemies(IPersonaFactory personaFactory)
        {
            StartCoroutine(SpawnEnemiesCoroutine(personaFactory));
        }

        protected virtual ITrap SpawnTrap(ITrapFactory trapFactory)
        {
            var spawnPoint = trapsSpawnPoints[Random.Range(0, trapsSpawnPoints.Length)];
            var trap = trapFactory.CreateTrap();
            trap.Position = spawnPoint.position;
            return trap;
        }

        public abstract void Initialize(IGameView gameView);
        
        public void Destroy()
        {
            Destroy(gameObject);
        }

        private IEnumerator SpawnEnemiesCoroutine(IPersonaFactory personaFactory)
        {
            yield return new WaitForSeconds(2);
            
            if (_enemiesActive >= maxNumberOfEnemies)
            {
                yield break;
            }
            
            var spawnAmount = maxNumberOfEnemies - _enemiesActive >= 3
                ? Random.Range(1, 3)
                : Random.Range(1, maxNumberOfEnemies - _enemiesActive);
            
            for (var i = 0; i < spawnAmount; i++)
            {
                var enemy = _enemiesPool.FirstOrDefault(es
                    => es.FactoryHash == personaFactory.GetHashCode() && es.Persona.State == PersonaState.Dead).Persona;
                
                if (enemy == null)
                {
                    enemy = personaFactory.CreatePersona();
                    _enemiesPool.Add(new EnemyStructure
                    {
                        FactoryHash = personaFactory.GetHashCode(),
                        Persona = enemy,
                    });
                    OnEnemyCreated.Invoke(enemy);
                }
                else
                {
                    enemy.Reset();
                    OnEnemyRespawned.Invoke(enemy);
                }
                
                var spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
                enemy.SetPosition(spawnPoint.position);
                _enemiesActive++;
            }
                
            if (_enemiesActive < maxNumberOfEnemies)
            {
                SpawnEnemies(personaFactory);
            }
        }
    }
}
