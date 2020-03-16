using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Common;
using ConstructionLine.CodingChallenge.Common.SampleData;
using ConstructionLine.CodingChallenge.Service;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private AutoMocker mockProvider;

        [SetUp]
        public void Setup()
        {
            this.mockProvider = new AutoMocker(Moq.MockBehavior.Strict);
        }


        [Test]
        public void PerformanceTest()
        {
            // Arrange
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var shirts = this.mockProvider.CreateInstance<SampleDataBuilder>().GetSampleData(50000);

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(shirts);

            this.mockProvider.Use<IShirtService>(this.mockProvider.CreateInstance<ShirtService>());
            this.mockProvider.Use<ISearchEngineService>(this.mockProvider.CreateInstance<SearchEngineService>());

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();
            

            // Act
            var results = searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"Test fixture finished in {sw.ElapsedMilliseconds} milliseconds");

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Once);

            AssertResults(results.Shirts, options);
            AssertSizeCounts(shirts, options, results.SizeCounts);
            AssertColorCounts(shirts, options, results.ColorCounts);
        }
    }
}
