using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба2
{
    internal class DiscriminantZero : Exception
    {
        public override string Message
        {
            get
            {
                return "! Дискриминант D < 0 !";
            }
        }
    }

    internal class IncorrectData : Exception
    {
        public override string Message
        {
            get
            {
                return "! Некорректные данные !";
            }
        }
    }

    internal class CoefficientAZero : Exception
    {
        public override string Message
        {
            get
            {
                return "! Коэффициент a = 0 !";
            }
        }
    }
}
