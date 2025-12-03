using Il2Cpp;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SunkenEngineStuffStuff
{
    public static class Fly
    {
        private static bool isFlying = false;
        private static Rigidbody playerBody;
        private static Collider playerCollider;

        public static bool IsFlying => isFlying;

        public static void ToggleFly()
        {
            PlayerFPSController player = Utils.GetPlayer();

            if (player == null)
                return;

            if (playerBody == null)
                playerBody = player.GetComponent<Rigidbody>();

            if (playerCollider == null)
                playerCollider = player.GetComponent<Collider>();

            isFlying = !isFlying;

            if (isFlying)
                EnableFly();
            else
                DisableFly();

            MelonLogger.Msg("[Fly] playerBody: " + (playerBody ? "OK" : "NULL"));
            MelonLogger.Msg("[Fly] playerCollider: " + (playerCollider ? "OK" : "NULL"));
        }

        private static void EnableFly()
        {
            MelonLogger.Msg("[Fly] Fly ENABLED");

            if (playerBody != null)
            {
                playerBody.useGravity = false;
                playerBody.velocity = Vector3.zero;
            }

            if (playerCollider != null)
                playerCollider.enabled = false;
        }

        private static void DisableFly()
        {
            MelonLogger.Msg("[Fly] Fly DISABLED");

            if (playerBody != null)
                playerBody.useGravity = true;

            if (playerCollider != null)
                playerCollider.enabled = true;
        }

        public static void FlyMovementUpdate()
        {
            if (!isFlying || playerBody == null)
                return;

            Keyboard kb = Keyboard.current;
            if (kb == null)
                return;

            Vector3 move = Vector3.zero;

            float speed = 10f;
            float verticalSpeed = 10f;

            if (kb.wKey.isPressed) move += Camera.main.transform.forward;
            if (kb.sKey.isPressed) move -= Camera.main.transform.forward;
            if (kb.aKey.isPressed) move -= Camera.main.transform.right;
            if (kb.dKey.isPressed) move += Camera.main.transform.right;

            if (kb.spaceKey.isPressed) move += Vector3.up * verticalSpeed;
            if (kb.leftCtrlKey.isPressed) move -= Vector3.up * verticalSpeed;

            if (move != Vector3.zero)
                move = move.normalized * speed;

            playerBody.velocity = move;
        }
    }
}
