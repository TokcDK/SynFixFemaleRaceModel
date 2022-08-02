using Mutagen.Bethesda;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace SynFixFemaleRaceModel
{
    public class Program
    {
        public static Lazy<Settings> PatchSettings = null!;

        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetAutogeneratedSettings("Settings", "settings.json", out PatchSettings)
                .SetTypicalOpen(GameRelease.SkyrimSE, "YourPatcher.esp")
                .Run(args);
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            bool checkFemale = PatchSettings.Value.CanCheckFemalePath;
            bool checkMale = PatchSettings.Value.CanCheckMalePath;
            var femaleFilePath = checkFemale ? @"Actors\Character\DefaultFemale.hkx" : null;
            var maleFilePath = checkMale ? @"Actors\Character\DefaultMale.hkx" : null;
            foreach (var raceGetter in state.LoadOrder.PriorityOrder.Race().WinningOverrides())
            {
                if (raceGetter == null || raceGetter.BehaviorGraph == null) continue;

                if (checkFemale && raceGetter.BehaviorGraph.Female != null && raceGetter.BehaviorGraph.Female.File == maleFilePath)
                {
                    Console.WriteLine($"Fix Female model path for {raceGetter.FormKey.ID}|{raceGetter.EditorID}");
                    state.PatchMod.Races.GetOrAddAsOverride(raceGetter).BehaviorGraph.Female!.File = femaleFilePath!;
                }
                if (checkMale && raceGetter.BehaviorGraph.Male != null && raceGetter.BehaviorGraph.Male.File == femaleFilePath)
                {
                    Console.WriteLine($"Fix Male model path for {raceGetter.FormKey.ID}|{raceGetter.EditorID}");
                    state.PatchMod.Races.GetOrAddAsOverride(raceGetter).BehaviorGraph.Male!.File = maleFilePath!;
                }
            }
        }
    }
}