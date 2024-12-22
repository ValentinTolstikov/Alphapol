using System.Globalization;
using System.Windows.Controls;

namespace Praktika.Validators
{
    public class NumbersOnlyValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            long res;
            bool parseResult = long.TryParse((string)value,out res);
            return new ValidationResult(parseResult, "Строка не является синей");
        }
    }
}
