using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField] RawImage loadingImage;
    [SerializeField] Slider loadingBar;
    [SerializeField] TextMeshProUGUI pressKeyText;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }

    private void Update()
    {
        
    }

    public void OnClickPlayButton(string sceneName)
    {
        if (loadingRoutine != null)
            return;

        loadingRoutine = StartCoroutine(LoadingRoutine(sceneName));
    }

    Coroutine loadingRoutine;
    IEnumerator LoadingRoutine(string sceneName)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneName);

        oper.allowSceneActivation = false;
        loadingImage.gameObject.SetActive(true);

        while (oper.isDone == false)
        {
            if (oper.progress < 0.9f)
            {
                loadingBar.value = oper.progress;
            }
            else
            {
                break;
            }
            yield return null;
        }

        float time = 0f;

        while (time < 5f)
        {
            time += Time.deltaTime;
            loadingBar.value = time / 5f;
            yield return null;
        }

        while (Input.anyKeyDown == false)
        {
            pressKeyText.gameObject.SetActive(true);
            yield return null;
        }

        oper.allowSceneActivation = true;
        pressKeyText.gameObject.SetActive(false);
        loadingImage.gameObject.SetActive(false);
    }
}
