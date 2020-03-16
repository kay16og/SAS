using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Lookups
{
    public class SizeLookup
    {
        public SizeLookup(Size size)
        {
            Size = size;
        }

        public Size Size { get; }

        public static IEqualityComparer<SizeLookup> SizeLookupComparer { get; } = new SizeLookupEqualityComparer();

        private sealed class SizeLookupEqualityComparer : IEqualityComparer<SizeLookup>
        {
            public bool Equals(SizeLookup x, SizeLookup y)
            {
                if (x == null && y == null)
                {
                    return true;
                }
                else if (x == null || y == null)
                {
                    return false;
                }

                return x.Size.Id == y.Size.Id && x.Size.Name == y.Size.Name;
            }

            public int GetHashCode(SizeLookup obj) => obj.Size.Id.GetHashCode() ^ obj.Size.Name.GetHashCode();
        }
    }
}
