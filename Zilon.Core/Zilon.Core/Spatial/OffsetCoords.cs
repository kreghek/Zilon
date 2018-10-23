using System.Diagnostics.CodeAnalysis;

namespace Zilon.Core.Spatial
{
    public struct OffsetCoords
    {
        public int X { get; }
        public int Y { get; }

        public OffsetCoords(int x, int y)
        {
            X = x;
            Y = y;
        }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (!(obj is OffsetCoords))
            {
                return false;
            }

            var coords = (OffsetCoords)obj;
            return X == coords.X &&
                   Y == coords.Y;
        }
    }
}
