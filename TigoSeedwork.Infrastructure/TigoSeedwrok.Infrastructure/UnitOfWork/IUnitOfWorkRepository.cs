using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TigoSeedwork.Domain;

namespace TigoSeedwork.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(IEntity item);
        void PersistUpdatedItem(IEntity item);
        void PersistDeletedItem(IEntity item);
    }
}
