using Il2Cpp;
using MelonLoader;
using UnityEngine.Playables;

namespace SunkenEngineStuffStuff
{
    public static class Time
    {
        private static bool lockTime = false;

        // Cache the instance to avoid repeated null checks
        public static bool IsTimeLocked
        {
            get => lockTime;
            set
            {
                // Only update if the value actually changes
                if (lockTime != value)
                {
                    lockTime = value;

                    if (lockTime)
                    {
                        MelonLogger.Msg("[Time] Time lock enabled.");
                        TimeManager.Instance?.PauseTimeFlow();
                    }
                    else
                    {
                        MelonLogger.Msg("[Time] Time lock disabled.");
                        TimeManager.Instance?.ResumeTimeFlow();
                    }
                }
            }
        }

        public static void AddTime(int hours, int minutes, int seconds)
        {
            if (TimeManager.Instance != null)
                TimeManager.Instance.AddTime(hours, minutes, seconds);
            else
                MelonLogger.Warning("[Time] Cannot add time, TimeManager.Instance is null!");
        }

        public static void SubtractTime(int hours, int minutes, int seconds)
        {
            if (TimeManager.Instance != null)
                TimeManager.Instance.SubtractTime(hours, minutes, seconds);
            else
                MelonLogger.Warning("[Time] Cannot subtract time, TimeManager.Instance is null!");
        }

        public static double GetTime()
        {
            if (TimeManager.Instance != null)
                return TimeManager.Instance.GetTime();

            MelonLogger.Warning("[Time] Cannot get time, TimeManager.Instance is null!");
            return 0.0;
        }

        public static void ToggleTimeLock()
        {
            IsTimeLocked = !IsTimeLocked;
        }
    }
}
