// Type: FastMembers.FastSetter
// Assembly: FastMembers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03B59111-626B-4E90-91A7-B0A3E0A00D19
// Assembly location: C:\Temp\Seeder\LoadTestOnyx\Release\FastMembers.dll

using System;

namespace FastMembers
{
  public class FastSetter : Member, IFastSetter, IMember
  {
    private readonly Func<object, object, object> _setter;

    public FastSetter(Func<object, object, object> setter)
    {
      this._setter = setter;
    }

    public object SetValue(object instance, object value)
    {
      return this._setter(instance, value);
    }
  }
}
