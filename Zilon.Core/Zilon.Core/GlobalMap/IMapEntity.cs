using JetBrains.Annotations;

using Zilon.Core.Spatial;

namespace Zilon.Core.GlobalMap
{
    public interface IMapEntity
    {
        [PublicAPI]
        ITerrainNode Node { get; }

        [PublicAPI]
        void MoveTo(ITerrainNode targetNode);
    }
}
