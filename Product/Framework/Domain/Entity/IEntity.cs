using System;

namespace Framework.Domain.Entity
{
    public interface IEntity : IEquatable<IEntity>
    {
        ulong Id { get; set; }
    }
}