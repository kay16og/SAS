using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge.Common.SampleData
{
    public interface ISampleDataBuilder
    {
        List<Shirt> GetSampleData(int numberOfShirts = 50000);
    }

    public class SampleDataBuilder : ISampleDataBuilder
    {
        private readonly Random _random = new Random();


        public List<Shirt> GetSampleData(int numberOfShirts)
        {
            return Enumerable.Range(0, numberOfShirts)
                .Select(i => new Shirt(Guid.NewGuid(), $"Shirt {i}", GetRandomSize(), GetRandomColor()))
                .ToList();
        }


        private Size GetRandomSize()
        {

            var sizes = Size.All;
            var index = _random.Next(0, sizes.Count);
            return sizes.ElementAt(index);
        }


        private Color GetRandomColor()
        {
            var colors = Color.All;
            var index = _random.Next(0, colors.Count);
            return colors.ElementAt(index);
        }
    }
}
