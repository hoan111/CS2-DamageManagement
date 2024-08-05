using CounterStrikeSharp.API.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDamageManagementAPI;

namespace DamageManagement.API
{
    public class DamageManagementAPI : IDamageManagementAPI
    {
        List<CallOriginalOnTakeDamageMethod> _handler = new List<CallOriginalOnTakeDamageMethod>();
        public void Hook_OnTakeDamage(IDamageManagementAPI.CallOriginalOnTakeDamageMethod handler)
        {
            _handler.Add(handler);
        }

        public void Unhook_OnTakeDamage(IDamageManagementAPI.CallOriginalOnTakeDamageMethod handler)
        {
            _handler.Remove(handler);
        }
        public bool IsNeedCallOriginalMethod()
        {
            foreach (var handler in _handler)
            {
                bool CallOriginalMethod = handler.Invoke();
                return CallOriginalMethod;
            } 
            return false;
        }
    }
}
