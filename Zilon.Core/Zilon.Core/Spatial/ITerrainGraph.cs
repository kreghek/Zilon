using System.Collections.Generic;

using JetBrains.Annotations;

namespace Zilon.Core.Spatial
{
    /// <summary>
    /// Пространственная карта в виде графа.
    /// </summary>
    public interface ITerrainGraph
    {
        [PublicAPI]
        /// <summary>
        /// Список узлов карты.
        /// </summary>
        IEnumerable<ITerrainNode> Nodes { get; }

        [PublicAPI]
        /// <summary>
        /// Предоставляет список узлов, с которыми соединён указанный узел.
        /// </summary>
        /// <param name="node"> Проверяемый узел. </param>
        /// <returns> Возвращает список соседних узлов. </returns>
        IEnumerable<ITerrainNode> GetNeighborNodes(ITerrainNode node);
    }
}
