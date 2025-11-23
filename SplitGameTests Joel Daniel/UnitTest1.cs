using SplitGameTests.GameLogic;
using Xunit;

namespace SplitGameTests
{
    public class SplitGameUnitTests
    {
        private readonly SplitGameLogic logic = new SplitGameLogic();

        // 1. Exact correct input should pass
        [Fact]
        public void CorrectSplit_ShouldReturnTrue()
        {
            bool result = logic.CheckSplit(
                "engraved,secret,slumber",
                "engraved,secret,slumber"
            );
            Assert.True(result);
        }

        // 2. Input with spaces should still pass (Normalize)
        [Fact]
        public void SplitWithSpaces_ShouldStillPass()
        {
            bool result = logic.CheckSplit(
                "engraved, secret, slumber",
                "engraved,secret,slumber"
            );
            Assert.True(result);
        }

        // 3. Uppercase input should also pass
        [Fact]
        public void UppercaseInput_ShouldNormalizeAndPass()
        {
            bool result = logic.CheckSplit(
                "ENGRAVED,SECRET,SLUMBER",
                "engraved,secret,slumber"
            );
            Assert.True(result);
        }

        // 4. Incorrect order should fail
        [Fact]
        public void WrongOrder_ShouldFail()
        {
            bool result = logic.CheckSplit(
                "secret,engraved,slumber",
                "engraved,secret,slumber"
            );
            Assert.False(result);
        }

        // 5. Extra characters should fail
        [Fact]
        public void ExtraLetters_ShouldFail()
        {
            bool result = logic.CheckSplit(
                "engraved,secret,slumberX",
                "engraved,secret,slumber"
            );
            Assert.False(result);
        }

        // 6. Missing a word should fail
        [Fact]
        public void MissingWord_ShouldFail()
        {
            bool result = logic.CheckSplit(
                "engraved,slumber",
                "engraved,secret,slumber"
            );
            Assert.False(result);
        }
    }
}
