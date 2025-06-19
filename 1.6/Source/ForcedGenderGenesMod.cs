using HarmonyLib;
using Verse;

namespace ForcedGenderGenes
{
    public class ForcedGenderGenesMod : Mod
    {
        public const string PACKAGE_ID = "forcedgendergenes.1trickPwnyta";
        public const string PACKAGE_NAME = "Forced Gender Genes";

        public ForcedGenderGenesMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}
