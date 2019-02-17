using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class Car2DController : MonoBehaviour
    {   
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        [UsedImplicitly]
        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        [UsedImplicitly]
        private void Update()
        {
            IsAccelerateButtonPressed = Input.GetButton(InputName.Accelerate);
            IsBrakesButtonPressed = Input.GetButton(InputName.Brakes);
            HorizontalAxis = Input.GetAxis(InputName.Horizontal);
        }

        /// <summary>
        /// Update is called once per physics update
        /// </summary>
        [UsedImplicitly]
        private void FixedUpdate()
        {
            HandleInput();
            AdjustVelocity();
        }

        private void AdjustVelocity()
        {
            _rigidBody.angularVelocity = HorizontalAxis * TorqueForce;

            _rigidBody.velocity = ForwardVelocity() + RightVelocity() * SlippyDriftFactor;
        }

        private void HandleInput()
        {
            if (IsAccelerateButtonPressed)
            {
                _rigidBody.AddForce(transform.up * SpeedForce);
                Debug.Log($"{InputName.Accelerate} Button pressed");
            }
            if (IsBrakesButtonPressed)
            {
                Debug.Log($"{InputName.Brakes} Button pressed");
            }
        }

        private Vector2 ForwardVelocity()
        {
            return CalculateVelocity(transform.up);
        }

        private Vector2 RightVelocity()
        {
            return CalculateVelocity(transform.right);
        }

        private Vector2 CalculateVelocity(Vector2 direction)
        {
            return direction * Vector2.Dot(_rigidBody.velocity, direction);
        }

        private bool IsAccelerateButtonPressed { get; set; }

        private bool IsBrakesButtonPressed { get; set; }

        private float HorizontalAxis { get; set; }
   
         
        private const float SpeedForce = 10;

        private const float TorqueForce = -200;

        private const float StickyDriftFactor = 0.1f;

        private const float SlippyDriftFactor = 0.999f;

        private const float MaxStickyVelocity = 2.5f;

        private Rigidbody2D _rigidBody;
    }
}
