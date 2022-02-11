using Sample.Domain.Shared;
using Sample.SharedKernel.SeedWork;
using System;
using System.Threading.Tasks;

namespace Sample.Domain.Meeting.Rules
{
    public class DateIsFixedHourRule : IBusinessRule
    {
        private readonly DateTimeOffset _date;
        private readonly string _propertyName;
        public DateIsFixedHourRule(DateTimeOffset date, string propertyName)
        {
            _date = date;
            _propertyName = propertyName;
        }

        public string Message => $"Date with given value '{_date}' is not valid";

        public string[] Properties => new[] { _propertyName };

        public string ErrorType => BusinessRuleType.PropertyValue.ToString("G");

        public async Task<bool> IsBroken()
        {
            var result = false;
            if (_date.Minute > 0 || _date.Second > 0 || _date.Millisecond > 0)
                result = true;

            return result;
        }
    }
}
