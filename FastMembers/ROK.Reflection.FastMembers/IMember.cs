namespace ROK.Reflection.FastMembers
{
    public interface IMember
    {
        IReadOnlyCollection<Attribute> Attributes { get; }
        Type DeclaringType { get; }
        string Name { get; }
        Type MemberType { get; }
    }
}
