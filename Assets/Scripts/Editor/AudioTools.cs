using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;


public class AudioTools : MonoBehaviour
{
    
    [MenuItem("AudioTools/Add Tests To Build Settings")]
    static void AddTests()
    {
        List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
        EditorBuildSettingsScene mainMenuScene, summaryScene;
        mainMenuScene = new EditorBuildSettingsScene("Assets/MainMenu.unity", true);
        summaryScene = new EditorBuildSettingsScene("Assets/Summary.unity", true);
        RecursiveAdding("Assets/Tests", editorBuildSettingsScenes);
        // Grab folder names of the tests to sort by alphanumerically and put all the sorted values to a new list to pass to Build settings.
        List<string> ScenePathStrings = new List<string>();
        for (int i = 0; i < editorBuildSettingsScenes.Count; i++)
        {
            if (editorBuildSettingsScenes[i].path != "" && editorBuildSettingsScenes[i].path != null)
            ScenePathStrings.Add(editorBuildSettingsScenes[i].path.Split('/')[2]);
        }

        string[] ScenePathStringsArray = ScenePathStrings.ToArray();
        System.Array.Sort(ScenePathStringsArray, new AlphanumComparatorFast());

        List<EditorBuildSettingsScene> editorBuildSettingsScenesSorted = new List<EditorBuildSettingsScene>();
        for (int i = 0; i < ScenePathStringsArray.Length; i++)
        {
            string target = ScenePathStringsArray[i];
            for (int j = 0; j < editorBuildSettingsScenes.Count; j++)
            {
                if (editorBuildSettingsScenes[j].path.Contains(target))
                {
                    editorBuildSettingsScenesSorted.Add(editorBuildSettingsScenes[j]);
                    break;
                }
            }
        }
        editorBuildSettingsScenesSorted.Insert(0,mainMenuScene);
        editorBuildSettingsScenesSorted.Insert(1,summaryScene);
        EditorBuildSettings.scenes = editorBuildSettingsScenesSorted.ToArray();
        Debug.Log(editorBuildSettingsScenes.Count + " Scenes added");
    }

    [MenuItem("AudioTools/Add audio files to Audio Source list")]
    static void AddAudioClips()
    {
        FileImporter_Controller controller = FindObjectOfType<FileImporter_Controller>();
        List<FileImporter_Controller.AudioWithFormat>audioClips = new List<FileImporter_Controller.AudioWithFormat>();
        RecursiveAdding("Assets/AudioFiles/FileType_Test", audioClips);
        if (controller != null)
        {
            controller.audioFiles = audioClips;
        }
        else
        {
            Debug.Log("Framework Controller not found");
        }

    }

    [MenuItem("AudioTools/Add Compression audio files to Audio Source list")]
    static void AddAudioClipsCompression()
    {
        FileCompression_Controller controller = FindObjectOfType<FileCompression_Controller>();
        List<FileCompression_Controller.AudioWithFormat> audioClips = new List<FileCompression_Controller.AudioWithFormat>();
        RecursiveAdding("Assets/AudioFiles/Compression_Test", audioClips);
        if (controller != null)
        {
            controller.audioFiles = audioClips;
        }
        else
        {
            Debug.Log("Framework Controller not found");
        }

    }

    static void RecursiveAdding(string path, List<EditorBuildSettingsScene> list)
    {

        DirectoryInfo dir = new DirectoryInfo(path);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (dirs.Length > 0)
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                RecursiveAdding(path + "/" + dirs[i].Name, list);
            }

            FileInfo[] info = dir.GetFiles();
            foreach (FileInfo f in info)
            {
                if (f.Name.Contains(".unity") && !f.Name.Contains(".meta"))
                {
                    list.Add(new EditorBuildSettingsScene(path + "/" + f.Name, true));
                    //Debug.Log("Scene found:" + f.Name);
                }
            }
        }
        else
        {
            FileInfo[] info = dir.GetFiles();
            foreach (FileInfo f in info)
            {
                if (f.Name.Contains(".unity") && !f.Name.Contains(".meta"))
                {
                    list.Add(new EditorBuildSettingsScene(path + "/" + f.Name, true));
                    //Debug.Log("Scene found:" + f.Name);
                }
            }
        }
    }

    static void RecursiveAdding(string path, List<FileImporter_Controller.AudioWithFormat> list)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (dirs.Length > 0)
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                RecursiveAdding(path + "/" + dirs[i].Name, list);
            }

        }
        else
        {
            FileInfo[] info = dir.GetFiles();
            foreach (FileInfo f in info)
            {
                if (!f.Name.Contains(".meta"))
                {
                    //Debug.Log(path + "/" + f.Name);
                    AudioClip ac = (AudioClip)AssetDatabase.LoadAssetAtPath(path +"/"+ f.Name, typeof(AudioClip));
                    FileImporter_Controller.AudioWithFormat temp = new FileImporter_Controller.AudioWithFormat();
                    temp.SetAudio(ac);
                    temp.SetFormat(f.Extension);
                    list.Add(temp);
                }
            }
        }
    }

    static void RecursiveAdding(string path, List<FileCompression_Controller.AudioWithFormat> list)
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (dirs.Length > 0)
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                RecursiveAdding(path + "/" + dirs[i].Name, list);
            }

        }
        else
        {
            FileInfo[] info = dir.GetFiles();
            foreach (FileInfo f in info)
            {
                if (!f.Name.Contains(".meta"))
                {
                    //Debug.Log(path + "/" + f.Name);
                    AudioClip ac = (AudioClip)AssetDatabase.LoadAssetAtPath(path + "/" + f.Name, typeof(AudioClip));
                    FileCompression_Controller.AudioWithFormat temp = new FileCompression_Controller.AudioWithFormat();
                    temp.SetAudio(ac);
                    temp.SetFormat(f.Extension);
                    list.Add(temp);
                }
            }
        }
    }
}
