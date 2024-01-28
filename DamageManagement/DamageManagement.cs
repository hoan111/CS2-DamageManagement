using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Menu;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DamageManagement
{
    public class Config : BasePluginConfig
    {
        [JsonPropertyName("Enable")] public bool enableCfg { get; set; } = true;
    }

    public class DamageManagement : BasePlugin, IPluginConfig<Config>
    {
        public override string ModuleName => "Faceit teammates damage modify";

        public override string ModuleVersion => "1.0";
        public override string ModuleAuthor => "HoanNK";
        public Config Config { get; set; }
        bool enabled { get; set; }

        public override void Load(bool hotReload)
        {
            VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Hook(OnTakeDamage, HookMode.Pre);
            Logger.LogInformation("Loading plugin");
        }

        public void OnConfigParsed(Config config)
        {
            enabled = config.enableCfg;
        }

        public override void Unload(bool hotReload)
        {
            VirtualFunctions.CBaseEntity_TakeDamageOldFunc.Unhook(OnTakeDamage, HookMode.Pre);
            Logger.LogInformation("Unloading plugin");
        }
        private HookResult OnTakeDamage(DynamicHook hook)
        {
            if (enabled)
            {
                try
                {
                    string[] takeDamageInflictors = { "inferno", "hegrenade_projectile", "flashbang_projectile", "smokegrenade_projectile", "decoy_projectile", "planted_c4" };
                    var victim = hook.GetParam<CEntityInstance>(0);
                    var damageInfo = hook.GetParam<CTakeDamageInfo>(1);
                    string inflictor = damageInfo.Inflictor.Value.DesignerName ?? "";
                    var attackerPlayer = new CCSPlayerPawn(damageInfo.Attacker.Value.Handle);
                    var playerTakenDmg = new CCSPlayerController(victim.Handle);
                    //Check if friendly fire
                    if (attackerPlayer.TeamNum == playerTakenDmg.TeamNum && "player".Equals(victim.DesignerName))
                    {
                        if (takeDamageInflictors.Contains(inflictor))
                        {
                            return HookResult.Continue;
                        }
                        return HookResult.Handled;
                    }
                    return HookResult.Continue;
                }
                catch (Exception ex)
                {
                    Logger.LogError("{@error}", ex);
                }
                return HookResult.Continue;
            }
            return HookResult.Continue;
        }

        [ConsoleCommand("faceit_dmg_enable", "Enable plugin")]
        [CommandHelper(minArgs: 1, usage: "[true/false]", whoCanExecute: CommandUsage.SERVER_ONLY)]
        [RequiresPermissions("@css/cvar")]
        public void OnEnableCommand(CCSPlayerController? player, CommandInfo commandInfo)
        {
            string arg = commandInfo.GetArg(1);
            if ("true".Equals(arg))
            {
                enabled = true;
            }
            else if ("false".Equals(arg))
            {
                enabled = false;
            }
            else
            {
                commandInfo.ReplyToCommand("true/false value only", true);
            }
        }
    }
}
