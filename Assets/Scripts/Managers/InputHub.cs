using UnityEngine;
using UnityEngine.InputSystem;

namespace EventHorizon.Managers
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHub : MonoBehaviour
    {
        public Vector2 Move { get { return move; } }
        public bool Jump { get { return jump; } }
        public bool Grab {  get { return grab; } }
        public bool Push { get { return push; } }
        public bool LockOn {  get { return lockOn; } }
        public bool Pause {  get { return pause; } }

        public static InputHub Instance;

        private PlayerInput input;

        private Vector2 move;
        private bool jump;
        private bool grab;
        private bool push;
        private bool lockOn;
        private bool pause;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);

            input = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            input.onActionTriggered += OnAction;
        }

        private void OnDisable()
        {
            input.onActionTriggered -= OnAction;
        }

        public void OnAction(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case "Move":
                    move = context.ReadValue<Vector2>();
                    break;
                case "Jump":
                    SetBool(ref jump, context);
                    break;
                case "Grab":
                    SetBool(ref grab, context);
                    break;
                case "Push":
                    SetBool(ref push, context);
                    break;
                case "LockOn":
                    SetBool(ref lockOn, context);
                    break;
                case "Pause":
                    SetBool(ref pause, context);
                    break;
            }
        }

        private void SetBool(ref bool value, InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                value = true;
            }

            if (context.canceled)
            {
                value = false;
            }
        }
    }
}
