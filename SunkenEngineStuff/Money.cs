using Il2Cpp;
using MelonLoader;

namespace SunkenEngineStuff
{
    public static class Money
    {
        public static void AddCash(int amount)
        {
            CashManager.Instance.AddCash(amount);
            MelonLogger.Msg("[Money] Added Cash: " + amount + ", New Amount: " + CashManager.Instance.CashSaveLoadData.Cash);
        }

        public static void RemoveCash(int amount)
        {
            CashManager.Instance.RemoveCash(amount);
            MelonLogger.Msg("[Money] Removed Cash: " + amount + ", New Amount: " + CashManager.Instance.CashSaveLoadData.Cash);
        }
    }
}