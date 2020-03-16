using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Lookups
{
    public class ColorLookup
    {
        public ColorLookup(Color color)
        {
            Color = color;
        }

        public Color Color { get; }

        public static IEqualityComparer<ColorLookup> ColorLookupComparer { get; } = new ColorLookupEqualityComparer();

        private sealed class ColorLookupEqualityComparer : IEqualityComparer<ColorLookup>
        {
            public bool Equals(ColorLookup x, ColorLookup y)
            {
                if (x == null && y == null)
                {
                    return true;
                }
                else if (x == null || y == null)
                {
                    return false;
                }

                return x.Color.Id == y.Color.Id && x.Color.Name == y.Color.Name;
            }

            public int GetHashCode(ColorLookup obj) => obj.Color.Id.GetHashCode() ^ obj.Color.Name.GetHashCode();
        }
    }
}
