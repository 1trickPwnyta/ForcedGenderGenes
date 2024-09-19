using FacialAnimation;
using HarmonyLib;
using System.Linq;
using Verse;

namespace ForcedGenderGenes
{
    public static class FacialAnimationUtility
    {
        public static void UpdateGenderedFacialFeatures(this Pawn pawn)
        {
            EyeballControllerComp eyeComp = pawn.GetComp<EyeballControllerComp>();
            if (eyeComp != null)
            {
                EyeballTypeDef def;
                DefDatabase<EyeballTypeDef>.AllDefsListForReading.Where(d => d.gender == Gender.None || d.gender == pawn.gender).TryRandomElement(out def);
                if (def != null)
                {
                    eyeComp.FaceType = def;
                }
            }

            LidControllerComp lidComp = pawn.GetComp<LidControllerComp>();
            if (lidComp != null)
            {
                LidTypeDef def;
                DefDatabase<LidTypeDef>.AllDefsListForReading.Where(d => d.gender == Gender.None || d.gender == pawn.gender).TryRandomElement(out def);
                if (def != null)
                {
                    lidComp.FaceType = def;
                }
            }

            AccessTools.Field(typeof(NL_SelectPartWindow), "changed").SetValue(NL_SelectPartTabWindow.instance, true);
        }
    }
}
