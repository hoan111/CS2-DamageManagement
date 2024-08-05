using CounterStrikeSharp.API.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDamageManagementAPI
{
    delegate bool CallOriginalOnTakeDamageMethod();
    void Hook_OnTakeDamage(CallOriginalOnTakeDamageMethod handler);
    void Unhook_OnTakeDamage(CallOriginalOnTakeDamageMethod handler);
}
