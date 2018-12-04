using Framework.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Framework.Domain.Repository
{
    public interface IReadRepository<TEntiy, TKey>
        where TEntiy : DomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntiy GetByID(TKey key);
        IEnumerable<TEntiy> GetAll();
    }
}