﻿using JetBrains.Annotations;

namespace Zilon.Core.Spatial
{
    public static class HexHelper
    {
        /// <summary>
        /// Возвращает смещения по часовой стрелке.
        /// </summary>
        /// <returns> Массив со смещениями. </returns>
        public static CubeCoords[] GetOffsetClockwise()
        {
            var offsets = new[]
            {
                new CubeCoords(-1, +1, 0), new CubeCoords(-1, 0, +1), new CubeCoords(0, -1, +1),
                new CubeCoords(+1, -1, 0),new CubeCoords(+1, 0, -1),new CubeCoords(0, +1, -1)
            };

            return offsets;
        }

        public static CubeCoords ConvertToCube(int offsetX, int offsetY)
        {
            var x = offsetX - (offsetY - (offsetY & 1)) / 2;
            var z = offsetY;
            var y = -x - z;

            return new CubeCoords(x, y, z);
        }

        public static OffsetCoords ConvertToOffset(CubeCoords cube)
        {
            var col = cube.X + (cube.Z - (cube.Z & 1)) / 2;
            var row = cube.Z;
            return new OffsetCoords(col, row);
        }

        [PublicAPI]
        public static float[] ConvertToWorld(int offsetX, int offsetY)
        {
            var rowOffset = offsetY % 2 == 0 ? 0 : 0.5f;
            return new[] {
                offsetX + rowOffset,
                offsetY * 3f / 4
            };
        }
    }
}
