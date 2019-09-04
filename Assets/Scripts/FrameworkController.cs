using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using System.Linq;

public class FrameworkController : MonoBehaviour
{
    [SerializeField]
    GameObject PanelPrefab;
    [SerializeField]
    List<TestController> Tests;
    [SerializeField]
    RectTransform ScrollviewContent;
    [SerializeField]
    Text chosenAudioName;
    [SerializeField]
    GameObject finishDialogue;
    [SerializeField]
    Text finishDialogueText;

    bool updateMusicName = true;
#pragma warning disable 0414
    private int currentIndex = 0;
#pragma warning restore 0414
    // Use this for initialization
    void Start()
    {
        Tests = new List<TestController>();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        int count = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < count; i++)
        {
            int x = i;
            string pathToScene = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
            if (!sceneName.Contains("MainMenu") && !sceneName.Contains("Summary"))
            {
                GameObject go = (GameObject)Instantiate(PanelPrefab, ScrollviewContent);
                go.GetComponentInChildren<Text>().text = (i-1) + "." + sceneName;
                go.GetComponent<Button>().onClick.AddListener(() => LoadSceneAt(x));
                if (sceneName == SceneManager.GetActiveScene().name)
                {
                    go.GetComponent<Image>().color = new Color(40f / 255, 40f / 255, 40f / 255, 100f / 255);
                }
                TestController temp = (TestController)ScriptableObject.CreateInstance(typeof(TestController));
                temp.name = sceneName + "_object";
                temp.sceneName = sceneName;
                temp.UIObject = go;
                temp.status = 0;
                temp.selected = false;
                Tests.Add(temp);
                //Destroy(temp);
            }
        }
    }
    
    void Update()
    {
        if (updateMusicName)
        UpdateChosenAudio();
    }

    public void LoadSceneAt(int index)
    {                                                           
        currentIndex = index - 2;                           // Since we're getting the index in "Build settings" scope, 
        SceneManager.LoadScene(index, LoadSceneMode.Single);// Just remove 2 to convert it to "Tests" scope which just doesn't have "MainMenu" and "Summmary" scenes in it
    }

    private void UpdateChosenTest(string sceneName)
    {
        for (int i = 0; i < Tests.Count; i++)
        {
            if (Tests[i].sceneName == sceneName)
            {
                Tests[i].SelectThis();
                currentIndex = i;
            }
            else
            {
                Tests[i].DeselectThis();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateChosenTest(scene.name);
    }

    public void PassTest()
    {
        for (int i = 0; i < Tests.Count; i++)
        {
            if (Tests[i].selected)
                Tests[i].PassThis();
        }
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            LoadSceneAt(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FailTest()
    {
        for (int i = 0; i < Tests.Count; i++)
        {
            if (Tests[i].selected)
                Tests[i].FailThis();
        }
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            LoadSceneAt(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NextTest()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            LoadSceneAt(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PreviousTest()
    {
        if (SceneManager.GetActiveScene().buildIndex - 2 > 0)
            LoadSceneAt(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void UpdateChosenAudio()
    {
        GameObject sourceObject = GameObject.FindGameObjectWithTag("MainSource");
        if (sourceObject != null)
        {
            AudioSource source = sourceObject.GetComponent<AudioSource>();
            if (source == null)
            {
                Debug.Log("Source not found. Make sure Main audio source is tagged as 'MainSource'");
                return;
            }
            else
            {
                if (source.clip != null)
                    chosenAudioName.text = source.clip.name;
                else
                    chosenAudioName.text = "No clip selected";
            }
        }
        else Debug.Log("Source not found. Make sure Main audio source is tagged as 'MainSource'");
    }

    public void FinishClick()
    {
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            source.Stop();
        }
        int unfinishedCount = 0;
        foreach (TestController test in Tests)
        {
            if (test.status == 0)
                unfinishedCount++;
        }

        if (unfinishedCount == 0)
            FinishYes();
        else
        {
            if (unfinishedCount > 1)
                finishDialogueText.text = "You have " + unfinishedCount + " unfinished tests. \n Do you still want to finish?";
            else finishDialogueText.text = "You have 1 unfinished test. \n Do you still want to finish?";
            ShowFinishDialogue();
        }
    }

    public void FinishYes()
    {
        StartCoroutine(TestSummary());
    }

    public void FinishNo()
    {
        finishDialogue.SetActive(false);
    }

    void ShowFinishDialogue()
    {
        finishDialogue.SetActive(true);
    }

    IEnumerator TestSummary ()
    {
        SceneManager.LoadScene("Summary", LoadSceneMode.Single);
        updateMusicName = false;
        yield return new WaitForSeconds(0.1f);  //Wait till scene finishes loading
        SummaryController sc = FindObjectOfType<SummaryController>();
        if (sc != null)
        {
            sc.PopulateScrollView(Tests);
            finishDialogue.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Summary Controller script not found");
        }
    }
}
