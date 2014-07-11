using System.Linq;
using NUnit.Framework;
using ROK.Reflection.FastMembers;

namespace ROK.Reflection.UnitTests
{
    [TestFixture]
    public class TypeExtensionTests
    {
        [Test]
        public void GetFastGettersWithTypeExpectOnlyPublicGettersCreated()
        {
            foreach (var type in new[] { typeof(ImbalancedMemberedTestType), typeof(MemberedTestType), typeof(MemberlessTestType) })
            {
                var getters = type.GetFastGetters();
                Assert.AreEqual(type.GetProperties().Count(p => p.CanRead) + type.GetFields().Length, getters.Count);
            }
        }

        [Test]
        public void GetFastSettersWithTypeWithPublicMembersExpectSettersCreated()
        {
            foreach (var type in new[] { typeof(ImbalancedMemberedTestType), typeof(MemberedTestType), typeof(MemberlessTestType) })
            {
                var setters = type.GetFastSetters();
                Assert.AreEqual(type.GetProperties().Count(p => p.CanWrite) + type.GetFields().Length, setters.Count);
            }
        }

        [Test]
        public void GetFastMembersWithTypeWithPublicMembersExpectOnlyPublicMembersCreated()
        {
            foreach (var type in new[] { typeof(ImbalancedMemberedTestType), typeof(MemberedTestType), typeof(MemberlessTestType) })
            {
                var members = type.GetFastMembers();
                Assert.AreEqual(type.GetProperties().Count(p => p.CanRead && p.CanWrite) + type.GetFields().Length, members.Count);
            }
        }

        private class MemberlessTestType
        {
        }

        private class MemberedTestType
        {
            public string ReferenceField;
            public int ValueField;
            public string ReferenceProperty { get; set; }
            public int ValueProperty { get; set; }
        }

        private class ImbalancedMemberedTestType
        {
            private string PrivateReferenceField;
            private int PrivateValueField;
            public string PublicReferenceField;
            public int PublicValueField;
            public string GetOnlyReferenceProperty { get; private set; }
            public string SetOnlyReferenceProperty { private get; set; }
            public int GetOnlyValueProperty { get; private set; }
            public int SetOnlyValueProperty { private get; set; }
        }
    }
}
