using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Level currentLevel;
    private Vector3 playerStartPos;
    public Vector3 PlayerStartPos { get => playerStartPos; set => playerStartPos = value; }

    private static LevelManager instance;

    private Player playerControl;

    private void Awake()
    {

        instance = this;

    }

    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
        SceneManager.LoadScene("SampleScene");
    }


    private void Start()
    {
        playerControl = Player.Instance;
        CreateLevel();

        
        GameObject level = Instantiate(currentLevel.PrefabLevel, Vector3.zero, Quaternion.identity);

        playerStartPos = level.transform.GetChild(0).transform.localPosition;

        print(currentLevel.name);
    }
    public static LevelManager Instance
    {
        get
        {
            return instance;
        }
    }
    void CreateLevel()
    {
        currentLevel = Resources.Load<Level>("Levels/Level" + PlayerPrefs.GetInt("Level", 1));
        print(currentLevel);
        if (currentLevel == null)
        {
            PlayerPrefs.SetInt("Level", 1);
            CreateLevel();

        }
    }

}
