using System;
using System.Collections;
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

        public virtual event Action<IPersona> OnEnemySpawn = delegate {  };
        public virtual event Action OnWinConditionMet = delegate {  };
        public virtual event Action OnLoseConditionMet = delegate {  };

        private int _enemiesToSpawn;
        private int _enemiesToKill;
        
        protected virtual void SetWinCondition(int enemiesToKill)
        {
            _enemiesToKill = enemiesToKill;
        }

        protected virtual void EnemyDied(Action<int> enemiesLeft)
        {
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

        protected virtual void SpawnEnemiesOverTime(int amount, IPersonaFactory personaFactory)
        {
            _enemiesToSpawn = amount;
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
            var spawnAmount = _enemiesToSpawn >= 3 ? Random.Range(1, 3) : Random.Range(1, _enemiesToSpawn);
            _enemiesToSpawn -= spawnAmount;
            for (var i = 0; i < spawnAmount; i++)
            {
                var enemy = personaFactory.CreatePersona();
                var spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
                enemy.SetPosition(spawnPoint.position);
                OnEnemySpawn.Invoke(enemy);
            }
            if (_enemiesToSpawn <= 0)
            {
                yield break;
            }
            StartCoroutine(SpawnEnemiesCoroutine(personaFactory));
        }
    }
}
