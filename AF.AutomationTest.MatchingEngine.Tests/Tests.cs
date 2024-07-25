using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AF.AutomationTest.MatchingEngine.Tests
{
    [TestClass]
    public class Tests
    {
        private static MatchingApi _matchingApi;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _matchingApi = new MatchingApi();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _matchingApi.ClearData();
        }

        [TestMethod]
        public void FindMatchAllCriteriaMatTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("test", 100, 19.99, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("test", 100, 19.99, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "Expected records to match based on criteria.");
        }

        [TestMethod]
        public void FindMatchWithCaseSensitiveSymbolsTest1()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("test", 100, 19.99, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 100, 19.99, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        [TestMethod]
        public void FindMatchWithCaseSensitiveSymbolsTest2()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 19.99, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("test", 100, 19.99, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        [TestMethod]
        public void FindMatchWithCaseSensitiveSymbolsTest3()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 100, 19.99, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 100, 19.99, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        [TestMethod]
        public void FindNoMatchWithDifferentSymbolsTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test1", 100, 100.55, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test2", 100, 100.55, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "...");
        }

        [TestMethod]
        public void FindNoMatchWithDifferentQuantityTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 103, 19.99, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 150, 19.99, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "Expected records not to match due to quantity difference.");
        }

        [TestMethod]
        public void FindMatchWithQuantityWithinAndEqualsToleranceTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 50, 19.00, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 55, 19.00, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        [TestMethod]
        public void FindMatchWithQuantityWithinToleranceAboveTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 33, 19.00, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 30, 19.00, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        [TestMethod]
        public void FindMatchWithQuantityWithinToleranceBelowTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 30, 19.00, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 33, 19.00, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        [TestMethod]
        public void FindNoMatchWithQuantityOutsideToleranceTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 50, 19.00, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 70, 19.00, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "...");
        }

        [TestMethod]
        public void FindMatchWithSamePricesTest1()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 70, 150, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 70, 150, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        public void FindMatchWithSamePricesTest2()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 70, 150.99, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 70, 150.99, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "...");
        }

        [TestMethod]
        public void FindNoMatchWithDifferentPricesTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 70, 19.99, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 70, 15.49, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "...");
        }

        [TestMethod]
        public void FindNoMatchWithDifferentDatesTest()
        {
            var date1 = DateTime.UtcNow;
            var date2 = DateTime.UtcNow.AddDays(-1);

            var record1 = _matchingApi.CreateRecord("Test", 70, 10.00, date1, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 70, 10.00, date2, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "...");
        }

        [TestMethod]
        public void FindMatchWithDifferentSideTypeTest()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 70, 19.00, date, Side.Sell);
            var record2 = _matchingApi.CreateRecord("Test", 70, 19.00, date, Side.Buy);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsTrue(isMatched, "...");
        }

        [TestMethod]
        public void FindNoMatchWithTheSameSideTypeTest1()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 30, 19.00, date, Side.Buy);
            var record2 = _matchingApi.CreateRecord("Test", 30, 19.00, date, Side.Buy);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "...");
        }

        [TestMethod]
        public void FindNoMatchWithTheSameSideTypeTest2()
        {
            var date = DateTime.UtcNow;

            var record1 = _matchingApi.CreateRecord("Test", 30, 19.00, date, Side.Sell);
            var record2 = _matchingApi.CreateRecord("Test", 30, 19.00, date, Side.Sell);

            var isMatched = _matchingApi.CheckIfRecordsMatched(record1, record2);

            Assert.IsFalse(isMatched, "...");
        }

    }
}