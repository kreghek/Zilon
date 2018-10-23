namespace Zilon.Core.Spatial
{
    /// <summary>
    /// Интерфейс узла графа карты.
    /// </summary>
    public interface ITerrainNode
    {
        /// <summary>
        /// Координаты смещения узла для графического представления.
        /// </summary>
        OffsetCoords Offset { get; }
    }
}
