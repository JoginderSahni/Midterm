using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midterm.Classes
{
    public enum PaymentType {Card, Cash, Check}
    public static class PaymentParse
    {
        public static PaymentType Parse(string s)
        {
            switch (s)
            {
                case "card":
                case "credit card":
                case "debit card":
                case "debit":
                case "credit":
                    return PaymentType.Card;
                case "check":
                case "ck":
                    return PaymentType.Check;
                default:
                    return PaymentType.Cash;
            }
        }
    }
}
