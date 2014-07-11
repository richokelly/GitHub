// Type: FastMembers.TypeExtensions
// Assembly: FastMembers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03B59111-626B-4E90-91A7-B0A3E0A00D19
// Assembly location: C:\Temp\Seeder\LoadTestOnyx\Release\FastMembers.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FastMembers
{
  public static class TypeExtensions
  {
    private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<IFastGetter>> FastGetters = new ConcurrentDictionary<Type, IReadOnlyCollection<IFastGetter>>();
    private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<IFastSetter>> FastSetters = new ConcurrentDictionary<Type, IReadOnlyCollection<IFastSetter>>();
    private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<IFastMember>> FastMembers = new ConcurrentDictionary<Type, IReadOnlyCollection<IFastMember>>();

    static TypeExtensions()
    {
    }

    public static IReadOnlyCollection<IFastGetter> GetFastGetters(this Type type)
    {
      return TypeExtensions.FastGetters.GetOrAdd(type, new Func<Type, IReadOnlyCollection<IFastGetter>>(TypeExtensions.CreateFastGetters));
    }

    public static IReadOnlyCollection<IFastSetter> GetFastSetters(this Type type)
    {
      return TypeExtensions.FastSetters.GetOrAdd(type, new Func<Type, IReadOnlyCollection<IFastSetter>>(TypeExtensions.CreateFastSetters));
    }

    public static IReadOnlyCollection<IFastMember> GetFastMembers(this Type type)
    {
      return TypeExtensions.FastMembers.GetOrAdd(type, new Func<Type, IReadOnlyCollection<IFastMember>>(TypeExtensions.CreateFastMembers));
    }

    private static IReadOnlyCollection<IFastMember> CreateFastMembers(Type type)
    {
      return (IReadOnlyCollection<IFastMember>) Enumerable.ToArray<IFastMember>(Enumerable.Join<IFastGetter, IFastSetter, string, IFastMember>((IEnumerable<IFastGetter>) TypeExtensions.GetFastGetters(type), (IEnumerable<IFastSetter>) TypeExtensions.GetFastSetters(type), (Func<IFastGetter, string>) (fg => fg.Name), (Func<IFastSetter, string>) (fs => fs.Name), (Func<IFastGetter, IFastSetter, IFastMember>) ((fg, fs) =>
      {
        return (IFastMember) new FastMember(new Func<object, object>(fg.GetValue), new Func<object, object, object>(fs.SetValue))
        {
          Attributes = fg.Attributes,
          DeclaringType = fg.DeclaringType,
          MemberType = fg.MemberType,
          Name = fg.Name
        };
      })));
    }

    private static IReadOnlyCollection<IFastGetter> CreateFastGetters(Type type)
    {
      return (IReadOnlyCollection<IFastGetter>) Enumerable.ToArray<IFastGetter>(Enumerable.Concat<IFastGetter>(Enumerable.Select<PropertyInfo, IFastGetter>(Enumerable.Where<PropertyInfo>((IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty), (Func<PropertyInfo, bool>) (p =>
      {
        if (p.CanRead)
          return p.GetMethod.GetParameters().Length == 0;
        else
          return false;
      })), new Func<PropertyInfo, IFastGetter>(TypeExtensions.CreateFastGetter)), Enumerable.Select<FieldInfo, IFastGetter>((IEnumerable<FieldInfo>) type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField), new Func<FieldInfo, IFastGetter>(TypeExtensions.CreateFastGetter))));
    }

    private static IReadOnlyCollection<IFastSetter> CreateFastSetters(Type type)
    {
      return (IReadOnlyCollection<IFastSetter>) Enumerable.ToArray<IFastSetter>(Enumerable.Concat<IFastSetter>(Enumerable.Select<PropertyInfo, IFastSetter>(Enumerable.Where<PropertyInfo>((IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty), (Func<PropertyInfo, bool>) (p =>
      {
        if (p.CanWrite)
          return p.SetMethod.GetParameters().Length == 1;
        else
          return false;
      })), new Func<PropertyInfo, IFastSetter>(TypeExtensions.CreateFastSetter)), Enumerable.Select<FieldInfo, IFastSetter>((IEnumerable<FieldInfo>) type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetField), new Func<FieldInfo, IFastSetter>(TypeExtensions.CreateFastSetter))));
    }

    private static IFastGetter CreateFastGetter(PropertyInfo property)
    {
      FastGetter fastGetter = new FastGetter(((Expression<Func<object, object>>) ()); //unable to render the statement
      fastGetter.Attributes = (IReadOnlyCollection<Attribute>) Enumerable.ToArray<Attribute>(CustomAttributeExtensions.GetCustomAttributes((MemberInfo) property));
      fastGetter.DeclaringType = property.DeclaringType;
      fastGetter.MemberType = property.PropertyType;
      fastGetter.Name = property.Name;
      return (IFastGetter) fastGetter;
    }

    private static IFastSetter CreateFastSetter(PropertyInfo property)
    {
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      Action<object, object> action = Expression.Lambda<Action<object, object>>((Expression) Expression.Call((Expression) Expression.Convert((Expression) parameterExpression1, property.DeclaringType), property.SetMethod, new Expression[1]
      {
        (Expression) Expression.Convert((Expression) parameterExpression2, property.PropertyType)
      }), new ParameterExpression[2]
      {
        parameterExpression1,
        parameterExpression2
      }).Compile();
      FastSetter fastSetter = new FastSetter((Func<object, object, object>) ((i, v) =>
      {
        action(i, v);
        return v;
      }));
      fastSetter.Attributes = (IReadOnlyCollection<Attribute>) Enumerable.ToArray<Attribute>(CustomAttributeExtensions.GetCustomAttributes((MemberInfo) property));
      fastSetter.DeclaringType = property.DeclaringType;
      fastSetter.MemberType = property.PropertyType;
      fastSetter.Name = property.Name;
      return (IFastSetter) fastSetter;
    }

    private static IFastGetter CreateFastGetter(FieldInfo field)
    {
      ParameterExpression parameterExpression;
      FastGetter fastGetter = new FastGetter(Expression.Lambda<Func<object, object>>((Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Convert((Expression) parameterExpression, field.DeclaringType), field), typeof (object)), new ParameterExpression[1]
      {
        parameterExpression
      }).Compile());
      fastGetter.Attributes = (IReadOnlyCollection<Attribute>) Enumerable.ToArray<Attribute>(CustomAttributeExtensions.GetCustomAttributes((MemberInfo) field));
      fastGetter.DeclaringType = field.DeclaringType;
      fastGetter.MemberType = field.FieldType;
      fastGetter.Name = field.Name;
      return (IFastGetter) fastGetter;
    }

    private static IFastSetter CreateFastSetter(FieldInfo field)
    {
      ParameterExpression parameterExpression;
      FastSetter fastSetter = new FastSetter(((Expression<Func<object, object, object>>) (()); //unable to render the statement
      fastSetter.Attributes = (IReadOnlyCollection<Attribute>) Enumerable.ToArray<Attribute>(CustomAttributeExtensions.GetCustomAttributes((MemberInfo) field));
      fastSetter.DeclaringType = field.DeclaringType;
      fastSetter.MemberType = field.FieldType;
      fastSetter.Name = field.Name;
      return (IFastSetter) fastSetter;
    }
  }
}
