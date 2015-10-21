using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsV1.Module.Attributes
{
    public sealed class ConfirmableValueAttribute : Attribute
    {
        public string MethodName { get; set; }
    }
}
