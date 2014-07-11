// Type: FastMembers.IMember
// Assembly: FastMembers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03B59111-626B-4E90-91A7-B0A3E0A00D19
// Assembly location: C:\Temp\Seeder\LoadTestOnyx\Release\FastMembers.dll

using System;
using System.Collections.Generic;

namespace FastMembers
{
  public interface IMember
  {
    IReadOnlyCollection<Attribute> Attributes { get; }

    Type DeclaringType { get; }

    string Name { get; }

    Type MemberType { get; }
  }
}
