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
    }
}
