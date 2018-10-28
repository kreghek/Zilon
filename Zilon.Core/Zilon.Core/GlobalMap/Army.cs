using Zilon.Core.Spatial;

namespace Zilon.Core.GlobalMap
{
    public class Army : IMapEntity
    {
        public ITerrainNode Node { get; private set; }

        public void MoveTo(ITerrainNode targetNode)
        {
            Node = targetNode;
        }
    }
}
