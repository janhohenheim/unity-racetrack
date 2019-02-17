using System;
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
            var torqueForceLerpStep = _rigidBody.velocity.magnitude / SpeedLimitForMaxTorqueApplication;
            var torqueForce = Mathf.Lerp(0, TorqueForce, torqueForceLerpStep);
            _rigidBody.angularVelocity = HorizontalAxis * torqueForce;

            var driftFactor = GetDriftFactor();

            _rigidBody.velocity = ForwardVelocity() + RightVelocity() * driftFactor;

        }

        private float GetDriftFactor()
        {
            var sidewaysVelocity = RightVelocity().magnitude;
            switch (_driftState)
            {
                case DriftState.Sticky:
                    var driftShouldGoSlippy = sidewaysVelocity > MaxStickyVelocity;
                    if (driftShouldGoSlippy)
                    {
                        _driftState = DriftState.Slippy;
                    }
                    return driftShouldGoSlippy ? SlippyDriftFactor : StickyDriftFactor;
                case DriftState.Slippy:
                    var driftShouldGoSticky = sidewaysVelocity < MinSlippyVelocity;
                    if (driftShouldGoSticky)
                    {
                        _driftState = DriftState.Sticky;
                    }
                    return driftShouldGoSticky ? StickyDriftFactor : SlippyDriftFactor;
                default:
                    throw new ArgumentOutOfRangeException(_driftState.ToString());
            }
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
                _rigidBody.AddForce(transform.up * -SpeedForce / 2f);
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


        private Rigidbody2D _rigidBody;

        private const float SpeedForce = 15f;

        private const float TorqueForce = -200f;

        private const float StickyDriftFactor = 0.9f;

        private const float SlippyDriftFactor = 1f;

        private const float MaxStickyVelocity = 2.5f;

        private const float MinSlippyVelocity = 1.5f;

        private const float SpeedLimitForMaxTorqueApplication = 2f;

        private DriftState _driftState = DriftState.Sticky;

        private enum DriftState
        {
            Sticky,
            Slippy
        }
    }
}
