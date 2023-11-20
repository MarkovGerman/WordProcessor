using WordProcessorApp.Parsers;
using WordProcessorApp.Repositories;

namespace WordProcessorApp.Tests
{
    public class ParserTest
    {
        private Parser parser;

        [SetUp]
        public void Setup()
        {
            parser = new Parser();
        }

        [TestCase("", 0)]
        [TestCase("word", 1)]
        [TestCase("парсер правильно", 2)]
        [TestCase("парсер правильно возвращает", 3)]
        [TestCase("парсер правильно возвращает список", 4)]

        public void TestNumberWords(string text, int countWord)
        {
            var words = parser.Parse(text);
            Assert.That(words.Count, Is.EqualTo(countWord), "number of words in the text does not correspond to the number of words of the parser");
        }

        [TestCase("word,", new string[] { "word" })]
        [TestCase("word. are", new string[] { "word", "are" })]
        [TestCase("word:", new string[] { "word" })]
        [TestCase("word;", new string[] { "word" })]
        [TestCase($"\"word\"", new string[] { "word" })]
        public void PunctuationMarksNotAffect(string text, string[] words)
        {
            CompareWordArrayText(text, words);
        }

        [TestCase("things. You", new string[] {"things", "You"})]
        [TestCase("CWOT", new string[] { "CWOT"})]
        [TestCase("It was YES", new string[] { "It", "was", "YES" })]
        
        public void TestInputDifferentRegisters(string text, string[] words)
        {
            CompareWordArrayText(text, words);
        }

        private void CompareWordArrayText(string text, string[] words)
        {
            var wordsParser = parser.Parse(text);
            if (wordsParser.Count != words.Length)
                Assert.AreEqual(words.Length, wordsParser.Count, "The number of words is different");

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != wordsParser[i])
                    Assert.AreEqual(wordsParser[i], words[i]);
            }
            Assert.IsTrue(true);
        }
    }
}