using Moq;
using System.Xml;
using WordProcessorApp.Repositories;
using WordProcessorApp.Services;

namespace WordProcessorApp.Tests
{
    internal class RepositoryTest
    {
        [TestCase("a", 0)]
        [TestCase("an", 0)]
        [TestCase("any", 1)]
        [TestCase("apple", 1)]
        public void TestAddWord(string word, int wordCount)
        {
            var data = new List<string>();
            var repositoryStub = new Mock<IRepository>();
            repositoryStub
                .Setup(x => x.AddWord(word))
                .Callback((string word) => data.Add(word));
            var dictionaryService = new DictionaryService(repositoryStub.Object);
            var _ = dictionaryService.AddWord(word);
            Assert.AreEqual(data.Count, wordCount);
        }
    }
}
