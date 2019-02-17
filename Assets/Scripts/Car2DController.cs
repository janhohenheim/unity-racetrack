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
            var rigidBody = GetComponent<Rigidbody2D>();
            if (IsAccelerateButtonPressed)
            {
                rigidBody.AddForce(transform.up * SpeedForce);
                Debug.Log($"{InputName.Accelerate} Button pressed");
            }
            if (IsBrakesButtonPressed)
            {
                Debug.Log($"{InputName.Brakes} Button pressed");
            }

            rigidBody.AddTorque(HorizontalAxis * TorqueForce);

            rigidBody.velocity = ForwardVelocity();
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
            var rigidBody = GetComponent<Rigidbody2D>();
            return direction * Vector2.Dot(rigidBody.velocity, direction);
        }

        private bool IsAccelerateButtonPressed { get; set; }

        private bool IsBrakesButtonPressed { get; set; }

        private float HorizontalAxis { get; set; }
   
         
        private float SpeedForce { get; } = 10;

        private float TorqueForce { get; } = -2;
    }
}
