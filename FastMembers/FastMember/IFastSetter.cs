using System;

namespace ROK.Reflection.FastMembers
{
    public interface IFastSetter : IMember
    {
        object SetValue(object instance, object value);
    }
}
