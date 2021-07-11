using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Studiosaurus
{
    public class Vector2Event : UnityEvent<Vector2> { }

    public class Vector2Controls : ConfigControls
    {
        [SerializeField] private TMP_InputField xInput = null;
        [SerializeField] private TMP_InputField yInput = null;

        public Vector2Event onVector2Inputted = new Vector2Event();

        public void OnEndEdit()
        {
            float.TryParse(xInput.text, out float x);
            float.TryParse(yInput.text, out float y);

            onVector2Inputted?.Invoke(new Vector2(x, y));
        }

        public void UpdateDisplayedVector(Vector2 vector2)
        {
            xInput.text = vector2.x.ToString("0.##");
            yInput.text = vector2.y.ToString("0.##");
        }
    }
}