using BepInEx;
using UnityEngine;
using UnityEngine.XR;
using Utilla;
using System.ComponentModel;

namespace NoMoreGravity
{
    [Description("HauntedModMenu")]
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool InModdedRoom;
        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }
        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().useGravity = true;
        }
        void Update()
        {
            if (InModdedRoom)
            {
                bool NoGravity; InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primaryButton, out NoGravity);
                if (NoGravity)
                {
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().useGravity = false;
                }
                else if (!NoGravity)
                {
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().useGravity = true;
                }
            }
        }
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            InModdedRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().useGravity = true;
            InModdedRoom = false;
        }
    }
}
