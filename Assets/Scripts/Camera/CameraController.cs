using UnityEngine;
using Sirenix.OdinInspector;

namespace ZombieDiorama.Cameras
{
    public class CameraController : MonoBehaviour
    {
        public Camera Cam;
        private Vector3 LastPosition;
        public Transform Target;
        public float CameraDistance = -40;

        public int MouseButtom = 0;

        [Title("Rotation Axis")]
        public bool MoveAxisX = true;
        public float AngleX = 180;
        public bool MoveAxisY = true;
        public float AngleY = 180;

        public float AngleZ = 0;

        private void Update()
        {
            if (Input.GetMouseButtonDown(MouseButtom))
            {
                LastPosition = Cam.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(MouseButtom))
            {
                Vector3 direction = LastPosition - Cam.ScreenToViewportPoint(Input.mousePosition);
                Cam.transform.position = Target.position;

                if (MoveAxisY)
                    Cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * AngleY);
                else
                    Cam.transform.eulerAngles = new Vector3(AngleY, Cam.transform.eulerAngles.y, AngleZ);

                if (MoveAxisX)
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

            if (!MoveAxisY)
                Cam.transform.eulerAngles = new Vector3(AngleY, Cam.transform.eulerAngles.y, AngleZ);

            if (!MoveAxisX)
                Cam.transform.eulerAngles = new Vector3(Cam.transform.eulerAngles.x, AngleX, AngleZ);
        }
    }
}
