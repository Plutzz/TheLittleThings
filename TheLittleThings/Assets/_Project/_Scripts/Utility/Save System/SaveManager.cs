using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class SaveManager : Singleton<SaveManager>
{
    private SaveState m_currentSave;
    private FileManager m_fileManager;

    private void Start()
    {
        LoadGame();
    }
    public void NewGame()
    {
        m_currentSave = new SaveState();
    }

    public void LoadGame()
    {
        if (m_currentSave == null)
        {
            NewGame();
        }
    }

    public void SaveGame()
    {

    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
    }
}
public interface IDataPersistent
{
    public void LoadData();
    public void SaveData();
}