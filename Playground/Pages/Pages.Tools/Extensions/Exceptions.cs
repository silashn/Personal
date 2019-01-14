using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Tools.Extensions
{
    public static class Exceptions
    {
        public static void ThrowIfParameterIsNull(this object parameter, string parameterName)
        {
            if(parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}
