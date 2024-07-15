namespace Sugar.UI
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;

    using TMPro;

    public class InteractionPrompt : MonoBehaviour
    {
        public TextMeshProUGUI promptAction;
        public Image promptIcon;
        public Button promptButton;

        public void UpdatePrompt(string action, Sprite icon)
        {
            promptAction.text = action;
            promptIcon.sprite = icon;
        }
    }
}
