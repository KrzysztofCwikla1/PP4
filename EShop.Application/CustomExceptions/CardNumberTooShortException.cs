using System;

namespace EShop.Application.CustomExceptions
{
    public class CardNumberTooShortException : Exception
    {
        public CardNumberTooShortException(string number)
            : base($"Numer karty jest zbyt krótki: {number.Length} cyfr.") { }
    }
}
