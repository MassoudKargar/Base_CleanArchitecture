using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infra.Validators.Templates
{
    public static class ErrorMessages
    {

        public static string RequiredProperty(string prop) => $"{prop} ضروریست";
        public static string RequiredPropertyLength(string prop, int length) => $"{prop} باید {length} کاراکتر باشد";
        public static string RequiredPropertyMaxLength(string prop, int length) => $"{prop} نباید بیش از {length} کاراکتر باشد";
        public static string RequiredPropertyMinLength(string prop, int length) => $"{prop} باید حداقل {length} کاراکتر باشد";
        public static string RequiredPropertyInRange(string prop, int min, int max) => $"{prop} باید بین {min}-{max} کاراکتر باشد";

    }
}
