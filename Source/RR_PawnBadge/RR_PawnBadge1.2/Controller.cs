using System.Collections.Generic;
using System.Linq;
using Verse;
using UnityEngine;

namespace RR_PawnBadge
{
	[StaticConstructorOnStartup]
    public static class Controller
    {
		public static readonly Texture2D GreyTex = NewSolidColorTexture(Color.gray);
		public static bool WhatTheHack = false;

		public static Texture2D NewSolidColorTexture(Color color)
		{
			if (!UnityData.IsInMainThread)
			{
				Log.Error("Tried to create a texture from a different thread.", false);
				return null;
			}
            Texture2D texture2D = new Texture2D(35, 35)
            {
                name = "RR_PawnBadge-SolidColorTex-" + color
            };

            var fillColorArray = texture2D.GetPixels();
			for (var i = 0; i < fillColorArray.Length; ++i)
			{
				fillColorArray[i] = color;
			}
			texture2D.SetPixels(fillColorArray);
			texture2D.Apply();
			return texture2D;
		}

		public static void EditDefs()
		{
			IEnumerable<ThingDef> things = (
				from def in DefDatabase<ThingDef>.AllDefs
				where def.race != null && (def.race.Humanlike || (WhatTheHack && def.race.IsMechanoid))
				select def
			);
			foreach (ThingDef t in things)
			{
				// add Badge component property to all humanlike pawns
				if (t.comps == null)
				{
					t.comps = new List<CompProperties>(1);
				}
				t.comps.Add(new CompProperties_Badge());
				//	Log.Message("added Badge comp to " + t.defName);

				// add badge tab to pawns
				if (t.inspectorTabsResolved == null)
				{
					t.inspectorTabsResolved = new List<InspectTabBase>(1);
				}
				/*
				// this was the original method, however the tab is lost if ResolveReferences is called on the def
					t.inspectorTabsResolved.Add(InspectTabManager.GetSharedInstance(typeof(ITab_Pawn_Badge)));
				*/
				// this is my fix, copied from Cpt Ohu's Corruption mod
				t.inspectorTabs.Add(typeof(ITab_Pawn_Badge));
				//	Log.Message("added Badge tab to " + t.defName);
				t.ResolveReferences();
			}
		}

		static Controller()
		{
			WhatTheHack = ModLister.GetActiveModWithIdentifier("roolo.whatthehack") != null;
			EditDefs();
		}
	}
}
