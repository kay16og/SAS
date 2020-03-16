using System;
using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Common;
using ConstructionLine.CodingChallenge.Common.SampleData;
using ConstructionLine.CodingChallenge.Service;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private AutoMocker mockProvider;

        [SetUp]
        public void Init()
        {
            this.mockProvider = new AutoMocker(MockBehavior.Strict);
        }

        [Test]
        public void GiveColorIsRed_WhenSizeIsSmall_ShouldReturnOneShirt_andCorrectCount()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(shirts)
                .Verifiable();

            this.mockProvider.Use<IShirtService>(this.mockProvider.CreateInstance<ShirtService>());
            this.mockProvider.Use<ISearchEngineService>(this.mockProvider.CreateInstance<SearchEngineService>());

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Once);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenColorIsRedBlue_WhenShirtSizeIsSmall_ShouldReturn2Shirts_andCorrectCount()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue },
                Sizes = new List<Size> { Size.Small }
            };

            var shirts = new List<Shirt>
            {
                // Red
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),

                // Black
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),

                // Blue
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
            };

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(shirts)
                .Verifiable();

            this.mockProvider.Use<IShirtService>(this.mockProvider.CreateInstance<ShirtService>());
            this.mockProvider.Use<ISearchEngineService>(this.mockProvider.CreateInstance<SearchEngineService>());

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Once);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenColorIsRedBlue_WhenShirtSizeIsSmallMedium_ShouldReturn3Shirts_andCorrectCount()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            var shirts = new List<Shirt>
            {
                // Red
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),

                // Black
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),

                // Blue
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
            };

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(shirts)
                .Verifiable();

            this.mockProvider.Use<IShirtService>(this.mockProvider.CreateInstance<ShirtService>());
            this.mockProvider.Use<ISearchEngineService>(this.mockProvider.CreateInstance<SearchEngineService>());

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Once);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenMultipleColors_WhenShirtSizeIsSmallAndMedium_ShouldReturn5Shirts_andCorrectCount()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            var shirts = new List<Shirt>
            {
                // Red
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),

                // Black
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),

                // Blue
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
            };

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(shirts)
                .Verifiable();

            this.mockProvider.Use<IShirtService>(this.mockProvider.CreateInstance<ShirtService>());
            this.mockProvider.Use<ISearchEngineService>(this.mockProvider.CreateInstance<SearchEngineService>());

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Once);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenSearchOptions_WhenSearchIsPerformed_ShouldInvokeSearchEngineService()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(new List<Shirt>())
                .Verifiable();

            this.mockProvider.GetMock<IShirtService>()
                .Setup(s => s.GetShirts(It.IsAny<int>()))
                .Returns(new List<Shirt>())
                .Verifiable();

            this.mockProvider.GetMock<ISearchEngineService>()
                .Setup(s => s.Search(searchOptions))
                .Returns(new SearchResults())
                .Verifiable();

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Never);
            this.mockProvider.GetMock<IShirtService>().Verify(s => s.GetShirts(50000), Times.Never);
            this.mockProvider.GetMock<ISearchEngineService>().Verify(s => s.Search(searchOptions), Times.Once);
        }

        [Test]
        public void GivenSearchOptions_WhenSearchIsPerformed_ShouldInvokeShirtService()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(new List<Shirt>())
                .Verifiable();

            this.mockProvider.GetMock<IShirtService>()
                .Setup(s => s.GetShirts(It.IsAny<int>()))
                .Returns(new List<Shirt>())
                .Verifiable();

            this.mockProvider.Use<ISearchEngineService>(this.mockProvider.CreateInstance<SearchEngineService>());

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Never);
            this.mockProvider.GetMock<IShirtService>().Verify(s => s.GetShirts(50000), Times.Once);
        }

        [Test]
        public void GivenSearchOptions_WhenSearchIsPerformed_ShouldInvokeSampleDataBuilder()
        {
            // Arrange
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue, Color.Black },
                Sizes = new List<Size> { Size.Small, Size.Medium, Size.Large }
            };

            var shirts = new List<Shirt>
            {
                // Red
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),

                // Black
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),

                // Blue
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Small", Size.Small, Color.Blue),
            };

            this.mockProvider.GetMock<ISampleDataBuilder>()
                .Setup(s => s.GetSampleData(It.IsAny<int>()))
                .Returns(shirts)
                .Verifiable();

            this.mockProvider.Use<IShirtService>(this.mockProvider.CreateInstance<ShirtService>());
            this.mockProvider.Use<ISearchEngineService>(this.mockProvider.CreateInstance<SearchEngineService>());

            var searchEngine = this.mockProvider.CreateInstance<SearchEngine>();

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            this.mockProvider.GetMock<ISampleDataBuilder>().Verify(s => s.GetSampleData(50000), Times.Once);
        }
    }
}
