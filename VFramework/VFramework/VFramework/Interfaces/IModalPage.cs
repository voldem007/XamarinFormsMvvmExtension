using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFramework.Interfaces
{
    interface IModalPage
    {
        TaskCompletionSource<bool> tcs { get; set; }
    }
}
