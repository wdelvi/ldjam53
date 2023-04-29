using UnityEngine;

namespace ExampleSonity {

    [AddComponentMenu("")]
    public class ExampleFlyingCamera : MonoBehaviour {

        private KeyCode keyMoveForward = KeyCode.W;
        private KeyCode keyMoveLeft = KeyCode.A;
        private KeyCode keyMoveBack = KeyCode.S;
        private KeyCode keyMoveRight = KeyCode.D;
        private KeyCode keyMoveUp = KeyCode.Q;
        private KeyCode keyMoveDown = KeyCode.E;
        private KeyCode keyMoveFaster = KeyCode.LeftShift;
        private KeyCode keyLookAround = KeyCode.Mouse1;

        private float mainSpeed = 5000f;
        private float shiftMultiply = 10;
        private float shiftMultiplyCurrent = 1f;
        private float mouseSpeed = 500f;
        private float yaw = 0.0f;
        private float pitch = 0.0f;
        private Rigidbody cachedRigidbody;

        void Start() {
            // Sets gravity, in case it was changed in the preferences
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
            cachedRigidbody = gameObject.AddComponent<Rigidbody>();
            cachedRigidbody.useGravity = false;
            cachedRigidbody.drag = 4f;
        }

        void Update() {
            // For looking around
            if (Input.GetKey(keyLookAround)) {
                yaw += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
                pitch -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;
                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }
        }

        void FixedUpdate() {
            // For moving the camera
            if (Input.GetKey(keyMoveFaster)) {
                shiftMultiplyCurrent = shiftMultiply;
            } else {
                shiftMultiplyCurrent = 1f;
            }
            if (Input.GetKey(keyMoveForward)) {
                cachedRigidbody.AddRelativeForce(Vector3.forward * mainSpeed * shiftMultiplyCurrent * Time.fixedDeltaTime);
            }
            if (Input.GetKey(keyMoveLeft)) {
                cachedRigidbody.AddRelativeForce(Vector3.left * mainSpeed * shiftMultiplyCurrent * Time.fixedDeltaTime);
            }
            if (Input.GetKey(keyMoveBack)) {
                cachedRigidbody.AddRelativeForce(Vector3.back * mainSpeed * shiftMultiplyCurrent * Time.fixedDeltaTime);
            }
            if (Input.GetKey(keyMoveRight)) {
                cachedRigidbody.AddRelativeForce(Vector3.right * mainSpeed * shiftMultiplyCurrent * Time.fixedDeltaTime);
            }
            if (Input.GetKey(keyMoveUp)) {
                cachedRigidbody.AddForce(Vector3.up * mainSpeed * shiftMultiplyCurrent * Time.fixedDeltaTime);
            }
            if (Input.GetKey(keyMoveDown)) {
                cachedRigidbody.AddForce(Vector3.down * mainSpeed * shiftMultiplyCurrent * Time.fixedDeltaTime);
            }
        }
    }
}