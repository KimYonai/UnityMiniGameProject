using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    [SerializeField] RawImage loadingImage;
    [SerializeField] Slider loadingBar;
    [SerializeField] TextMeshProUGUI pressKeyText;
    [SerializeField] AudioSource BGM;

    public void OnClickPlayButton(string sceneName)
    {
        if (loadingRoutine != null)
            return;
        
        loadingRoutine = StartCoroutine(LoadingRoutine(sceneName));
        
        BGM.gameObject.SetActive(false);
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

    public void OnClickSettingButton()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void GameQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
