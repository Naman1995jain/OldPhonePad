using System;
using Xunit;

namespace OldPhonePadChallenge.Tests
{
    /// <summary>
    /// Unit tests for the OldPhonePad converter
    /// </summary>
    public class OldPhonePadTests
    {
        #region Basic Functionality Tests

        [Fact]
        public void ConvertInput_SingleLetter_ReturnsCorrectCharacter()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("33#");

            // Assert
            Assert.Equal("E", result);
        }

        [Fact]
        public void ConvertInput_WithBackspace_RemovesLastCharacter()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("227*#");

            // Assert
            Assert.Equal("B", result);
        }

        [Fact]
        public void ConvertInput_ComplexWord_ReturnsHELLO()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("4433555 555666#");

            // Assert
            Assert.Equal("HELLO", result);
        }

        [Fact]
        public void ConvertInput_WithSpacesAndBackspace_ReturnsTURING()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("8 88777444666*664#");

            // Assert
            Assert.Equal("TURING", result);
        }

        #endregion

        #region Edge Cases

        [Fact]
        public void ConvertInput_OnlySendButton_ReturnsEmptyString()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("#");

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ConvertInput_MultipleBackspaces_HandlesCorrectly()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("222***#");

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ConvertInput_BackspaceOnEmpty_NoError()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("*#");

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ConvertInput_CyclingThroughAllLetters_ReturnsLastLetter()
        {
            // Pressing 2 four times cycles back: A->B->C->A
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("2222#");

            // Assert
            Assert.Equal("A", result);
        }

        #endregion

        #region Keypad Mapping Tests

        [Theory]
        [InlineData("0#", " ")]           // Space
        [InlineData("2#", "A")]           // 2 once = A
        [InlineData("22#", "B")]          // 2 twice = B
        [InlineData("222#", "C")]         // 2 thrice = C
        [InlineData("3#", "D")]           // 3 once = D
        [InlineData("33#", "E")]          // 3 twice = E
        [InlineData("333#", "F")]         // 3 thrice = F
        [InlineData("7777#", "S")]        // 7 four times = S
        [InlineData("9999#", "Z")]        // 9 four times = Z
        public void ConvertInput_VariousKeys_ReturnsCorrectLetters(string input, string expected)
        {
            // Act
            var result = OldPhonePad.ConvertInput(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertInput_Key1_ReturnsSpecialCharacters()
        {
            // Arrange & Act
            var ampersand = OldPhonePad.ConvertInput("1#");
            var apostrophe = OldPhonePad.ConvertInput("11#");
            var leftParen = OldPhonePad.ConvertInput("111#");

            // Assert
            Assert.Equal("&", ampersand);
            Assert.Equal("'", apostrophe);
            Assert.Equal("(", leftParen);
        }

        #endregion

        #region Space Handling Tests

        [Fact]
        public void ConvertInput_SpaceBetweenSameKey_AllowsTwoLettersFromSameKey()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("222 2 22#");

            // Assert
            Assert.Equal("CAB", result);
        }

        [Fact]
        public void ConvertInput_MultipleSpaces_HandledCorrectly()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("2  2#");

            // Assert
            Assert.Equal("AA", result);
        }

        [Fact]
        public void ConvertInput_ActualSpaceCharacter_AddsSpace()
        {
            // Arrange & Act
            var result = OldPhonePad.ConvertInput("44 444 0 9966 6#");

            // Assert
            Assert.Equal("HI YOU", result);
        }

        #endregion

        #region Validation Tests

        [Fact]
        public void ConvertInput_NullInput_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => OldPhonePad.ConvertInput(null!));
        }

        [Fact]
        public void ConvertInput_EmptyInput_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => OldPhonePad.ConvertInput(""));
        }

        [Fact]
        public void ConvertInput_NoSendButton_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => OldPhonePad.ConvertInput("222"));
        }

        #endregion

        #region Complex Scenarios

        [Fact]
        public void ConvertInput_LongMessage_HandlesCorrectly()
        {
            // Arrange - Spelling "CODING"
            var input = "222666 6334 4446#";

            // Act
            var result = OldPhonePad.ConvertInput(input);

            // Assert
            Assert.Equal("CODING", result);
        }

        [Fact]
        public void ConvertInput_BackspaceInMiddle_WorksCorrectly()
        {
            // Arrange - "ABC" but backspace the B, resulting in "AC"
            var input = "222*22#";

            // Act
            var result = OldPhonePad.ConvertInput(input);

            // Assert
            Assert.Equal("AB", result);
        }

        [Fact]
        public void ConvertInput_MixedOperations_HandlesComplex()
        {
            // Arrange - Complex mix of letters, spaces, and backspaces
            var input = "222 2*7777 777#";

            // Act
            var result = OldPhonePad.ConvertInput(input);

            // Assert
            Assert.Equal("CSP", result);
        }

        #endregion
    }
}