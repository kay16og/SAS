using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Lookups
{
    public class SizeAndColorLookup
    {
        public SizeAndColorLookup(Size size, Color color)
        {
            Size = size;
            Color = color;
        }

        public Size Size { get; }

        public Color Color { get; }

        public static IEqualityComparer<SizeAndColorLookup> SizeAndColorLookupComparer { get; } = new SizeAndColorLookupEqualityComparer();

        private sealed class SizeAndColorLookupEqualityComparer : IEqualityComparer<SizeAndColorLookup>
        {
            public bool Equals(SizeAndColorLookup x, SizeAndColorLookup y)
            {
                if (x == null && y == null)
                {
                    return true;
                }
                else if (x == null || y == null)
                {
                    return false;
                }

                return x.Size.Id == y.Size.Id &&
                    x.Size.Name == y.Size.Name &&
                        x.Color.Id == y.Color.Id &&
                            x.Color.Name == y.Color.Name;
            }

            public int GetHashCode(SizeAndColorLookup obj)
            {
                return obj.Size.Id.GetHashCode() ^ obj.Size.Name.GetHashCode() ^ obj.Color.Id.GetHashCode() ^ obj.Color.Name.GetHashCode();
            }
        }
    }
}
