using HarmonyLib;
using RimWorld;
using Verse;

namespace ForcedGenderGenes
{
    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GenerateGenes")]
    public static class Patch_PawnGenerator
    {
        public static void Prefix(ref XenotypeDef xenotype, ref PawnGenerationRequest request)
        {
            bool valid = true;

            if (request.FixedGender == Gender.Female)
            {
                GeneDef maleOnlyGene = DefDatabase<GeneDef>.GetNamed("MaleOnly");
                if (xenotype.genes.Contains(maleOnlyGene))
                {
                    valid = false;
                }
                if (request.ForcedCustomXenotype != null && request.ForcedCustomXenotype.genes.Contains(maleOnlyGene))
                {
                    valid = false;
                }
            }
            if (request.FixedGender == Gender.Male)
            {
                GeneDef femaleOnlyGene = DefDatabase<GeneDef>.GetNamed("FemaleOnly");
                if (xenotype.genes.Contains(femaleOnlyGene))
                {
                    valid = false;
                }
                if (request.ForcedCustomXenotype != null && request.ForcedCustomXenotype.genes.Contains(femaleOnlyGene))
                {
                    valid = false;
                }
            }

            if (!valid)
            {
                xenotype = XenotypeDefOf.Baseliner;
                request.ForcedCustomXenotype = null;
            }
        }
    }
}
