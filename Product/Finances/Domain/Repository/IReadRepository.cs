using Finances.Domain.Entity;
using System.Collections.Generic;

namespace Finances.Domain.Repository
{
    public interface IReadRepository<TEntiy>
        where TEntiy : DomainEntity
    {
        TEntiy GetByID(int key);
        IEnumerable<TEntiy> GetAll();
    }
}