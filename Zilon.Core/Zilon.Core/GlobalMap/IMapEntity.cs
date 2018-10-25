using Zilon.Core.Spatial;

namespace Zilon.Core.GlobalMap
{
    public interface IMapEntity
    {
        ITerrainNode Node { get; }

        void MoveTo(ITerrainNode targetNode);
    }
}
