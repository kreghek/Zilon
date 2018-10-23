namespace Zilon.Core.Spatial
{
    /// <summary>
    /// Интерфейс ребра графа карты.
    /// </summary>
    public interface ITerrainEdge
    {
        /// <summary>
        /// Узел начала ребра.
        /// </summary>
        ITerrainNode StartNode { get; }

        /// <summary>
        /// Узел конца ребра.
        /// </summary>
        ITerrainNode EndNode { get; }
    }
}
