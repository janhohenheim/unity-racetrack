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
            IsAccelerateButtonPressed = Input.GetButton(ButtonName.Accelerate);
            IsBrakesButtonPressed = Input.GetButton(ButtonName.Brakes);
        }

        /// <summary>
        /// Update is called once per physics update
        /// </summary>
        [UsedImplicitly]
        private void FixedUpdate()
        {
            if (IsAccelerateButtonPressed)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * SpeedForce);
                Debug.Log($"{ButtonName.Accelerate} Button pressed");
            }
            if (IsBrakesButtonPressed)
            {
                Debug.Log($"{ButtonName.Brakes} Button pressed");
            }
        }

        private bool IsAccelerateButtonPressed { get; set; }

        private bool IsBrakesButtonPressed { get; set; }

        private float SpeedForce { get; } = 10;
    }
}
