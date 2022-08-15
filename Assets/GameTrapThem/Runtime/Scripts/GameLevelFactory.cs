using System;
using Object = UnityEngine.Object;

namespace TrapThem
{
    public class GameLevelFactory : IGameLevelFactory
    {
        private readonly GameLevelsIterator _iterator;
        
        internal GameLevelFactory(GameLevelsIterator iterator)
        {
            _iterator = iterator;
        }

        public (IGameLevel, bool) BuildNext()
        {
            if (_iterator.GetItems().Count == 0)
            {
                throw new Exception("No game levels found");
            }
            
            var next = _iterator.MoveNext();

            if (!next)
            {
                _iterator.Reset();
            }
            
            var level = Object.Instantiate(_iterator.Current() as GameLevel);
            return (level, _iterator.Key() == _iterator.GetItems().Count - 1);
        }
    }
}