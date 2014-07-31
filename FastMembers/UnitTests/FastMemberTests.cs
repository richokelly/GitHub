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

        [Test]
        public void SetReferenceFieldValueExpectSameReferenceReturned()
        {
            var instance = new MemberedTestType { ReferenceField = Guid.NewGuid().ToString() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ReferenceField");
            Assert.AreSame(instance.ReferenceField, member.SetValue(instance, instance.ReferenceField));
        }

        [Test]
        public void SetValueFieldValueExpectSameValueReturned()
        {
            var instance = new MemberedTestType { ValueField = Guid.NewGuid().GetHashCode() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ValueField");
            Assert.AreEqual(instance.ValueField, member.SetValue(instance, instance.ValueField));
        }

        [Test]
        public void SetReferencePropertyValueExpectSameReferenceReturned()
        {
            var instance = new MemberedTestType { ReferenceProperty = Guid.NewGuid().ToString() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ReferenceProperty");
            Assert.AreSame(instance.ReferenceProperty, member.SetValue(instance, instance.ReferenceProperty));
        }

        [Test]
        public void SetValuePropertyValueExpectSameValueReturned()
        {
            var instance = new MemberedTestType { ValueProperty = Guid.NewGuid().GetHashCode() };
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ValueProperty");
            Assert.AreEqual(instance.ValueProperty, member.SetValue(instance, instance.ValueProperty));
        }

        [Test]
        public void SetReferenceFieldValueExpectCorrectValueSet()
        {
            var instance = new MemberedTestType();
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ReferenceField");
            var value = member.SetValue(instance, Guid.NewGuid().ToString());
            Assert.AreSame(instance.ReferenceField, value);
        }

        [Test]
        public void SetValueFieldValueExpectCorrectValueSet()
        {
            var instance = new MemberedTestType();
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ValueField");
            var value = member.SetValue(instance, Guid.NewGuid().GetHashCode());
            Assert.AreEqual(instance.ValueField, value);
        }

        [Test]
        public void SetReferencePropertyValueExpectCorrectValueSet()
        {
            var instance = new MemberedTestType();
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ReferenceProperty");
            var value = member.SetValue(instance, Guid.NewGuid().ToString());
            Assert.AreSame(instance.ReferenceProperty, value);
        }

        [Test]
        public void SetValuePropertyValueExpectCorrectValueSet()
        {
            var instance = new MemberedTestType();
            var member = instance.GetType().GetFastMembers().Single(m => m.Name == "ValueProperty");
            var value = member.SetValue(instance, Guid.NewGuid().GetHashCode());
            Assert.AreEqual(instance.ValueProperty, value);
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