//***************************************************************************************
// Writer: Stylish Esper
//***************************************************************************************

using System;

namespace Esper.Freeloader
{
    /// <summary>
    /// A loading progress tracker.
    /// </summary>
    public struct LoadingProgressTracker
    {
        /// <summary>
        /// The display text of the progress. This will be displayed in on the loading screen.
        /// </summary>
        public string displayText;

        /// <summary>
        /// A function that returns the value out of 100 that represents the progress of the process.
        /// </summary>
        public Func<float> progressGetter;

        /// <summary>
        /// The current progress as a percentage out of 100.
        /// </summary>
        private float progress;

        /// <summary>
        /// The current progress as a percentage out of 100. This will get the value from the progressGetter as needed.
        /// </summary>
        public float Progress 
        {
            get
            {
                if (IsComplete)
                {
                    return progress;
                }

                progress = progressGetter();
                return progress;
            }
        }

        /// <summary>
        /// If this process is complete.
        /// </summary>
        public bool IsComplete { get => progress >= 100f; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="displayText">The display text of the progress. This will be displayed in on the loading screen.</param>
        /// <param name="progressGetter">A function that returns the value out of 100 that represents the progress of the process.</param>
        public LoadingProgressTracker(string displayText, Func<float> progressGetter)
        {
            this.displayText = displayText;
            this.progressGetter = progressGetter;
            progress = 0;
        }
    }
}
