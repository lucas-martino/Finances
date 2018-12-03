using Finances.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Finances.Domain.Repository
{
    public interface IReadRepository<TEntiy, TKey>
        where TEntiy : DomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TEntiy GetByID(TKey key);
        IEnumerable<TEntiy> GetAll();
    }
}