// Type: FastMembers.FastGetter
// Assembly: FastMembers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03B59111-626B-4E90-91A7-B0A3E0A00D19
// Assembly location: C:\Temp\Seeder\LoadTestOnyx\Release\FastMembers.dll

using System;

namespace FastMembers
{
  public class FastGetter : Member, IFastGetter, IMember
  {
    private readonly Func<object, object> _getter;

    public FastGetter(Func<object, object> getter)
    {
      this._getter = getter;
    }

    public object GetValue(object instance)
    {
      return this._getter(instance);
    }
  }
}
