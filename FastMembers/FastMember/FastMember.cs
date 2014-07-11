using System;

namespace ROK.Reflection.FastMembers
{
    public class FastMember : Member, IFastMember
    {
        private readonly Func<object, object> _getter;
        private readonly Func<object, object, object> _setter;

        public FastMember(Func<object, object> getter, Func<object, object, object> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public object GetValue(object instance)
        {
            return _getter(instance);
        }

        public object SetValue(object instance, object value)
        {
            return _setter(instance, value);
        }
    }
}
