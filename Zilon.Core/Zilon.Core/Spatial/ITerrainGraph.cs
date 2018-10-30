using System.Collections.Generic;

using JetBrains.Annotations;

namespace Zilon.Core.Spatial
{
    /// <summary>
    /// Пространственная карта в виде графа.
    /// </summary>
    public interface ITerrainGraph
    {
        /// <summary>
        /// Список узлов карты.
        /// </summary>
        [PublicAPI]
        IEnumerable<ITerrainNode> Nodes { get; }

        /// <summary>
        /// Предоставляет список узлов, с которыми соединён указанный узел.
        /// </summary>
        /// <param name="node"> Проверяемый узел. </param>
        /// <returns> Возвращает список соседних узлов. </returns>
        [PublicAPI]
        IEnumerable<ITerrainNode> GetNeighborNodes(ITerrainNode node);
    }
}
