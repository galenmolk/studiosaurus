using TMPro;
using UnityEngine;

namespace Studiosaurus {
    public class StatusBar : MonoBehaviour
    {
        [SerializeField] private Animator loadResultAnimator = null;
        [SerializeField] private TMP_Text loadResultText = null;

        private const string DISPLAY_ANIM_TRIGGER = "Display";

        private void Awake()
        {
            AssetLoadService.Instance.broadcastLoadMessage.AddListener(DisplayLoadMessage);
        }

        private void DisplayLoadMessage(string message)
        {
            loadResultText.text = message;
            loadResultAnimator.SetTrigger(DISPLAY_ANIM_TRIGGER);
        }
    }
}