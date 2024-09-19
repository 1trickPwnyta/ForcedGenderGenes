using RimWorld;
using Verse;

namespace ForcedGenderGenes
{
    public class Gene_ForcedGender : Gene
    {
        private Gender originalGender;

        public override void PostAdd()
        {
            base.PostAdd();
            originalGender = pawn.gender;
            if (def.defName == "FemaleOnly")
            {
                pawn.gender = Gender.Female;
            }
            else if (def.defName == "MaleOnly")
            {
                pawn.gender = Gender.Male;
            }
            Notify_GenderChanged();
        }

        public override void PostRemove()
        {
            base.PostRemove();
            pawn.gender = originalGender;
            Notify_GenderChanged();
        }

        public void Notify_GenderChanged()
        {
            if (pawn.story.bodyType == BodyTypeDefOf.Female || pawn.story.bodyType == BodyTypeDefOf.Male)
            {
                pawn.story.bodyType = GeneticBodyType.Standard.ToBodyType(pawn);
            }

            if (pawn.story.headType.gender != Gender.None && pawn.story.headType.gender != pawn.gender)
            {
                Verse.HeadTypeDef def = DefDatabase<Verse.HeadTypeDef>.GetNamedSilentFail(pawn.story.headType.defName.Replace(pawn.story.headType.gender.ToString(), pawn.gender.ToString()));
                if (def != null)
                {
                    pawn.story.headType = def;
                }
            }
            
            if (pawn.gender == Gender.Female && !pawn.genes.HasActiveGene(DefDatabase<GeneDef>.GetNamed("Beard_Always")))
            {
                pawn.style.beardDef = BeardDefOf.NoBeard;
            }

            if (ModsConfig.IsActive("Nals.FacialAnimation"))
            {
                pawn.UpdateGenderedFacialFeatures();
            }

            pawn.Drawer.renderer.SetAllGraphicsDirty();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref originalGender, "originalGender");
        }
    }
}
