using ConstructionLine.CodingChallenge.Common;
using ConstructionLine.CodingChallenge.Common.SampleData;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Service
{
    public interface IShirtService
    {
        List<Shirt> GetShirts(int numberOfShirts = 50000);
    }

    public sealed class ShirtService : IShirtService
    {
        private readonly ISampleDataBuilder sampleDataProvider;

        public ShirtService(ISampleDataBuilder sampleDataProvider)
        {
            this.sampleDataProvider = sampleDataProvider;
        }

        public List<Shirt> GetShirts(int numberOfShirts)
        {
            return this.sampleDataProvider.GetSampleData(numberOfShirts);
        }
    }
}
