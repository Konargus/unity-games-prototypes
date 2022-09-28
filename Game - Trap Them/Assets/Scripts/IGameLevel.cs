using System;
using com.konargus.ui;

namespace TrapThem
{
    public interface IGameLevel
    {
        event Action OnWinConditionMet;
        event Action OnLoseConditionMet;
        void Initialize(IGameView gameView);
        void Destroy();
    }
}