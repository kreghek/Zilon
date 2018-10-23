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
        /// Ребра карты.
        /// </summary>
        ITerrainEdge[] Edges { get; }
    }
}
