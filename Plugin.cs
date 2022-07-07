﻿using BepInEx;
using GorillaLocomotion;
using System;
using UnityEngine;
using Utilla;

namespace GorillaTagModTemplateProject
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>


	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;
		private Rigidbody PlayerBody;

		void OnEnable() {


			HarmonyPatches.ApplyHarmonyPatches();
			Utilla.Events.GameInitialized += OnGameInitialized;

		}


		void OnDisable() {


			HarmonyPatches.RemoveHarmonyPatches();
			Utilla.Events.GameInitialized -= OnGameInitialized;
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			PlayerBody = ((Collider)Player.Instance.bodyCollider).attachedRigidbody;
		}

		void Update()
		{
			Main();

		}


		void Main()
		{
			if (OVRInput.Get(OVRInput.Button.One))
			{
				if (PlayerBody.useGravity == true)
				{
					PlayerBody.useGravity = false;
				}
				else
				{
					PlayerBody.useGravity = true;
				}
			}

		}

		[ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{
			inRoom = true;
		}

		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{
			inRoom = false;
		}
	}
}