﻿using System;
using System.Collections.Generic;

namespace Zilon.Core.Common
{
    /// <summary>
    /// Общий интерфейс для всех менеджеров сущностей сектора
    /// </summary>
    /// <typeparam name="TCombatEntity">
    /// Тип сущности сектора.
    /// Сейчас это <see cref="Combat.ICombatSquad">IActor</see>.
    /// </typeparam>
    public interface IEntityManager<TCombatEntity> where TCombatEntity : class
    {
        /// <summary>
        /// Текущий список всех актёров.
        /// </summary>
        IEnumerable<TCombatEntity> Items { get; }

        /// <summary>
        /// Добавляет сущность в общий список.
        /// </summary>
        /// <param name="entity"> Целевая сущность. </param>
        void Add(TCombatEntity entity);

        /// <summary>
        /// Добавляет несколько сущностей в общикй список.
        /// </summary>
        /// <param name="entities"> Набор целевых сущностей. </param>
        void Add(IEnumerable<TCombatEntity> entities);

        /// <summary>
        /// Удаляет актёра из общего списка.
        /// </summary>
        /// <param name="entity"> Целевая сущность. </param>
        void Remove(TCombatEntity entity);

        /// <summary>
        /// Удаляет актёра из общего списка.
        /// </summary>
        /// <param name="entities"> Набор целевых сущностей. </param>
        void Remove(IEnumerable<TCombatEntity> entities);

        /// <summary>
        /// Событие выстреливает, когда в менеджере добавляются сущности.
        /// </summary>
        event EventHandler<ManagerItemsChangedEventArgs<TCombatEntity>> Added;

        /// <summary>
        /// Событие выстреливает, когда из менеджера удаляются сущности.
        /// </summary>
        event EventHandler<ManagerItemsChangedEventArgs<TCombatEntity>> Removed;
    }
}
