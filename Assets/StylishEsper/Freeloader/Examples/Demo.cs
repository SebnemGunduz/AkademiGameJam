//***************************************************************************************
// Writer: Stylish Esper
//***************************************************************************************

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esper.Freeloader.Examples
{
    public class Demo : MonoBehaviour
    {
        private static float progress;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            progress = 0;
            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            LoadingScreen.Instance.GetComponent<UIDocument>().enabled = true;

            yield return new WaitForSeconds(2f);

            if (!LoadingScreen.Instance.IsLoading)
            {
                var process = new LoadingProgressTracker("Loading...", () => progress);
                LoadingScreen.Instance.Load("SceneToLoad", process);
            }

            while (progress < 100)
            {
                progress += 10;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}