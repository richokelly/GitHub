namespace ROK.Reflection.FastMembers
{
    public abstract class Member : IMember
    {
        public IReadOnlyCollection<Attribute> Attributes { get; set; }
        public Type DeclaringType { get; set; }
        public string Name { get; set; }
        public Type MemberType { get; set; }
    }
}
