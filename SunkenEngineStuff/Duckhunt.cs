using Il2Cpp;
using UnityEngine;
using Il2CppSystem.Linq;
using MelonLoader;

namespace SunkenEngineStuffStuff
{
    public static class Duckhunt
    {
        private static bool aimbotEnabled = false;
        private static float bulletSpeed = 100f;

        public static void Update()
        {
            if (!aimbotEnabled)
                return;

            if (!CarnivalManager.Instance.CarnivalGameDuckHuntController.IsGameActive)
            {
                aimbotEnabled = false;
                return;
            }

            var controller = CarnivalManager.Instance.CarnivalGameDuckHuntController;
            var cameraTarget = controller.CameraTarget;
            var rifleRoot = controller.RifleRoot;

            if (cameraTarget == null)
            {
                MelonLogger.Msg("[Duckhunt] CameraTarget is null!");
                return;
            }

            var activeTargets = controller.activeTargets;
            if (activeTargets == null || activeTargets.Count == 0)
                return;

            DuckHuntTargetController target = null;
            foreach (var t in activeTargets)
            {
                if (t != null && t.Data != null && t.Data.HuntType == HuntType.Duck)
                {
                    target = t;
                    break;
                }
            }

            if (target == null)
                return;

            Vector3 currentPos = (target.box != null)
                ? target.box.bounds.center
                : target.transform.position;

            Vector3 duckVelocity = Vector3.zero;
            
            if (target.TryGetComponent<Rigidbody>(out var rb))
            {
                duckVelocity = rb.velocity;
            }

            float distance = Vector3.Distance(rifleRoot.position, currentPos);
            
            float travelTime = distance / bulletSpeed;
            
            Vector3 predictedPos = currentPos + (duckVelocity * travelTime);

            predictedPos.y -= 0.1f;

            Vector3 cameraLocalDirection =
                cameraTarget.parent.InverseTransformPoint(predictedPos) - cameraTarget.localPosition;

            Quaternion cameraLocalLook = Quaternion.LookRotation(cameraLocalDirection);
            Vector3 cameraEuler = cameraLocalLook.eulerAngles;

            cameraTarget.localRotation = Quaternion.Euler(cameraEuler.x, cameraEuler.y, 0f);

            if (rifleRoot != null)
            {
                Vector3 worldDirection = (predictedPos - rifleRoot.position).normalized;
                Quaternion worldRotation = Quaternion.LookRotation(worldDirection);
                rifleRoot.rotation = worldRotation;
            }

            MelonLogger.Msg($"[Duckhunt] Predicting duck position. Travel time: {travelTime:F2}s, Distance: {distance:F2}");
        }
        
        public static void ToggleAimbot() {
            MelonLoader.MelonLogger.Msg("[Duckhunt] Toggled Aimbot");

            aimbotEnabled = !aimbotEnabled;
        }

        public static bool AimbotEnabled() {
            return aimbotEnabled;
        }
    }
}