using UnityEngine;

namespace EKTemplate
{
    public class CameraManager : MonoBehaviour
    {
        [HideInInspector] public Camera cam;

        #region Singleton
        public static CameraManager instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            cam = GetComponent<Camera>();
        }
        #endregion
    }
}