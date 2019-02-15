using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target;

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
            transform.position = new Vector3(Target.position.x, Target.position.y, -10f);
        }
    }
}
