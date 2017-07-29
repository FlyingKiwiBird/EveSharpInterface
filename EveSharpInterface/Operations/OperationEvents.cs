using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.Operations
{
  public static class OperationEvents
  {
    public delegate void OperationSuccessHandler(dynamic data);
    public delegate void OperationFailureHandler();
  }
}
