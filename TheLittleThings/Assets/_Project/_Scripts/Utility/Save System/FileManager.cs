using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
    private string SaveDirPath;

    public FileManager(string saveDirPath)
    {
        SaveDirPath = saveDirPath;
    }
    public void Save(SaveState s)
    {
        string path = Path.Combine(SaveDirPath, s.SaveName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            string dataToStore = JsonUtility.ToJson(s, true);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public SaveState Load(string name)
    {
        string path = Path.Combine(SaveDirPath, name);

        if (File.Exists(path))
        {
            try
            {
                string data = "";

                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data = reader.ReadToEnd();
                    }
                }

                return (SaveState)JsonUtility.FromJson(data, typeof(SaveState));
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        return null;
    }

    public string[] GetSaveNames()
    {
        return Directory.GetFiles(SaveDirPath);
    }
}