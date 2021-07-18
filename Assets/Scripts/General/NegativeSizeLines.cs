using UnityEngine;

namespace Studiosaurus {
    public class NegativeSizeLines : MonoBehaviour
    {
        [SerializeField] private LineRenderer line1;
        [SerializeField] private LineRenderer line2;

        [SerializeField] private float lineWidth;

        [Header("Corners")]
        [SerializeField] private RectTransform topLeft;
        [SerializeField] private RectTransform topRight;
        [SerializeField] private RectTransform bottomLeft;
        [SerializeField] private RectTransform bottomRight;

        private readonly Vector3[] linePoints1 = new Vector3[2];
        private readonly Vector3[] linePoints2 = new Vector3[2];

        private bool linesEnabled = false;

        private Camera cam;
        public Camera Cam
        {
            get
            {
                if (cam == null)
                    cam = Camera.main;

                return cam;
            }
        }

        private void Awake()
        {
            gameObject.SetActive(false);
            InitializeLines();
        }

        private void InitializeLines()
        {
            line1.positionCount = 2;
            line1.startWidth = lineWidth;
            line1.endWidth = lineWidth;

            line2.positionCount = 2;
            line2.startWidth = lineWidth;
            line2.endWidth = lineWidth;
        }

        public void SetEnabled(bool isEnabled)
        {
            if (isEnabled == linesEnabled)
                return;

            linesEnabled = isEnabled;
            gameObject.SetActive(isEnabled);
        }

        private void Update()
        {
            if (!linesEnabled)
                return;

            ShowLines();
        }

        public void ShowLines()
        {
            linePoints1[0] = (Vector2)Cam.ScreenToWorldPoint(bottomLeft.transform.position);
            linePoints1[1] = (Vector2)Cam.ScreenToWorldPoint(topRight.transform.position);

            linePoints2[0] = (Vector2)Cam.ScreenToWorldPoint(topLeft.transform.position);
            linePoints2[1] = (Vector2)Cam.ScreenToWorldPoint(bottomRight.transform.position);

            line1.SetPositions(linePoints1);
            line2.SetPositions(linePoints2);
        }
    }
}