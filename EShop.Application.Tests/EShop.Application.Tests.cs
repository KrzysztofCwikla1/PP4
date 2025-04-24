using Xunit;
using EShop.Application;
using EShop.Application.Enum;
using EShop.Application.CustomExceptions;

namespace EShop.Application.Tests
{
    public class CardValidatorTests
    {
        private readonly CardValidator _validator = new();

        [Theory]
        [InlineData("4532 2080 2150 4434", CreditCardProvider.Visa)]
        [InlineData("5530016454538418", CreditCardProvider.MasterCard)]
        [InlineData("378523393817437", CreditCardProvider.AmericanExpress)]
        public void Validate_ValidCardNumbers_ReturnsTrueAndProvider(string cardNumber, CreditCardProvider expectedProvider)
        {
            var result = _validator.Validate(cardNumber);
            Assert.True(result.IsValid);
            Assert.Equal(expectedProvider, result.Provider);
        }

        [Theory]
        [InlineData("4532 2080 2150 4434 1234 5678")] // 24 digits
        [InlineData("1234567890123456789012345")]    // 25 digits
        public void Validate_CardTooLong_ThrowsCardNumberTooLongException(string cardNumber)
        {
            Assert.Throws<CardNumberTooLongException>(() => _validator.Validate(cardNumber));
        }

        [Theory]
        [InlineData("123456789")] // 9 digits
        [InlineData("1234")]      // 4 digits
        public void Validate_CardTooShort_ThrowsCardNumberTooShortException(string cardNumber)
        {
            Assert.Throws<CardNumberTooShortException>(() => _validator.Validate(cardNumber));
        }

        [Theory]
        [InlineData("4532-2080-2150-44AB")]
        [InlineData("4532 2080 2150 FOUR")]
        public void Validate_InvalidCharacters_ThrowsCardNumberInvalidException(string cardNumber)
        {
            Assert.Throws<CardNumberInvalidException>(() => _validator.Validate(cardNumber));
        }

        [Fact]
        public void Validate_FailsLuhnCheck_ThrowsCardNumberInvalidException()
        {
            string invalidLuhn = "4532208021504435"; // altered last digit to fail Luhn
            Assert.Throws<CardNumberInvalidException>(() => _validator.Validate(invalidLuhn));
        }

        [Fact]
        public void Validate_UnsupportedProvider_ThrowsCardNumberInvalidException()
        {
            string unsupported = "6011111111111117"; // Discover card
            Assert.Throws<CardNumberInvalidException>(() => _validator.Validate(unsupported));
        }

        [Theory]
        [InlineData("4532 2080 2150 4434", CreditCardProvider.Visa)]
        [InlineData("5530016454538418", CreditCardProvider.MasterCard)]
        [InlineData("378523393817437", CreditCardProvider.AmericanExpress)]
        [InlineData("6011111111111117", null)] // Unsupported
        public void GetCardType_ReturnsCorrectProviderOrNull(string cardNumber, CreditCardProvider? expected)
        {
            var result = _validator.GetCardType(cardNumber);
            Assert.Equal(expected, result);
        }
    }
}
