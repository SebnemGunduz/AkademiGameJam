//***************************************************************************************
// Writer: Stylish Esper
//***************************************************************************************

using System.Collections.Generic;
using UnityEngine;

namespace Esper.Freeloader
{
    /// <summary>
    /// Settings for a loading screen.
    /// </summary>
    [CreateAssetMenu(fileName = "LoadingScreenSettings", menuName = "Freeloader/Loading Screen Settings", order = 1)]
    public class LoadingScreenSettings : ScriptableObject
    {
        /// <summary>
        /// The name of the loading screen.
        /// </summary>
        public string settingsName = "Name";

        /// <summary>
        /// The name of the pseudo class used by the loading screen to animate the tips.
        /// </summary>
        public string tipAnimatorPseudoClassName;

        /// <summary>
        /// The name of the pseudo class used by the loading screen to animate the loading label.
        /// </summary>
        public string loadingLabelAnimatorPseudoClassName;

        /// <summary>
        /// The name of the pseudo class used by the loading screen to animate the continue label.
        /// </summary>
        public string continueLabelAnimatorPseudoClassName;

        /// <summary>
        /// The name of the pseudo class used by the loading screen to animate the background zoom.
        /// </summary>
        public string backgroundScaleAnimatorPseudoClassName;

        /// <summary>
        /// If the loading bar should be hidden.
        /// </summary>
        public bool hideBar;

        /// <summary>
        /// The default loading text to display.
        /// </summary>
        public string defaultLoadingText = "Loading...";

        /// <summary>
        /// If the progress percentage should be displayed.
        /// </summary>
        public bool showPercentage = true;

        /// <summary>
        /// If the animated spinner icon should be displayed.
        /// </summary>
        public bool showSpinner = true;

        /// <summary>
        /// The spinning speed of the spinner icon.
        /// </summary>
        public float spinnerSpeed = 1f;

        /// <summary>
        /// The spinner icon.
        /// </summary>
        public Texture2D spinnerIcon;

        /// <summary>
        /// A list of backgrounds. If there's more than 1, the backgrounds will work as a slideshow.
        /// </summary>
        public List<Texture2D> backgrounds;

        /// <summary>
        /// The length a single background is displayed for before being replaced by another.
        /// </summary>
        public float backgroundDisplayLength = 6;

        /// <summary>
        /// If this is true, the background will play a zoom in and out looped animation.
        /// </summary>
        public bool enableBackgroundZoom;

        /// <summary>
        /// If tips should be enabled.
        /// </summary>
        public bool showTips = true;

        /// <summary>
        /// A list of tips. The loading screen will rotate through each tip if there's more than 1.
        /// </summary>
        public List<string> tips;

        /// <summary>
        /// The title of the tips.
        /// </summary>
        public string tipsTitle = "Tip";

        /// <summary>
        /// The length a single tip is displayed for before being replaced by another.
        /// </summary>
        public float tipDisplayLength = 6;

        /// <summary>
        /// If this is enabled, the loading screen will require user input to close after loading is completed.
        /// </summary>
        public bool requireInputToContinue;

        /// <summary>
        /// The text to display when input is required to continue.
        /// </summary>
        public string continueText = "Continue";

#if UNITY_EDITOR
        /// <summary>
        /// Saves the object (editor only).
        /// </summary>
        public void Save()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
    }
}