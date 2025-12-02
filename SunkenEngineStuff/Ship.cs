using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunkenEngineStuffStuff
{
    public static class Ship
    {
        public static void RemoveShip()
        {
            if (ShipManager.Instance != null)
            {
                ShipManager.Instance.RemoveShip();
            }
            else
            {
                MelonLoader.MelonLogger.Warning("[Ship] Cannot remove ship, ShipManager.Instance is null!");
            }
        }
    }
}
