using NUnit.Framework;

namespace ROK.Reflection.UnitTests
{
    [TestFixture]
    public class TypeExtensionTests
    {
        [Test]
        public void GetFastGettersWithTypeWithNoPublicMembersExpectEmptyCollectionReturned()
        {
            var getters = typeof(MemberlessTestType).GetFastGetters();
            Assert.IsEmpty(getters);
        }

        [Test]
        public void GetFastGettersWithTypeWithPublicMembersExpectGettersCreated()
        {
            var getters = typeof(MemberlessTestType).GetFastGetters();
            Assert.IsEmpty(getters);
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
    }
}
