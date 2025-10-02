using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pulsante Carica Partita")]
    [SerializeField] private Button firstSelectedButton;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float sceneChangeDelay = 0.3f;
    public void OnStartButton()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitButton()
    {
        StartCoroutine(PlayClickAndQuit());
    }

    //private void PlayClickAndLoadScene(string sceneName, bool deleteSave)
    //{
    //    if (clickSound != null && audioSource != null)
    //    {
    //        audioSource.PlayOneShot(clickSound);
    //    }

    //    StartCoroutine(DelayedSceneLoad(sceneName, deleteSave));
    //}

    //private IEnumerator DelayedSceneLoad(string sceneName, bool deleteSave)
    //{
    //    yield return new WaitForSeconds(sceneChangeDelay);

    //    SceneLoader.LoadScene(sceneName);
    //}

    private IEnumerator PlayClickAndQuit()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        yield return new WaitForSeconds(sceneChangeDelay);

        Application.Quit();
    }


}
