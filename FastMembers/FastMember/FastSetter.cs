using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROK.Reflection.FastMembers
{
    public class FastSetter : Member, IFastSetter
    {
        private readonly Func<object, object, object> _setter;

        public FastSetter(Func<object, object, object> setter)
        {
            _setter = setter;
        }

        public object SetValue(object instance, object value)
        {
            return _setter(instance, value);
        }
    }
}
