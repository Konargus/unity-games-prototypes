using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TrapThem
{
    public class GameLevel : MonoBehaviour, IGameLevel
    {
        [SerializeField] private Transform[] enemySpawnPoints;
        [SerializeField] private Transform[] trapsSpawnPoints;

        public event Action<ICharacter> OnEnemySpawn = delegate {  };
        public event Action OnWinConditionMet = delegate {  };

        private int _enemiesToSpawn;
        private int _enemiesToKill;
        
        public virtual void SetWinCondition(int enemiesToKill)
        {
            _enemiesToKill = enemiesToKill;
        }

        public virtual void EnemyDied(Action<int> enemiesLeft)
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
        
        public virtual void SpawnEnemiesOverTime(int amount, Character character)
        {
            _enemiesToSpawn = amount;
            StartCoroutine(SpawnEnemiesCoroutine(character));
        }
        
        public virtual ITrap SpawnTrap(Trap trapPrefab)
        {
            var spawnPoint = trapsSpawnPoints[Random.Range(0, trapsSpawnPoints.Length)];
            var trap = Instantiate(trapPrefab);
            trap.gameObject.transform.position = spawnPoint.position;
            return trap;
        }

        private IEnumerator SpawnEnemiesCoroutine(Character character)
        {
            yield return new WaitForSeconds(2);
            var spawnAmount = _enemiesToSpawn >= 3 ? Random.Range(1, 3) : Random.Range(1, _enemiesToSpawn);
            _enemiesToSpawn -= spawnAmount;
            for (var i = 0; i < spawnAmount; i++)
            {
                var enemy = SpawnEnemy(character);
                OnEnemySpawn.Invoke(enemy);
                yield return new WaitForSeconds(0.1f);
            }
            if (_enemiesToSpawn <= 0)
            {
                yield break;
            }
            StartCoroutine(SpawnEnemiesCoroutine(character));
        }
        
        private ICharacter SpawnEnemy(Character character)
        {
            var spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
            var enemy = Instantiate(character);
            var movementType = enemy.gameObject.AddComponent<IsometricMovement>();
            enemy.SetMovementType(movementType);
            enemy.Transform.position = spawnPoint.position;
            return enemy;
        }
    }
}
