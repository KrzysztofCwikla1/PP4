using System.Text.RegularExpressions;
using EShop.Application.Enum;
using EShop.Application.CustomExceptions;
namespace EShop.Application
{
    public class CardValidator
    {

        public (bool IsValid, CreditCardProvider? Provider) Validate(string cardNumber)
        {
             cardNumber = cardNumber.Replace(" ", "").Replace("-", "");
            if (cardNumber.Length > 19) 
                throw new CardNumberTooLongException(cardNumber);
            if (cardNumber.Length <13)
                throw new CardNumberTooShortException(cardNumber);
            if (!cardNumber.All(char.IsDigit))
                throw new CardNumberInvalidException(cardNumber);

            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                alternate = !alternate;
            }
                if (sum % 10 != 0)
                throw new CardNumberInvalidException(cardNumber);
            var type = GetCardType(cardNumber);
            if ((!type.HasValue))
                throw new CardNumberInvalidException(cardNumber);
            return(true, type);
        }
        public CreditCardProvider? GetCardType(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15}|\d{18})$"))
                return CreditCardProvider.Visa;
            else if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$"))
                return CreditCardProvider.MasterCard;

            if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                return CreditCardProvider.AmericanExpress;

            //if (Regex.IsMatch(cardNumber, @"^(6011\d{12}|65\d{14}|64[4-9]\d{13}|622(1[2-9][6-9]|[2-8]\d{2}|9([01]\d|2[0-5]))\d{10})$"))
            //    return "Discover";

   //            if (Regex.IsMatch(cardNumber, @"^(352[89]|35[3-8]\d)\d{12}$"))
     //           return "JCB";

       //     if (Regex.IsMatch(cardNumber, @"^3(0[0-5]|[68]\d)\d{11}$"))
         //       return "DinersClub";

//            if (Regex.IsMatch(cardNumber, @"^(50|5[6-9]|6\d)\d{10,17}$"))
 //               return "Maestro";

                return null;
        }
    }
}
