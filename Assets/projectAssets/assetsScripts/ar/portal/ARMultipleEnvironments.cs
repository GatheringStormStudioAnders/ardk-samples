namespace Sugar.AR.Portal
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using TMPro;
    public class ARMultipleEnvironments : MonoBehaviour
    {
        public ARPortal myARPortal;

        public UnityEvent<int> onEnvironmentChanged;

        public TMP_Dropdown dropdown;

        public void Start()
        {
            onEnvironmentChanged.RemoveAllListeners();
            onEnvironmentChanged.AddListener(myARPortal.SelectEnvironment);
        }
        public void OnDropDownChanged()
        {
            onEnvironmentChanged?.Invoke(dropdown.value);
        }
    }
}
