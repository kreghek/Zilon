using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

namespace Zilon.Core.Common
{
    /// <summary>
    /// Базовая реализация менеджера сущностей.
    /// </summary>
    /// <typeparam name="TCombatEntity">
    /// Тип сущности сектора.
    /// См описание <see cref="IEntityManager{TCombatEntity}">IEntityManager</see>.
    /// </typeparam>
    [PublicAPI]
    public class EntityManager<TCombatEntity> : IEntityManager<TCombatEntity> where TCombatEntity : class
    {
        private readonly List<TCombatEntity> _items;

        public IEnumerable<TCombatEntity> Items => _items;

        protected EntityManager()
        {
            _items = new List<TCombatEntity>();
        }

        public void Add(TCombatEntity entity)
        {
            _items.Add(entity);

            DoAdded(entity);
        }

        public void Add(IEnumerable<TCombatEntity> entities)
        {
            var entityArray = entities.ToArray();
            _items.AddRange(entityArray);

            DoAdded(entityArray);
        }

        public void Remove(TCombatEntity entity)
        {
            _items.Remove(entity);

            DoRemoved(entity);
        }

        public void Remove(IEnumerable<TCombatEntity> entities)
        {
            var entityArray = entities.ToArray();
            foreach (var entity in entityArray)
            {
                _items.Remove(entity);
            }

            DoRemoved(entityArray);
        }

        public event EventHandler<ManagerItemsChangedEventArgs<TCombatEntity>> Added;
        public event EventHandler<ManagerItemsChangedEventArgs<TCombatEntity>> Removed;


        private void DoAdded(params TCombatEntity[] entities)
        {
            var args = new ManagerItemsChangedEventArgs<TCombatEntity>(entities);
            Added?.Invoke(this, args);
        }

        private void DoRemoved(params TCombatEntity[] entities)
        {
            var args = new ManagerItemsChangedEventArgs<TCombatEntity>(entities);
            Removed?.Invoke(this, args);
        }
    }
}
