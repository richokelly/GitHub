namespace ROK.Reflection.FastMembers
{
    public interface IFastGetter : IMember
    {
        object GetValue(object instance);
    }
}
