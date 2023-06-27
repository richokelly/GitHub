using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace ROK.Reflection.FastMembers
{
    public static class TypeExtensions
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<IFastGetter>> FastGetters = new ConcurrentDictionary<Type, IReadOnlyCollection<IFastGetter>>();
        private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<IFastSetter>> FastSetters = new ConcurrentDictionary<Type, IReadOnlyCollection<IFastSetter>>();
        private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<IFastMember>> FastMembers = new ConcurrentDictionary<Type, IReadOnlyCollection<IFastMember>>();

        public static IReadOnlyCollection<IFastGetter> GetFastGetters(this Type type)
        {
            return FastGetters.GetOrAdd(type, CreateFastGetters);
        }

        public static IReadOnlyCollection<IFastSetter> GetFastSetters(this Type type)
        {
            return FastSetters.GetOrAdd(type, CreateFastSetters);
        }

        public static IReadOnlyCollection<IFastMember> GetFastMembers(this Type type)
        {
            return FastMembers.GetOrAdd(type, CreateFastMembers);
        }

        private static IReadOnlyCollection<IFastMember> CreateFastMembers(Type type)
        {
            return GetFastGetters(type).Join(GetFastSetters(type), fg => fg.Name, fs => fs.Name, (fg, fs) => new FastMember(fg.GetValue, fs.SetValue)
            {
                Attributes = fg.Attributes,
                DeclaringType = fg.DeclaringType,
                MemberType = fg.MemberType,
                Name = fg.Name
            }).ToArray();
        }

        private static IReadOnlyCollection<IFastGetter> CreateFastGetters(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty).Where(p => p.CanRead && p.GetMethod.GetParameters().Length == 0).Select(CreateFastGetter).Concat(type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField).Select(CreateFastGetter)).ToArray();
        }

        private static IReadOnlyCollection<IFastSetter> CreateFastSetters(Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(p => p.CanWrite && p.SetMethod.GetParameters().Length == 1).Select(CreateFastSetter).Concat(type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField).Select(CreateFastSetter)).ToArray();
        }

        private static IFastGetter CreateFastGetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object));
            var getter = Expression.Call(Expression.Convert(instance, property.DeclaringType), property.GetMethod);
            var cast = Expression.Convert(getter, typeof(object));

            return new FastGetter(Expression.Lambda<Func<object, object>>(cast, instance).Compile())
            {
                Attributes = property.GetCustomAttributes().ToArray(),
                DeclaringType = property.DeclaringType,
                MemberType = property.PropertyType,
                Name = property.Name
            };
        }

        private static IFastSetter CreateFastSetter(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(object));
            var value = Expression.Parameter(typeof(object));
            var setter = Expression.Call(Expression.Convert(instance, property.DeclaringType), property.SetMethod, Expression.Convert(value, property.PropertyType));
            var compiled = Expression.Lambda<Action<object, object>>(setter, instance, value).Compile();
            return new FastSetter((i, v) => { compiled(i, v); return v; })
            {
                Attributes = property.GetCustomAttributes().ToArray(),
                DeclaringType = property.DeclaringType,
                MemberType = property.PropertyType,
                Name = property.Name
            };
        }

        private static IFastGetter CreateFastGetter(FieldInfo field)
        {
            var instance = Expression.Parameter(typeof(object));
            var getter = Expression.Field(Expression.Convert(instance, field.DeclaringType), field);
            var cast = Expression.Convert(getter, typeof(object));

            return new FastGetter(Expression.Lambda<Func<object, object>>(cast, instance).Compile())
            {
                Attributes = field.GetCustomAttributes().ToArray(),
                DeclaringType = field.DeclaringType,
                MemberType = field.FieldType,
                Name = field.Name
            };
        }

        private static IFastSetter CreateFastSetter(FieldInfo field)
        {
            var instance = Expression.Parameter(typeof(object));
            var value = Expression.Parameter(typeof(object));
            var setter = Expression.Assign(Expression.Field(Expression.Convert(instance, field.DeclaringType), field), Expression.Convert(value, field.FieldType));
            var cast = Expression.Convert(setter, typeof(object));

            return new FastSetter(Expression.Lambda<Func<object, object, object>>(cast, instance, value).Compile())
            {
                Attributes = field.GetCustomAttributes().ToArray(),
                DeclaringType = field.DeclaringType,
                MemberType = field.FieldType,
                Name = field.Name
            };
        }
    }
}
