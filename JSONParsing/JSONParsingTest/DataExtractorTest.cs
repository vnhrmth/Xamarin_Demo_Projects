using System;
using System.Collections.Generic;
using JSONParsing.DataAccessLayer;
using NUnit.Framework;

namespace JSONParsingTest
{
    public class DataExtractorTest
    {
        DataExtractor dataExtractor;

        [SetUp]
        public void Setup()
        {
            dataExtractor = new DataExtractor();
        }

        [Test]
        public void WhenDataExtractorIsCalledThenItShouldNotBeNull()
        {
            Assert.IsNotNull(dataExtractor);
        }

        [Test]
        public void WhenExtractMethodIsCalledWithEmptyUrlThenItShouldThrowException()
        {
            string url = "";
            Assert.Throws<Exception>(()=>dataExtractor.Extract(url));
        }

        [Test]
        public void WhenExtractIsCalledWithAUrlAndWebClientIsNullThenThrowException()
        {
            string url = "https://picsum.photos/v2/list";
            Assert.DoesNotThrow(() => dataExtractor.Extract(url));
        }

        [Test]
        public void WhenExtractIsCalledWitValidUrlThenTheDownloadedUrlsCountShouldBeGreaterThanZero()
        {
            string url = "https://picsum.photos/v2/list";
            List<string> downloadedUrls = dataExtractor.Extract(url);
            Assert.AreNotEqual(0, downloadedUrls.Count);
        }
    }
}