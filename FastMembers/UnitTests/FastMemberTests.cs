using System;
using System.Linq;
using NUnit.Framework;
using ROK.Reflection.FastMembers;

namespace ROK.Reflection.UnitTests
{
    [TestFixture]
    public class FastMemberTests
    {
        [Test]
        public void GetReferenceFieldValueExpectSameReferenceReturned()
        {
            var instance = new MemberedTestType { ReferenceField = Guid.NewGuid().ToString() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ReferenceField");
            Assert.AreSame(instance.ReferenceField, member.GetValue(instance));
        }

        [Test]
        public void GetValueFieldValueExpectSameValueReturned()
        {
            var instance = new MemberedTestType { ValueField = Guid.NewGuid().GetHashCode() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ValueField");
            Assert.AreEqual(instance.ValueField, member.GetValue(instance));
        }

        [Test]
        public void GetReferencePropertyValueExpectSameReferenceReturned()
        {
            var instance = new MemberedTestType { ReferenceProperty = Guid.NewGuid().ToString() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ReferenceProperty");
            Assert.AreSame(instance.ReferenceProperty, member.GetValue(instance));
        }

        [Test]
        public void GetValuePropertyValueExpectSameValueReturned()
        {
            var instance = new MemberedTestType { ValueProperty = Guid.NewGuid().GetHashCode() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ValueProperty");
            Assert.AreEqual(instance.ValueProperty, member.GetValue(instance));
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