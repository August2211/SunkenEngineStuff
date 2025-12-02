using Il2Cpp;
using MelonLoader;

namespace SunkenEngineStuff
{
    public static class Level
    {
        public static void AddLevels(int levelsToAdd)
        {
            MelonLogger.Msg("[Level] Adding levels: " + levelsToAdd);
            LevelManager.Instance.AddLevel(levelsToAdd);
        }
    }
}