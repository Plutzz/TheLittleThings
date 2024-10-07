using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;

public class SaveManager : Singleton<SaveManager>
{
    public SaveState CurrentSave;
    private FileManager m_fileManager;
    private string SavePath = "C:\\Games\\TheLittleThings\\Saves";

    private void Start()
    {
        m_fileManager = new FileManager(SavePath);
        string[] saves = m_fileManager.GetSaveNames();

        if (saves.Length == 0)
        {
            string saveName = "test" + ".json"; // prompt player for save name later™
            NewGame(saveName);
        }
        else
        {
            LoadGame(saves[0]); // prompt player for save name later™
        }
    }
    public void NewGame(string name)
    {
        CurrentSave = new SaveState(name);

        SaveGame();
    }

    public void LoadGame(string name)
    {
        CurrentSave = m_fileManager.Load(name);
        Debug.Log(CurrentSave.hp);
    }

    public void SaveGame()
    {
        m_fileManager.Save(CurrentSave);
    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
    }
}