using UnityEngine;


namespace VoxelCharacter
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0, 1.0f, 0);
        [SerializeField] private float sensitivity = 3.0f;
        [SerializeField] private float smoothTime = 0.1f;

        private float distanceFromTarget;
        private float rotationX = 160.0f;

        private Vector3 currentRotation = Vector3.zero;
        private Vector3 smoothVelocity = Vector3.zero;


        private void Awake()
        {
            distanceFromTarget = Vector3.Distance(this.transform.position, target.transform.position);

            currentRotation = transform.position + Vector3.up * rotationX;
            Rotate();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                float mouseX = Input.GetAxis("Mouse X") * sensitivity;
                rotationX += mouseX;
            }
            
            currentRotation = Vector3.SmoothDamp(currentRotation, transform.position + Vector3.up * rotationX, ref smoothVelocity, smoothTime);
            Rotate();
        }


        private void Rotate()
        {
            transform.localEulerAngles = currentRotation;
            transform.position = (target.position + offset) - transform.forward * distanceFromTarget;
        }

    }
}