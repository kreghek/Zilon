namespace Zilon.Core.Spatial
{
    /// <summary>
    /// Пространственная карта в виде графа.
    /// </summary>
    public interface ITerrainGraph<TNode> where TNode : ITerrainNode
    {
        /// <summary>
        /// Список узлов карты.
        /// </summary>
        ITerrainNode[] Nodes { get; }

        /// <summary>
        /// Предоставляет список узлов, с которыми соединён указанный узел.
        /// </summary>
        /// <param name="node"> Проверяемый узел. </param>
        /// <returns> Возвращает список соседних узлов. </returns>
        ITerrainNode[] GetNeighborNodes(ITerrainNode node);
    }
}
