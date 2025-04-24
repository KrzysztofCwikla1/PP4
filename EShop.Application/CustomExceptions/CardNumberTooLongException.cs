using System;

namespace EShop.Application.CustomExceptions
{
    public class CardNumberTooLongException : Exception
    {
        public CardNumberTooLongException(string number)
            : base($"Numer karty jest zbyt długi: {number.Length} cyfr.") { }
    }
}
