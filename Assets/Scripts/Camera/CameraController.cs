using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace ZombieDiorama.Cameras
{
    public class CameraController : MonoBehaviour
    {
        [FormerlySerializedAs("cam")] public Camera Cam;
        [FormerlySerializedAs("posanterior")] private Vector3 LastPosition;
        [FormerlySerializedAs("alvo")] public Transform Target;
        [FormerlySerializedAs("camdistancia")] public float CameraDistance = -40;

        public int mouseButtom = 0;

        [Title("Rotation Axis")]
        public bool moveAxisX = true;
        public float AngleX = 180;
        public bool moveAxisY = true;
        public float AngleY = 180;

        public float AngleZ = 0;

        private void Update()
        {
            if (Input.GetMouseButtonDown(mouseButtom))
            {
                LastPosition = Cam.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(mouseButtom))
            {
                Vector3 direction = LastPosition - Cam.ScreenToViewportPoint(Input.mousePosition);
                Cam.transform.position = Target.position;

                if (moveAxisY)
                    Cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * AngleY);
                else
                    Cam.transform.eulerAngles = new Vector3(AngleY, Cam.transform.eulerAngles.y, AngleZ);

                if (moveAxisX)
                    Cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * AngleX, Space.World);
                else
                    Cam.transform.eulerAngles = new Vector3(Cam.transform.eulerAngles.x, AngleX, AngleZ);

                Cam.transform.Translate(new Vector3(0, 0, CameraDistance));
                LastPosition = Cam.ScreenToViewportPoint(Input.mousePosition);
            }
        }

        private void LateUpdate()
        {
            Cam.transform.position = Target.position;
            Cam.transform.Translate(new Vector3(0, 0, CameraDistance));

            if (!moveAxisY)
                Cam.transform.eulerAngles = new Vector3(AngleY, Cam.transform.eulerAngles.y, AngleZ);

            if (!moveAxisX)
                Cam.transform.eulerAngles = new Vector3(Cam.transform.eulerAngles.x, AngleX, AngleZ);
        }
    }
}
