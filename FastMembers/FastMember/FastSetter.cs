using System;

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
