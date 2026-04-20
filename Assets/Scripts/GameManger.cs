using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    //单例
    public static GameManger Instance { get; private set; }

    [Header("面板")]
    public Button pauseButton;
    public GameObject pausePanel;
    public GameObject helpPanel;
    public GameObject canvas;
    public GameObject titlePanel;
    public GameObject gameOver;
    public GameObject gameWin;
    [Header("上方数值")]
    public int score;
    public int health;
    public Text scoreText;
    public Text healthText;
    public Text levelText; //关卡名字
    public Text nameText; //制作名字
    [Header("结算面板文字")]
    public Text scoreEndText;
    public Text scoreEndWinText;
    public Text healthEndText;

    [Header("音效")]
    public AudioSource audioSource;
    public AudioClip rightAudioClip;
    public AudioClip hurtAudioClip;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // --- 新增：订阅场景加载事件 ---
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 当场景加载时，Unity 会自动调用这个方法
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 这里调用你的初始化逻辑
        StartGame();
    }

    // 别忘了在不需要时取消订阅，防止内存泄漏（如果是常驻单例，通常不需要，但在关闭游戏时是个好习惯）
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //游戏开始时调用这个函数来初始化UI
    public void StartGame()
    {
        transform.GetChild(0).GetComponent<AudioSource>().Stop();
        transform.GetChild(0).GetComponent<AudioSource>().Play();

        Debug.Log("单例加载");
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Main":
                canvas.SetActive(false);
                break;

            case "Level1":

                levelText.text = "Level 1";
                nameText.text = "by：Ma Xiaohan";
                ChuShiHua();
                break;
            case "Level2":
                levelText.text = "Level 2";
                nameText.text = "by：Ouyang Hongke";
                ChuShiHua();
                break;

            case "Level3":
                levelText.text = "Level 3";
                nameText.text = "by：Zhang Yuqi";
                ChuShiHua();
                break;

            case "Level4":
                levelText.text = "Level 4";
                nameText.text = "by：Jiang Guihan";
                ChuShiHua();
                break;
            default:
                break;
        }

        ScoreController();
    }

    void ChuShiHua()
    {
        score = 0;
        health = 3;

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }

        canvas.gameObject.SetActive(true);
        titlePanel.SetActive(true);
        pauseButton.gameObject.SetActive(true);
    }

    public void AddScore()
    {
        score += 1;
        ScoreController();
    }
    public void SubHealth()
    {
        health -= 1;
        if (health <= 0)
        {
            gameOver.SetActive(true);
            FindAnyObjectByType<PlayerController>().isOver = true;

        }
        ScoreController();
    }

    public void WinGame()
    {
        gameWin.SetActive(true);
        FindAnyObjectByType<PlayerController>().isOver = true;
    }

    //得分或者受伤时调用这个函数来更新UI
    public void ScoreController()
    {
        scoreText.text = score.ToString();
        scoreEndText.text = score.ToString();
        scoreEndWinText.text = score.ToString();

        healthText.text = health.ToString();
        healthEndText.text = health.ToString();
    }

    public void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0; // 暂停游戏
            pauseButton.transform.GetChild(0).GetComponent<Text>().text = "X";
            pauseButton.transform.GetChild(0).GetComponent<Text>().color = Color.red;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.transform.GetChild(0).GetComponent<Text>().text = "||";
            pauseButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            pausePanel.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1; // 确保在加载新场景时游戏不处于暂停状态
        SceneManager.LoadScene(sceneName);
    }

    public void RestPlayer()
    {
        Time.timeScale = 1; // 确保在重置玩家时游戏不处于暂停状态
        pauseButton.transform.GetChild(0).GetComponent<Text>().text = "||";
        pauseButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        transform.GetChild(0).GetComponent<AudioSource>().Stop();
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 重新加载当前场景 
    }

    public void HelpPanel()
    {
        helpPanel.SetActive(true);
    }
    void Start()
    {

    }
    public void ClosePanel(GameObject close)
    {
        close.SetActive(false);
    }

    public void NextLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Level1":
                SceneManager.LoadScene("Level2");
                break;
            case "Level2":
                SceneManager.LoadScene("Level3");
                break;
            case "Level3":
                SceneManager.LoadScene("Level4");
                break;
            case "Level4":
                SceneManager.LoadScene("Main");
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
