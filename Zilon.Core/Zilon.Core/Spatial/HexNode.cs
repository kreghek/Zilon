namespace Zilon.Core.Spatial
{
    public sealed class HexNode: ITerrainNode
    {
        public OffsetCoords Offset { get; }

        public HexNode(int offsetX, int offsetY)
        {
            var offset = new OffsetCoords(offsetX, offsetY);
            Offset = offset;
        }

        public override string ToString()
        {
            return $"{Offset}";
        }
    }
}