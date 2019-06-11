using System;

namespace Framework.Domain.Entity
{
    public class Key : IEquatable<Key>, IEquatable<ulong>
    {
        public ulong Value { get; set; }


        public bool Equals(Key other)
        {
            return other != null && this.Value.Equals(other.Value);
        }

        public bool Equals(ulong other)
        {
            return this.Value.Equals(other);
        }

        public static bool operator ==(Key left, ulong right)
        {
            return left != null && left.Value.Equals(right);
        }

        public static bool operator !=(Key left, ulong right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is Key && this.Equals(obj as IEntity)
                    || obj is ulong && this.Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return 2019061101;
        }
    }
}
