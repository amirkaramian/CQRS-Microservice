using Payscrow.PaymentInvite.Domain.SeedWork;
using System.Collections.Generic;

namespace Payscrow.PaymentInvite.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string CountryCode { get; private set; }
        public string LocalNumber { get; private set; }

        public PhoneNumber() { }

        public PhoneNumber(string countryCode, string localNumber)
        {
            CountryCode = countryCode;
            LocalNumber = localNumber;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CountryCode;
            yield return LocalNumber;
        }

        public override string ToString()
        {
            return CountryCode + LocalNumber;
        }
    }
}
