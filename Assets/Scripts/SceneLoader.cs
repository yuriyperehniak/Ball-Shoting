using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    private void Start()
    {
        gameObject.GetComponent<Button>()?.onClick.AddListener(() => UploadScene(sceneName));
    }

    private static void UploadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}