using System.Collections.Generic;
using com.konargus.helpers;

namespace TrapThem
{
    public class GameLevelsIterator : Iterator
    {
        private readonly List<GameLevel> _collection = new ();
        private int _position = -1;

        public List<GameLevel> GetItems()
        {
            return _collection;
        }
        
        public void Add(GameLevel item)
        {
            _collection.Add(item);
        }

        public override int Key()
        {
            return _position;
        }

        public override object Current()
        {
            return _collection[_position];
        }

        public override bool MoveNext()
        {
            var updatedPosition = _position + 1;

            if (updatedPosition < 0 || updatedPosition >= _collection.Count)
            {
                return false;
            }
            
            _position = updatedPosition;
            return true;
        }

        public override void Reset()
        {
            _position = -1;
        }
    }
}