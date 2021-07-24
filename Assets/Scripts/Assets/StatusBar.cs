using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus {
    public class StatusBar : MonoBehaviour
    {
        [SerializeField] private Animator loadResultAnimator = null;
        [SerializeField] private TMP_Text loadResultText = null;
        [SerializeField] private Slider progressBar = null;
        [SerializeField] private TMP_Text progressText = null;

        private const string DISPLAY_ANIM_TRIGGER = "Display";

        private const string LOADING_TEXT = "LOADING:";

        private void Awake()
        {
            AssetLoadService.Instance.broadcastLoadMessage.AddListener(DisplayLoadMessage);
            AssetLoadService.Instance.onDownloadInProgress.AddListener(DisplayLoadProgress);
        }

        private void DisplayLoadMessage(string message)
        {
            progressBar.gameObject.SetActive(false);
            loadResultText.text = message;
            loadResultAnimator.SetTrigger(DISPLAY_ANIM_TRIGGER);
        }

        private void DisplayLoadProgress(float progress)
        {
            if (!progressBar.gameObject.activeInHierarchy)
                progressBar.gameObject.SetActive(true);

            progressText.text = $"{LOADING_TEXT} {(int)(progress * 100)}%";
            progressBar.value = progress;
        }
    }
}