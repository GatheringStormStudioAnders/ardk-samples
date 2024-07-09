using UnityEngine.Rendering;
using TMPro;
using System;
using UnityEngine.UI;
using Niantic.Experimental.Lightship.AR.WorldPositioning;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// A component that can be used to access the most recently received HDR light estimation information
    /// for the physical environment as observed by an AR device.
    /// </summary>
    public class HDRLightEstimation : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The ARCameraManager which will produce frame events containing light estimation information.")]
        ARCameraManager m_CameraManager;

        public TextMeshProUGUI lightIntensity;
        public TextMeshProUGUI rotationDisplay;

        public Image colourDisplay;

        /// <summary>
        /// Get or set the <c>ARCameraManager</c>.
        /// </summary>
        public ARCameraManager cameraManager
        {
            get => m_CameraManager;
            set
            {
                if (m_CameraManager == value)
                    return;

                if (m_CameraManager != null)
                    m_CameraManager.frameReceived -= FrameChanged;

                m_CameraManager = value;

                if (m_CameraManager != null & enabled)
                    m_CameraManager.frameReceived += FrameChanged;
            }
        }
        
        /// <summary>
        /// The estimated brightness of the physical environment, if available.
        /// </summary>
        public float? brightness { get; private set; }

        /// <summary>
        /// The estimated color temperature of the physical environment, if available.
        /// </summary>
        public float? colorTemperature { get; private set; }

        /// <summary>
        /// The estimated color correction value of the physical environment, if available.
        /// </summary>
        public Color? colorCorrection { get; private set; }
        
        /// <summary>
        /// The estimated direction of the main light of the physical environment, if available.
        /// </summary>
        public Vector3? mainLightDirection { get; private set; }

        /// <summary>
        /// The estimated color of the main light of the physical environment, if available.
        /// </summary>
        public Color? mainLightColor { get; private set; }

        /// <summary>
        /// The estimated intensity in lumens of main light of the physical environment, if available.
        /// </summary>
        public float? mainLightIntensityLumens { get; private set; }

        /// <summary>
        /// The estimated spherical harmonics coefficients of the physical environment, if available.
        /// </summary>
        public SphericalHarmonicsL2? sphericalHarmonics { get; private set; }

        public int frameCount;
        public TextMeshProUGUI frameCountUI;

        private void Start()
        {
            m_CameraManager.frameReceived += FrameChanged;
        }

        void FrameChanged(ARCameraFrameEventArgs args)
        {
            //frameCount++;
            //frameCountUI.text = "Frame: " + frameCount.ToString();
            //Debug.Log("FrameChanged");
            //if (args.lightEstimation.averageBrightness.HasValue)
            //{
            //    brightness = args.lightEstimation.averageBrightness.Value;
            //    m_Light.intensity = brightness.Value;
            //    //lightIntensity.text = "brightness : " + brightness.Value.ToString();
            //}
            //else
            //{
            //    brightness = null;
            //    //lightIntensity.text = "brightness : null";
            //}

            //if (args.lightEstimation.mainLightIntensityLumens.HasValue)
            //{
            //    m_Light.intensity = args.lightEstimation.mainLightIntensityLumens.Value;
            //}
            //else
            //{
            //    mainLightIntensityLumens = null;
            //}

            if (args.lightEstimation.averageColorTemperature.HasValue)
            {
                colorTemperature = args.lightEstimation.averageColorTemperature.Value;
                m_Light.colorTemperature = colorTemperature.Value;
            }
            else
            {
                colorTemperature = null;
            }

            //if (args.lightEstimation.colorCorrection.HasValue)
            //{
            //    colorCorrection = args.lightEstimation.colorCorrection.Value;
            //    m_Light.color = colorCorrection.Value;
            //}
            //else
            //{
            //    colorCorrection = null;
            //}

            //if (args.lightEstimation.mainLightDirection.HasValue)
            //{
            //    mainLightDirection = args.lightEstimation.mainLightDirection;
            //    Quaternion rotation = Quaternion.LookRotation(mainLightDirection.Value);
            //    m_Light.transform.rotation = rotation;
            //    //rotationDisplay.text = "Rotation : " + mainLightDirection.Value.ToString();
            //}
            //else
            //{
            //    //rotationDisplay.text = "Rotation : null"; 
            //}

            if (args.lightEstimation.mainLightColor.HasValue)
            {
                //colourDisplay.color = mainLightColor.Value;
                mainLightColor = args.lightEstimation.mainLightColor;
                m_Light.color = mainLightColor.Value;
            }
            else
            {
                mainLightColor = null;
                //colourDisplay.color = new Color(0, 0, 0, 0);
            }

            //if (args.lightEstimation.ambientSphericalHarmonics.HasValue)
            //{
            //    sphericalHarmonics = args.lightEstimation.ambientSphericalHarmonics;
            //    RenderSettings.ambientMode = AmbientMode.Skybox;
            //    RenderSettings.ambientProbe = sphericalHarmonics.Value;
            //}
            //else
            //{
            //    sphericalHarmonics = null;
            //}
        }

        public Light m_Light;
    }
}
