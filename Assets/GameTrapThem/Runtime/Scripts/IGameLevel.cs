using System;

namespace TrapThem
{
    public interface IGameLevel
    {
        event Action<ICharacter> OnEnemySpawn;
        event Action OnWinConditionMet;
        void SetWinCondition(int enemiesToKill);
        void EnemyDied(Action<int> enemiesLeft);
        void SpawnEnemiesOverTime(int amount, Character character);
        ITrap SpawnTrap(Trap trap);
    }
}