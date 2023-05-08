using System;
using System.Collections.Generic;

namespace Localization_container
{
    public class NoSerializedPair<TKey, TValue> : IEquatable<NoSerializedPair<TKey, TValue>>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public NoSerializedPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public bool Equals(NoSerializedPair<TKey, TValue> other)
        {
            return EqualityComparer<TKey>.Default.Equals(Key, other.Key) &&
                   EqualityComparer< TValue>.Default.Equals(Value, other.Value);
        }
    }
}