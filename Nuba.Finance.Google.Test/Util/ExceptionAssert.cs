using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nuba.Finance.Google.Test.Util
{
    public static class ExceptionAssert
    {
        public static T Throws<T>(Action func) where T : Exception
        {
            try
            {
                func.Invoke();
                throw new AssertFailedException(
                    String.Format("An exception of type {0} was expected, but not thrown", typeof(T)));
            }
            catch (T e)
            {
                return e;
            }
        }
    }
}
