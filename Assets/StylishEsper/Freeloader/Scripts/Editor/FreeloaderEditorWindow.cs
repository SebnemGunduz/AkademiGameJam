//***************************************************************************************
// Writer: Stylish Esper
//***************************************************************************************

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esper.Freeloader.Editor
{
    public class FreeloaderEditorWindow : EditorWindow
    {
        private VisualElement settingsElement;
        private Label settingsTitleLabel;
        private Toggle hideBarToggle;
        private ListView backgroundList;
        private Toggle showTipsToggle;
        private ListView tipsList;
        private Toggle requireInputToggle;
        private TextField defaultLoadingTextField;
        private TextField continueTextField;
        private FloatField backgroundDisplayLengthField;
        private Toggle enableBackgroundZoomToggle;
        private FloatField tipDisplayLengthField;
        private TextField tipsTitleField;
        private Toggle showPercentageToggle;
        private Toggle showSpinnerToggle;
        private FloatField spinnerSpeedField;
        private ObjectField spinnerIconField;
        private Button documentationButton;

        private LoadingScreenSettings settings;

        [MenuItem("Window/Freeloader")]
        public static void Open()
        {
            FreeloaderEditorWindow wnd = GetWindow<FreeloaderEditorWindow>();
            wnd.titleContent = new GUIContent("Freeloader");
        }

        public void CreateGUI()
        {
            minSize = new Vector2(750, 500);

            VisualElement root = rootVisualElement;
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StylishEsper/Freeloader/Scripts/Editor/FreeloaderEditorWindow.uxml");
            visualTree.CloneTree(root);

            var simpleSettings = AssetDatabase.LoadAssetAtPath<LoadingScreenSettings>("Assets/StylishEsper/Freeloader/LoadingScreens/Simple/Settings.asset");
            var settingsButtonSimple = root.Q<Button>("SettingsButtonSimple");
            settingsButtonSimple.clicked += () => OpenSettings(simpleSettings);

            var cuteSettings = AssetDatabase.LoadAssetAtPath<LoadingScreenSettings>("Assets/StylishEsper/Freeloader/LoadingScreens/Cute/Settings.asset");
            var settingsButtonCute = root.Q<Button>("SettingsButtonCute");
            settingsButtonCute.clicked += () => OpenSettings(cuteSettings);

            var horrorSettings = AssetDatabase.LoadAssetAtPath<LoadingScreenSettings>("Assets/StylishEsper/Freeloader/LoadingScreens/Horror/Settings.asset");
            var settingsButtonHorror = root.Q<Button>("SettingsButtonHorror");
            settingsButtonHorror.clicked += () => OpenSettings(horrorSettings);

            var pixelSettings = AssetDatabase.LoadAssetAtPath<LoadingScreenSettings>("Assets/StylishEsper/Freeloader/LoadingScreens/Pixel/Settings.asset");
            var settingsButtonPixel = root.Q<Button>("SettingsButtonPixel");
            settingsButtonPixel.clicked += () => OpenSettings(pixelSettings);

            var sciFiSettings = AssetDatabase.LoadAssetAtPath<LoadingScreenSettings>("Assets/StylishEsper/Freeloader/LoadingScreens/Sci-Fi/Settings.asset");
            var settingsButtonSciFi = root.Q<Button>("SettingsButtonSciFi");
            settingsButtonSciFi.clicked += () => OpenSettings(sciFiSettings);

            var closeSettingsButton = root.Q<Button>("CloseSettingsButton");
            closeSettingsButton.clicked += CloseSettings;

            var simpleButton = root.Q<Button>("SimpleButton");
            simpleButton.clicked += () =>
            {
                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<LoadingScreen>("Assets/StylishEsper/Freeloader/LoadingScreens/Simple/LoadingScreen.prefab"));
                Close();
            };

            var cuteButton = root.Q<Button>("CuteButton");
            cuteButton.clicked += () =>
            {
                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<LoadingScreen>("Assets/StylishEsper/Freeloader/LoadingScreens/Cute/LoadingScreen.prefab"));
                Close();
            };

            var pixelButton = root.Q<Button>("PixelButton");
            pixelButton.clicked += () =>
            {
                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<LoadingScreen>("Assets/StylishEsper/Freeloader/LoadingScreens/Pixel/LoadingScreen.prefab"));
                Close();
            };

            var scifiButton = root.Q<Button>("SciFiButton");
            scifiButton.clicked += () =>
            {
                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<LoadingScreen>("Assets/StylishEsper/Freeloader/LoadingScreens/Sci-Fi/LoadingScreen.prefab"));
                Close();
            };

            var horrorButton = root.Q<Button>("HorrorButton");
            horrorButton.clicked += () =>
            {
                PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<LoadingScreen>("Assets/StylishEsper/Freeloader/LoadingScreens/Horror/LoadingScreen.prefab"));
                Close();
            };

            var documentationButton = root.Q<Button>("DocumentationButton");
            documentationButton.clicked += () =>
            {
                Application.OpenURL("https://stylishesper.gitbook.io/freeloader/");
            };

            settingsElement = root.Q<VisualElement>("Settings");
            settingsTitleLabel = root.Q<Label>("SettingsTitleLabel");
            hideBarToggle = root.Q<Toggle>("HideBarToggle");
            backgroundList = root.Q<ListView>("BackgroundList");
            showTipsToggle = root.Q<Toggle>("ShowTipsToggle");
            tipsList = root.Q<ListView>("TipsList");
            requireInputToggle = root.Q<Toggle>("RequireInputToggle");
            defaultLoadingTextField = root.Q<TextField>("DefaultLoadingTextField");
            continueTextField = root.Q<TextField>("ContinueTextField");
            backgroundDisplayLengthField = root.Q<FloatField>("BackgroundDisplayLengthField");
            enableBackgroundZoomToggle = root.Q<Toggle>("EnableBackgroundZoomToggle");
            tipDisplayLengthField = root.Q<FloatField>("TipDisplayLengthField");
            tipsTitleField = root.Q<TextField>("TipsTitleField");
            showPercentageToggle = root.Q<Toggle>("ShowPercentageToggle");
            showSpinnerToggle = root.Q<Toggle>("ShowSpinnerToggle");
            spinnerSpeedField = root.Q<FloatField>("SpinnerSpeedField");
            spinnerIconField = root.Q<ObjectField>("SpinnerIconField");

            showTipsToggle.RegisterValueChangedCallback(x =>
            {
                UpdateSettingElements(0);
            });

            requireInputToggle.RegisterValueChangedCallback(x =>
            {
                UpdateSettingElements(0);
            });

            showSpinnerToggle.RegisterValueChangedCallback(x =>
            {
                UpdateSettingElements(0);
            });

            backgroundList.itemsAdded += (x) => UpdateSettingElements(10);
            backgroundList.itemsRemoved += (x) => UpdateSettingElements(10);
            tipsList.itemsAdded += (x) => UpdateSettingElements(10);
            tipsList.itemsRemoved += (x) => UpdateSettingElements(10);

            settingsElement.style.display = DisplayStyle.None;
            CloseSettings();
        }

        private void OpenSettings(LoadingScreenSettings loadingScreenSettings)
        {
            settings = loadingScreenSettings;
            var serializedObject = new SerializedObject(loadingScreenSettings);
            settingsTitleLabel.text = loadingScreenSettings.settingsName;
            hideBarToggle.BindProperty(serializedObject.FindProperty("hideBar"));
            showTipsToggle.BindProperty(serializedObject.FindProperty("showTips"));
            backgroundList.BindProperty(serializedObject.FindProperty("backgrounds"));
            tipsList.BindProperty(serializedObject.FindProperty("tips"));
            requireInputToggle.BindProperty(serializedObject.FindProperty("requireInputToContinue"));
            defaultLoadingTextField.BindProperty(serializedObject.FindProperty("defaultLoadingText"));
            continueTextField.BindProperty(serializedObject.FindProperty("continueText"));
            backgroundDisplayLengthField.BindProperty(serializedObject.FindProperty("backgroundDisplayLength"));
            enableBackgroundZoomToggle.BindProperty(serializedObject.FindProperty("enableBackgroundZoom"));
            tipsTitleField.BindProperty(serializedObject.FindProperty("tipsTitle"));
            tipDisplayLengthField.BindProperty(serializedObject.FindProperty("tipDisplayLength"));
            showPercentageToggle.BindProperty(serializedObject.FindProperty("showPercentage"));
            showSpinnerToggle.BindProperty(serializedObject.FindProperty("showSpinner"));
            spinnerSpeedField.BindProperty(serializedObject.FindProperty("spinnerSpeed"));
            spinnerIconField.BindProperty(serializedObject.FindProperty("spinnerIcon"));

            settingsElement.style.display = DisplayStyle.Flex;
            settingsElement.SetEnabled(true);

            UpdateSettingElements(0);
        }

        private void UpdateSettingElements(long delay)
        {
            rootVisualElement.schedule.Execute(() =>
            {
                if (settings)
                {
                    if (settings.showTips)
                    {
                        tipsList.style.display = DisplayStyle.Flex;
                        tipsTitleField.style.display = DisplayStyle.Flex;
                        tipDisplayLengthField.style.display = settings.tips.Count > 1 ? DisplayStyle.Flex : DisplayStyle.None;
                    }
                    else
                    {
                        tipsList.style.display = DisplayStyle.None;
                        tipsTitleField.style.display = DisplayStyle.None;
                        tipDisplayLengthField.style.display = DisplayStyle.None;
                    }

                    continueTextField.style.display = settings.requireInputToContinue ? DisplayStyle.Flex : DisplayStyle.None;
                    backgroundDisplayLengthField.style.display = settings.backgrounds.Count > 1 ? DisplayStyle.Flex : DisplayStyle.None;

                    if (settings.showSpinner)
                    {
                        spinnerSpeedField.style.display = DisplayStyle.Flex;
                        spinnerIconField.style.display = DisplayStyle.Flex;
                    }
                    else
                    {
                        spinnerSpeedField.style.display = DisplayStyle.None;
                        spinnerIconField.style.display = DisplayStyle.None;

                    }
                }
            }).StartingIn(delay);
        }

        private void CloseSettings()
        {
            settingsElement.SetEnabled(false);
        }
    }
}
#endif