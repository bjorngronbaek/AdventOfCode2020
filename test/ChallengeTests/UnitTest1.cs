using AdventOfCode2020.challenges;
using NUnit.Framework;

namespace ChallengeTests
{
    public class Challenge5Tests
    {
        private Challenge5 sut;
        
        [SetUp]
        public void Setup()
        {
            sut = new Challenge5();
        }

        [TestCase("FBFBBFFRLR", 357)]
        [TestCase("BFFFBBFRRR", 567)]
        [TestCase("FFFBBBFRRR", 119)]
        [TestCase("BBFFBBFRLL", 820)]
        public void ParseSeatIdTest(string code,long seatId)
        {
            Assert.AreEqual(seatId,sut.ParseSeatId(code));
        }
    }
}