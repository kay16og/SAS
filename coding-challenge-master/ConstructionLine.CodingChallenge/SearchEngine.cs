using ConstructionLine.CodingChallenge.Common;
using ConstructionLine.CodingChallenge.Service;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly ISearchEngineService searchEngineService;

        public SearchEngine(ISearchEngineService searchEngineService)
        {
            this.searchEngineService = searchEngineService;
        }

        public SearchResults Search(SearchOptions options)
        {
            return this.searchEngineService.Search(options);
        }
    }
}