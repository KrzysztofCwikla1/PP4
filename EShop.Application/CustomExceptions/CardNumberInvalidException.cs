using System;

namespace EShop.Application.CustomExceptions
{
    public class CardNumberInvalidException : Exception
    {
        public CardNumberInvalidException(string number)
            : base($"Numer karty jest nieprawidłowy – niezgodny z algorytmem Luhna.") { }
    }
}
