//***************************************************************************************
// Writer: Stylish Esper
//***************************************************************************************

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Esper.Freeloader
{
    /// <summary>
    /// A screen shown to the player while the game is loading.
    /// </summary>
    public class LoadingScreen : MonoBehaviour
    {
        /// <summary>
        /// The settings that this loading screen should use.
        /// </summary>
        public LoadingScreenSettings settings;

        /// <summary>
        /// The UI document.
        /// </summary>
        protected UIDocument document;

        /// <summary>
        /// The label that displays the tips title.
        /// </summary>
        protected Label tipsTitleLabel;

        /// <summary>
        /// The label that displays the current tip.
        /// </summary>
        protected Label tipLabel;

        /// <summary>
        /// The element that contains the tip.
        /// </summary>
        protected VisualElement tipsContainer;

        /// <summary>
        /// The loading bar element.
        /// </summary>
        protected VisualElement loadingBar;

        /// <summary>
        /// The loading label.
        /// </summary>
        protected Label loadingLabel;

        /// <summary>
        /// The loading icon container.
        /// </summary>
        protected VisualElement loadingIconContainer;

        /// <summary>
        /// The element that displays the spinner icon.
        /// </summary>
        protected VisualElement spinnerIcon;

        /// <summary>
        /// The root element of the loading screen.
        /// </summary>
        protected VisualElement root;

        /// <summary>
        /// The element that contains loading content.
        /// </summary>
        protected VisualElement loadingContent;

        /// <summary>
        /// The label that displays the continue text.
        /// </summary>
        protected Label continueLabel;

        /// <summary>
        /// The first background element.
        /// </summary>
        protected VisualElement backgroundOne;

        /// <summary>
        /// The second background element.
        /// </summary>
        protected VisualElement backgroundTwo;

        /// <summary>
        /// The spinner action.
        /// </summary>
        protected IVisualElementScheduledItem spinnerAction;

        /// <summary>
        /// The index of the displayed tip.
        /// </summary>
        protected int currentTipIndex = -1;

        /// <summary>
        /// The index of the current process.
        /// </summary>
        protected int currentProcessIndex = 0;

        /// <summary>
        /// The index of the displayed background.
        /// </summary>
        protected int currentBackgroundIndex = 0;

        /// <summary>
        /// A list of loading processes to undergo. This list will be automatically cleared when loading is complete.
        /// </summary>
        protected LoadingProgressTracker[] processes;

        /// <summary>
        /// A callback for when loading is started.
        /// </summary>
        public UnityEvent onStart = new();

        /// <summary>
        /// A callback for when the loading progress has updated.
        /// </summary>
        public UnityEvent<float> onProgressChanged = new();

        /// <summary>
        /// A callback for when loading is complete.
        /// </summary>
        public UnityEvent onComplete = new();

        /// <summary>
        /// The current progress as a percentage out of 100.
        /// </summary>
        public float Progress { get; private set; }

        /// <summary>
        /// If loading processes are running.
        /// </summary>
        public bool IsLoading { get; private set; }

        /// <summary>
        /// If the loading screen is opened.
        /// </summary>
        public bool IsOpen { get => root.enabledSelf; }

        /// <summary>
        /// The active loading screen instance.
        /// </summary>
        public static LoadingScreen Instance { get; private set; }

        protected virtual void Awake()
        {
            // Singleton
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            document = GetComponent<UIDocument>();
            tipsTitleLabel = document.rootVisualElement.Q<Label>("TipsTitleLabel");
            tipLabel = document.rootVisualElement.Q<Label>("TipLabel");
            tipsContainer = document.rootVisualElement.Q<VisualElement>("TipsContainer");
            loadingBar = document.rootVisualElement.Q<VisualElement>("LoadingBar");
            root = document.rootVisualElement.Q<VisualElement>("Root");
            loadingLabel = document.rootVisualElement.Q<Label>("LoadingLabel");
            loadingIconContainer = document.rootVisualElement.Q<VisualElement>("LoadingIconContainer");
            spinnerIcon = document.rootVisualElement.Q<VisualElement>("SpinnerIcon");
            loadingContent = document.rootVisualElement.Q<VisualElement>("LoadingContent");
            continueLabel = document.rootVisualElement.Q<Label>("ContinueLabel");
            backgroundOne = document.rootVisualElement.Q<VisualElement>("BackgroundOne");
            backgroundTwo = document.rootVisualElement.Q<VisualElement>("BackgroundTwo");

            RefreshElementStates();
        }

        private void Start()
        {
            Close();
        }

        /// <summary>
        /// Refreshes the states of each element based on the loading screen settings.
        /// </summary>
        protected virtual void RefreshElementStates()
        {
            tipsTitleLabel.text = settings.tipsTitle;

            if (settings.showTips)
            {
                tipsContainer.style.display = DisplayStyle.Flex;
            }
            else
            {
                tipsContainer.style.display = DisplayStyle.None;
            }

            loadingBar.style.display = settings.hideBar ? DisplayStyle.None : DisplayStyle.Flex;
            loadingIconContainer.style.display = settings.showSpinner ? DisplayStyle.Flex : DisplayStyle.None;
            spinnerIcon.style.backgroundImage = settings.spinnerIcon;
        }

        /// <summary>
        /// Toggles the loading label animator pseudo class.
        /// </summary>
        protected virtual void ToggleLoadingLabelClass( TransitionEndEvent endEvent)
        {
            loadingLabel.ToggleInClassList(settings.loadingLabelAnimatorPseudoClassName);
        }

        /// <summary>
        /// Toggles the continue label animator pseudo class.
        /// </summary>
        protected virtual void ToggleContinueLabelClass(TransitionEndEvent endEvent)
        {
            continueLabel.ToggleInClassList(settings.continueLabelAnimatorPseudoClassName);
        }

        /// <summary>
        /// Toggles the background one scale animator pseudo class.
        /// </summary>
        protected virtual void ToggleBackgroundOneScaleClass(TransitionEndEvent endEvent)
        {
            backgroundOne.ToggleInClassList(settings.backgroundScaleAnimatorPseudoClassName);
        }

        /// <summary>
        /// Toggles the background two scale animator pseudo class.
        /// </summary>
        protected virtual void ToggleBackgroundTwoScaleClass(TransitionEndEvent endEvent)
        {
            backgroundTwo.ToggleInClassList(settings.backgroundScaleAnimatorPseudoClassName);
        }

        /// <summary>
        /// Toggles the tip animator pseudo class.
        /// </summary>
        protected virtual void ToggleTipClass()
        {
            tipLabel.ToggleInClassList(settings.tipAnimatorPseudoClassName);
        }

        /// <summary>
        /// Toggles the tip animator pseudo class and increments the tip if the animator class is in the
        /// disabled state.
        /// </summary>
        protected virtual void ToggleTipClassAndDisplayNextTip()
        {
            ToggleTipClass();

            if (!tipLabel.ClassListContains(settings.tipAnimatorPseudoClassName))
            {
                NextTip();
            }
        }

        /// <summary>
        /// Displays the next tip.
        /// </summary>
        protected virtual void NextTip()
        {
            currentTipIndex++;

            if (currentTipIndex >= settings.tips.Count)
            {
                currentTipIndex = 0;
            }

            tipLabel.text = settings.tips[currentTipIndex];
        }

        /// <summary>
        /// Displays the continue content.
        /// </summary>
        protected virtual void ShowContinueContent()
        {
            continueLabel.text = settings.continueText;
            continueLabel.style.display = DisplayStyle.Flex;
            continueLabel.RegisterCallback<TransitionEndEvent>(ToggleContinueLabelClass);
            loadingContent.AddToClassList("fadeOut");

            // Nullify spinner action
            if (spinnerAction != null)
            {
                spinnerAction.Pause();
                spinnerAction = null;
            }

            InputSystem.onAnyButtonPress.CallOnce(x => Close());
        }

        /// <summary>
        /// Updates the spinner icon rotation.
        /// </summary>
        /// <param name="changeValue">The amount of rotation to add.</param>
        protected virtual void UpdateSpinner(float changeValue)
        {
            spinnerIcon.style.rotate = new Rotate(new Angle(spinnerIcon.style.rotate.value.angle.value + changeValue));
        }

        /// <summary>
        /// Updates the loading bar based on the completed processes.
        /// </summary>
        protected virtual void UpdateLoadingBar()
        {
            // Make the loader bar length represent the progress value
            loadingBar.style.width = new Length(Progress, LengthUnit.Percent);

            if (processes != null && processes.Length > 0)
            {
                string percentText = !settings.showPercentage ? string.Empty : $" {Progress:0.##}%";
                loadingLabel.text = $"{processes[currentProcessIndex].displayText}{percentText}";
            }
            else
            {
                loadingLabel.text = settings.defaultLoadingText;
            }
        }

        /// <summary>
        /// Handles the background slideshow.
        /// </summary>
        /// <returns>Yields for a delay.</returns>
        protected IEnumerator BackgroundSlideshowLoopCoroutine()
        {
            currentBackgroundIndex++;

            if (currentBackgroundIndex >= settings.backgrounds.Count)
            {
                currentBackgroundIndex = 0;
            }

            yield return new WaitForSeconds(0.5f);

            if (backgroundOne.enabledSelf)
            {
                backgroundTwo.style.backgroundImage = settings.backgrounds[currentBackgroundIndex];
            }
            else
            {
                backgroundOne.style.backgroundImage = settings.backgrounds[currentBackgroundIndex];
            }

            yield return new WaitForSeconds(settings.backgroundDisplayLength);

            backgroundOne.SetEnabled(!backgroundOne.enabledSelf);

            if (IsOpen)
            {
                StartCoroutine(BackgroundSlideshowLoopCoroutine());
            }
        }

        /// <summary>
        /// Begins the background slideshow.
        /// </summary>
        protected virtual void BeginBackgroundSlideshow()
        {
            if (settings.backgrounds.Count == 0)
            {
                return;
            }

            currentBackgroundIndex = 0;

            backgroundOne.SetEnabled(true);
            backgroundOne.style.backgroundImage = settings.backgrounds[currentBackgroundIndex];

            if (settings.backgrounds.Count > 1)
            {
                StartCoroutine(BackgroundSlideshowLoopCoroutine());
            }
        }

        /// <summary>
        /// Handles the background slideshow.
        /// </summary>
        /// <returns>Yields for a delay.</returns>
        protected IEnumerator TipSlideshowLoopCoroutine()
        {
            if (tipLabel.ClassListContains(settings.tipAnimatorPseudoClassName))
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(settings.tipDisplayLength);
            }
  
            if (IsOpen)
            {
                ToggleTipClassAndDisplayNextTip();
                StartCoroutine(TipSlideshowLoopCoroutine());
            }
        }

        /// <summary>
        /// Begins the background slideshow.
        /// </summary>
        protected virtual void BeginTipSlideshow()
        {
            if (settings.tips.Count == 0)
            {
                return;
            }

            if (settings.tips.Count > 1)
            {
                StartCoroutine(TipSlideshowLoopCoroutine());
            }
            else
            {
                tipLabel.EnableInClassList(settings.tipAnimatorPseudoClassName, false);
                tipLabel.text = settings.tips[0].ToString();
            }
        }

        /// <summary>
        /// Loads a scene. You can use loading processes to track other things that need to be loaded.
        /// </summary>
        /// <param name="buildIndex">The build index of the scene.</param>
        /// <param name="processes">A list of loading processes other than the scene.</param>
        public virtual void Load(int buildIndex = -1, params LoadingProgressTracker[] processes)
        {
            var sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(buildIndex));
            Load(sceneName, processes);
        }

        /// <summary>
        /// Loads a scene. You can use loading processes to track other things that need to be loaded.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="processes">A list of loading processes other than the scene.</param>
        public virtual void Load(string sceneName = "", params LoadingProgressTracker[] processes)
        {
            if (IsLoading)
            {
                Debug.LogWarning("Freeloader: cannot load a scene while loading is ongoing.");
                return;
            }
            else if (string.IsNullOrEmpty(sceneName) && processes.Length == 0)
            {
                Debug.LogWarning("Freeloader: nothing to load.");
                return;
            }

            Open();
            StartCoroutine(LoadingCoroutine(sceneName, processes));
        }

        /// <summary>
        /// Handles the loading process.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="processes">A list of loading processes other than the scene.</param>
        /// <returns>Yields until all processes are complete.</returns>
        protected IEnumerator LoadingCoroutine(string sceneName, params LoadingProgressTracker[] processes)
        {
            // Wait for loading screen animation
            yield return new WaitForSeconds(0.5f);

            IsLoading = true;
            onStart.Invoke();

            // Add loading scene progress tracker
            if (!string.IsNullOrEmpty(sceneName))
            {
                this.processes = new LoadingProgressTracker[processes.Length + 1];
                var operation = SceneManager.LoadSceneAsync(sceneName);

                this.processes[0] = new LoadingProgressTracker(settings.defaultLoadingText, () => operation.progress * 100f);

                for (int i = 0; i < processes.Length; i++)
                {
                    this.processes[i + 1] = processes[i];
                }
            }
            else
            {
                this.processes = processes;
            }

            // Wait for all processes to complete
            for (int i = 0; i < this.processes.Length; i++)
            {
                currentProcessIndex = i;
                var process = this.processes[i];
                float previousProgress = 0;

                var waitForProcess = new WaitUntil(() =>
                {
                    Progress = (process.Progress + (100 * i)) / (100 * this.processes.Length) * 100;

                    if (Progress != previousProgress)
                    {
                        previousProgress = Progress;
                        onProgressChanged.Invoke(Progress);
                        UpdateLoadingBar();
                    }

                    return process.Progress >= 100f;
                });

                yield return waitForProcess;
            }

            IsLoading = false;
            onComplete.Invoke();

            // End loading
            if (settings.requireInputToContinue)
            {
                ShowContinueContent();
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Opens the loading screen.
        /// </summary>
        public virtual void Open()
        {
            // Register events
            loadingLabel.RegisterCallback<TransitionEndEvent>(ToggleLoadingLabelClass);

            if (settings.enableBackgroundZoom)
            {
                backgroundOne.RegisterCallback<TransitionEndEvent>(ToggleBackgroundOneScaleClass);
                backgroundTwo.RegisterCallback<TransitionEndEvent>(ToggleBackgroundTwoScaleClass);
            }
            else
            {
                backgroundOne.EnableInClassList(settings.backgroundScaleAnimatorPseudoClassName, false);
            }

            // Enable and start
            root.SetEnabled(true);
            BeginBackgroundSlideshow();
            BeginTipSlideshow();
            UpdateLoadingBar();

            // Start animtions
            if (settings.showSpinner)
            {
                float speed = -12 * settings.spinnerSpeed;
                spinnerAction = spinnerIcon.schedule.Execute(() => UpdateSpinner(speed)).Every(17);
            }

            ToggleLoadingLabelClass(null);

            if (loadingContent.ClassListContains("fadeOut"))
            {
                loadingContent.RemoveFromClassList("fadeOut");
            }
        }

        /// <summary>
        /// Closes the loading screen.
        /// </summary>
        public virtual void Close()
        {
            // Unregister events
            loadingLabel.UnregisterCallback<TransitionEndEvent>(ToggleLoadingLabelClass);
            continueLabel.UnregisterCallback<TransitionEndEvent>(ToggleContinueLabelClass);
            backgroundOne.UnregisterCallback<TransitionEndEvent>(ToggleBackgroundOneScaleClass);
            backgroundTwo.UnregisterCallback<TransitionEndEvent>(ToggleBackgroundTwoScaleClass);

            // Nullify spinner action
            if (spinnerAction != null)
            {
                spinnerAction.Pause();
                spinnerAction = null;
            }

            // Disable
            continueLabel.style.display = DisplayStyle.None;
            root.SetEnabled(false);
        }
    }
}