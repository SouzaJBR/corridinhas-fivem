using System;
using System.Threading.Tasks;
using System.IO;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using Newtonsoft.Json;
using System.Collections.Generic;

using Corridinhas.Shared.Model.SocialClub;

namespace Corridinhas.Client
{
    public class ClientMain : BaseScript
    {
        public ClientMain()
        {
            Debug.WriteLine("Hi from Corridinhas.Client! v3");
            EventHandlers["requestTrackLoad"] += new Action<string>(LoadTrackFromJob);

            
        }

        public void LoadTrackFromJob(string id) {

            Debug.WriteLine("Received");

            var jobResponse = File.ReadAllText(@"lh9ZKASM6UmL4ZFiA3OuAA.json");
            var jobData = JsonConvert.DeserializeObject<UserGeneratedRace>(jobResponse);

            var mission = jobData.mission;

            var objects = mission.prop;

            var models = objects.model;
            var locations = objects.loc;
            var rotations = objects.vRot;
            var headings = objects.head;

            var createdObjects = new List<int>();

            for (int i = 0; i < objects.no; i++) {
                var model = (uint) models[i];
                var location = locations[i];
                var rotation = rotations[i];
                var heading = headings[i];

                if (!IsModelInCdimage(model)) {
                    Debug.WriteLine($"Model hash {model} not in Cdimage");
                    return;
                }

                RequestModel(model);

                while (!HasModelLoaded(model)) Wait(0);

                var objectCreated = CreateObjectNoOffset(model, (float) location.x, (float) location.y, (float) location.z, true, true, false);
                createdObjects.Add(objectCreated);

                FreezeEntityPosition(objectCreated, true);

                SetEntityHeading(objectCreated, (float) heading);
                SetEntityRotation(objectCreated, (float) rotation.x, (float) rotation.y, (float) rotation.z, 2, false);
            }

            var playerPed = GetPlayerPed(-1);
            var firstObjectLocation = locations[0];
            SetEntityCoords(playerPed, (float) firstObjectLocation.x, (float) firstObjectLocation.y, (float) firstObjectLocation.z, false, false, false, false);
        }

        [Tick]
        public Task OnTick()
        {
            return Task.FromResult(0);
        }
    }
}
