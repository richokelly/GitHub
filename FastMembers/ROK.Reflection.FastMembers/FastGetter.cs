namespace ROK.Reflection.FastMembers
{
    public class FastGetter : Member, IFastGetter
    {
        private readonly Func<object, object> _getter;

        public FastGetter(Func<object, object> getter)
        {
            _getter = getter;
        }

        public object GetValue(object instance)
        {
            return _getter(instance);   
        }
    }
}
