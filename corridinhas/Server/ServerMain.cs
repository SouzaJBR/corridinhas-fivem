using System;
using System.IO;
using CitizenFX.Core;
using System.Collections.Generic;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;

using Corridinhas.Shared.Model.SocialClub;
using System.Dynamic;

namespace Corridinhas.Server
{
    public class ServerMain : BaseScript
    {
        public ServerMain()
        {
            Debug.WriteLine("Hi from Corridinhas.Server! v4");
            Debug.WriteLine("Done!");
        }

        [Command("hello_server")]
        public void HelloServer(int source, List<object> args, string raw)
        {
            if (source > 0) {
                var name = GetPlayerName(source.ToString());
                var ped = GetPlayerPed(source.ToString());

                GiveWeaponToPed(ped, (uint) GetHashKey("WEAPON_PISTOL"), 15, false, true);

                Debug.WriteLine($"Gave a Weapon to {name}");
            } else {
                Debug.WriteLine("Not invoked from player");
            }
        }

        [Command("corre")]
        public void LoadRaceJob(int source, List<object> args, string raw)
        {
            if (source > 0) {
                var name = GetPlayerName(source.ToString());
                var ped = GetPlayerPed(source.ToString());


                TriggerLatentClientEvent("requestTrackLoad", 5 * 1024, "" );

                Debug.WriteLine($"Gave a Weapon to {name}");
            } else {
                Debug.WriteLine("Not invoked from player");
            }
        }
    }
}
