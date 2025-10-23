namespace Roo.Cli.Tests
{
    public class PromptHandlerTests
    {
        private readonly IRooLogger _logger = Substitute.For<IRooLogger>();
        private readonly IRooUserInput _userInput = Substitute.For<IRooUserInput>();
        
        private PromptHandler _sut;

        public PromptHandlerTests()
        {
            _sut = new PromptHandler(_logger, _userInput);
        }

        [Fact]
        public void PromptYesNo_ShouldReturnYes_WhenUserEntersYes()
        {
            // Arrange
            _userInput.ReadInput().Returns("yes");

            // Act
            var result = _sut.PromptYesNo("Confirm?");

            // Assert
            Assert.Equal(PromptAnswer.Yes, result);
        }

        [Theory]
        [InlineData("y")]
        [InlineData("Y")]
        [InlineData("Ye")]
        [InlineData("YES")]
        public void PromptYesNo_ShouldReturnYes_ForAllYesVariants(string input)
        {
            _userInput.ReadInput().Returns(input);

            var result = _sut.PromptYesNo("Confirm?");

            Assert.Equal(PromptAnswer.Yes, result);
        }

        [Theory]
        [InlineData("n")]
        [InlineData("N")]
        [InlineData("no")]
        [InlineData("NO")]
        public void PromptYesNo_ShouldReturnNo_ForAllNoVariants(string input)
        {
            _userInput.ReadInput().Returns(input);

            var result = _sut.PromptYesNo("Confirm?");

            Assert.Equal(PromptAnswer.No, result);
        }

        [Fact]
        public void PromptYesNo_ShouldDefaultToNo_WhenInputIsEmpty()
        {
            _userInput.ReadInput().Returns("");

            var result = _sut.PromptYesNo("Confirm?");

            Assert.Equal(PromptAnswer.No, result);
        }

        [Fact]
        public void PromptYesNo_ShouldRetry_WhenInputIsInvalid()
        {
            // Arrange: simulate invalid, then valid input
            _userInput.ReadInput().Returns("maybe", "y");

            // Act
            var result = _sut.PromptYesNo("Confirm?");

            // Assert
            Assert.Equal(PromptAnswer.Yes, result);

            // Verify feedback message was shown
            _logger.Received().Log(Arg.Is<string>(msg => msg.Contains("Please enter")));
        }
    }
}
