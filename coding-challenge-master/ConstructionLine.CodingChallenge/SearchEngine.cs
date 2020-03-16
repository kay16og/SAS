using ConstructionLine.CodingChallenge.Extensions;
using ConstructionLine.CodingChallenge.Lookups;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly ILookup<SizeLookup, Shirt> _shirtSizeLookup;
        private readonly ILookup<ColorLookup, Shirt> _shirtColorLookup;
        private readonly ILookup<SizeAndColorLookup, Shirt> _shirtSizeColorLookup;

        public SearchEngine(List<Shirt> shirts)
        {
            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.
            _shirtSizeLookup = shirts.ToLookup(s => new SizeLookup(s.Size), SizeLookup.SizeLookupComparer);
            _shirtColorLookup = shirts.ToLookup(s => new ColorLookup(s.Color), ColorLookup.ColorLookupComparer);
            _shirtSizeColorLookup = shirts.ToLookup(s => new SizeAndColorLookup(s.Size, s.Color), SizeAndColorLookup.SizeAndColorLookupComparer);
        }

        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            // TODO: Caching: In the real-world, some caching impl will peek the cache
            // to determine if the given search option hasn't already had a cached dataset
            // before querying the 'datastore'. 

            var shirts = default(SearchResults);

            if (options != null)
            {
                if (options.Sizes.Any() && options.Colors.Any())
                {
                    shirts = _shirtSizeColorLookup.PerformSearch(options);
                }
                else if (options.Sizes.Any())
                {
                    shirts = _shirtSizeLookup.PerformSearch(options);
                }
                else if (options.Colors.Any())
                {
                    shirts = _shirtColorLookup.PerformSearch(options);
                }
            }
            
            return shirts;
        }
    }
}