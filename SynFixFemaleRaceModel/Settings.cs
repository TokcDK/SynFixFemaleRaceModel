using Mutagen.Bethesda.Synthesis.Settings;

namespace SynFixFemaleRaceModel
{
    public class Settings
    {
        [SynthesisSettingName("Check Female model path switcher")]
        [SynthesisTooltip("Enable if need to check Female model path")]
        [SynthesisDescription("When enabled will be check Female model path and if it equals to Male, it will be fixed")]
        public bool CanCheckFemalePath { get; set; } = true;
        [SynthesisSettingName("Check Male model path switcher")]
        [SynthesisTooltip("Enable if need to check Male model path")]
        [SynthesisDescription("When enabled will be check Male model path and if it equals to Female, it will be fixed")]
        public bool CanCheckMalePath { get; set; } = true;
    }
}