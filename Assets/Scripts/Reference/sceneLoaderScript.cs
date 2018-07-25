using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneLoaderScript : MonoBehaviour {

    public Text loadingText;

    IEnumerator Start()
    {
        
        AsyncOperation async = SceneManager.LoadSceneAsync("grouping");
        StartCoroutine(loopText());
        yield return async;
        Debug.Log("Loading Complete!");
    }

    IEnumerator loopText()
    {
        while (true)
        {
            loadingText.text = "Loading...";

            if(loadingText.text == "Loading...")
            {
                loadingText.text = "Loading";
            }
            if (loadingText.text == "Loading")
            {
                loadingText.text = "Loading.";
            }
            if (loadingText.text == "Loading.")
            {
                loadingText.text = "Loading..";
            }
            if (loadingText.text == "Loading..")
            {
                loadingText.text = "Loading...";
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
