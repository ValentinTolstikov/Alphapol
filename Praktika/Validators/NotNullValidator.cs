using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Praktika.Validators
{
    public class NotNullValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || ((string)value == string.Empty))
            {
                return new ValidationResult(false, "Field is null");
            }
            return new ValidationResult(true, string.Empty);
        }
    }
}
