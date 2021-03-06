﻿using ConstructionLine.CodingChallenge.Common;
using ConstructionLine.CodingChallenge.Lookups;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge.Service
{
    public interface ISearchEngineService
    {
        SearchResults Search(SearchOptions searchOptions);
    }

    public sealed class SearchEngineService : ISearchEngineService
    {
        private readonly ILookup<SizeLookup, Shirt> _shirtSizeLookup;
        private readonly ILookup<ColorLookup, Shirt> _shirtColorLookup;
        private readonly ILookup<SizeAndColorLookup, Shirt> _shirtSizeColorLookup;

        public SearchEngineService(IShirtService shirtService)
        {
            var shirts = shirtService.GetShirts();

            _shirtSizeLookup = shirts.ToLookup(s => new SizeLookup(s.Size), SizeLookup.SizeLookupComparer);
            _shirtColorLookup = shirts.ToLookup(s => new ColorLookup(s.Color), ColorLookup.ColorLookupComparer);
            _shirtSizeColorLookup = shirts.ToLookup(s => new SizeAndColorLookup(s.Size, s.Color), SizeAndColorLookup.SizeAndColorLookupComparer);
        }

        public SearchResults Search(SearchOptions searchOptions)
        {
            var shirts = default(SearchResults);

            if (searchOptions != null)
            {
                if (searchOptions.Sizes.Any() && searchOptions.Colors.Any())
                {
                    shirts = PerformSearch(_shirtSizeColorLookup, searchOptions);
                }
                else if (searchOptions.Sizes.Any())
                {
                    shirts = PerformSearch(_shirtSizeLookup, searchOptions);
                }
                else if (searchOptions.Colors.Any())
                {
                    shirts = PerformSearch(_shirtColorLookup, searchOptions);
                }
            }

            return shirts;
        }

        public static SearchResults PerformSearch(ILookup<SizeLookup, Shirt> shirts, SearchOptions searchOptions)
        {
            var searchResult = LookupShirtsBySize(shirts, searchOptions.Sizes);

            return new SearchResults
            {
                Shirts = searchResult.ToList(),
                SizeCounts = GetSearchOptionsSizeCount(searchResult, searchOptions),
                ColorCounts = GetSearchOptionsColorCount(searchResult, searchOptions)
            };
        }

        public static SearchResults PerformSearch(ILookup<ColorLookup, Shirt> shirts, SearchOptions searchOptions)
        {
            var searchResult = LookupShirtsByColor(shirts, searchOptions.Colors);

            return new SearchResults
            {
                Shirts = searchResult.ToList(),
                SizeCounts = GetSearchOptionsSizeCount(searchResult, searchOptions),
                ColorCounts = GetSearchOptionsColorCount(searchResult, searchOptions)
            };
        }

        public static SearchResults PerformSearch(ILookup<SizeAndColorLookup, Shirt> shirts, SearchOptions searchOptions)
        {
            var searchResult = LookupShirtsBySizeAndColor(shirts, searchOptions);

            return new SearchResults
            {
                Shirts = searchResult.ToList(),
                SizeCounts = GetSearchOptionsSizeCount(searchResult, searchOptions),
                ColorCounts = GetSearchOptionsColorCount(searchResult, searchOptions)
            };
        }

        private static IEnumerable<Shirt> LookupShirtsBySize(ILookup<SizeLookup, Shirt> shirts, List<Size> size)
        {
            var sizeLookups = size.Select(shirt => new SizeLookup(shirt)).ToList();
            var shirtLookupResult = new List<Shirt>() as IEnumerable<Shirt>;

            foreach (var sizeLookup in sizeLookups)
            {
                shirtLookupResult = shirtLookupResult.Concat(shirts[sizeLookup]);
            }

            return shirtLookupResult;
        }

        private static IEnumerable<Shirt> LookupShirtsByColor(ILookup<ColorLookup, Shirt> shirts, List<Color> colors)
        {
            var colorLookups = colors.Select(s => new ColorLookup(s)).ToList();
            var shirtLookupResult = new List<Shirt>() as IEnumerable<Shirt>;

            foreach (var colorFilter in colorLookups)
            {
                shirtLookupResult = shirtLookupResult.Concat(shirts[colorFilter]);
            }

            return shirtLookupResult;
        }

        private static IEnumerable<Shirt> LookupShirtsBySizeAndColor(ILookup<SizeAndColorLookup, Shirt> shirts, SearchOptions searchOptions)
        {
            var sizeColorLookups = searchOptions.Sizes.SelectMany(
                            size => searchOptions.Colors.Select(
                                color => new SizeAndColorLookup(size, color)))
                        .ToList();

            IEnumerable<Shirt> shirtLookupResult = new List<Shirt>();

            foreach (var sizeColorLookup in sizeColorLookups)
            {
                shirtLookupResult = shirtLookupResult.Concat(shirts[sizeColorLookup]);
            }

            return shirtLookupResult;
        }

        private static List<SizeCount> GetSearchOptionsSizeCount(IEnumerable<Shirt> shirts, SearchOptions options)
        {
            var sizeCounts = options.Sizes.Select(
                size => new SizeCount
                {
                    Size = size,
                    Count = shirts.Count(r => r.Size.Id == size.Id)
                })
                .ToList();

            foreach (var size in Size.All)
            {
                if (!sizeCounts.Any(sc => sc.Size == size))
                {
                    sizeCounts.Add(new SizeCount { Size = size, Count = 0 });
                }
            }

            return sizeCounts;
        }

        private static List<ColorCount> GetSearchOptionsColorCount(IEnumerable<Shirt> shirts, SearchOptions options)
        {
            var colorCounts = options.Colors.Select(
                color => new ColorCount
                {
                    Color = color,
                    Count = shirts.Count(c => c.Color.Id == color.Id)
                })
                .ToList();

            foreach (var color in Color.All)
            {
                if (!colorCounts.Any(cc => cc.Color == color))
                {
                    colorCounts.Add(new ColorCount { Color = color, Count = 0 });
                }
            }

            return colorCounts;
        }
    }
}
