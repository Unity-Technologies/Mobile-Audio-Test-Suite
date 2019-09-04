using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;


public class RenameAudioFiles : MonoBehaviour {

   // [MenuItem("AudioTools/Rename Audio Files")]
    static void RenameFiles()
    {
        //string path = "Assets/AudioFiles";
        //RecursiveRenaming(path);
        Debug.Log("Obsolete Function");
    }

    static void RecursiveRenaming(string path)
    {
        /*DirectoryInfo dir = new DirectoryInfo(path);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (dirs.Length > 0)
        {
            for (int i = 0; i < dirs.Length; i++)
            {
                RecursiveRenaming(path + "/" + dirs[i].Name);
            }
        }
        else
        {
            FileInfo[] info = dir.GetFiles();
            int counter = 0;
            foreach (FileInfo f in info)
            {

                if (!f.Name.Contains(".meta"))
                {
                    if (!f.Name.Contains("CompressedInMemory") && !f.Name.Contains("DecompressOnLoad") && !f.Name.Contains("Streaming"))
                    {
                        string currentPath = (dir.FullName + "/" + f.Name);
                        string extension = f.FullName.Substring(f.FullName.Length-4);
                        //Debug.Log(extension);
                        AudioClip ac = (AudioClip)AssetDatabase.LoadAssetAtPath(path + "/" + f.Name, typeof(AudioClip));
                        string[] withoutNumber = f.Name.Split(' ');
                        string newPath = "";
                        if (ac.loadType == AudioClipLoadType.CompressedInMemory)
                        {
                            
                            if (withoutNumber.Length > 1)
                            {

                                newPath = path + "/" + withoutNumber[0] + "_CompressedInMemory" + extension;
                            }
                            else
                            {
                                newPath = path + "/" + f.Name.Substring(0,f.Name.Length-4) + "_CompressedInMemory" + extension;
                            }
                        }
                        if (ac.loadType == AudioClipLoadType.DecompressOnLoad)
                        {
                            if (withoutNumber.Length > 1)
                            {

                                newPath = path + "/" + withoutNumber[0] + "_DecompressOnLoad" + extension;
                            }
                            else
                            {
                                newPath = path + "/" + f.Name.Substring(0, f.Name.Length - 4) + "_DecompressOnLoad" + extension;
                            }
                        }
                        if (ac.loadType == AudioClipLoadType.Streaming)
                        {
                            if (withoutNumber.Length > 1)
                            {

                                newPath = path + "/" + withoutNumber[0] + "_Streaming" + extension;
                            }
                            else
                            {
                                newPath = path + "/" + f.Name.Substring(0, f.Name.Length - 4) + "_Streaming" + extension;
                            }
                        }
                        File.Move(f.FullName, newPath);

                        //Debug.Log(f.Name);
                    }
                }
            }
        }*/
    }

}
