using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    int coin;
    int coinMax;
    float time;
    public GameObject coins;
    public TextMeshProUGUI text;
    public TextMeshProUGUI resultTextCoin;
    public TextMeshProUGUI resultTextTime;
    // Start is called before the first frame update
    void Start()
    {
        coins = GameObject.Find("Coins");
        text = GameObject.Find("Text_Coin").GetComponent<TextMeshProUGUI>();
        time =0; coin = 0;
        coinMax = coins.transform.childCount;
        ScoreUpdate();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene=SceneManager.GetActiveScene();
        if (scene.name== "ClearScene")
        {
            resultTextCoin = GameObject.Find("Text_Score").GetComponent<TextMeshProUGUI>();
            resultTextTime = GameObject.Find("Text_Time").GetComponent<TextMeshProUGUI>();
            resultTextTime.text = time.ToString("F1") + " s";
            resultTextCoin.text = coin.ToString() + "/" + coinMax.ToString();
        }
        if(scene.name== "TitleScene")
        {
            Destroy(gameObject);
        }
        if(scene.name== "GameScene")
        {
            time += Time.deltaTime;
        }
    }
    public void GetCoin()
    {
        coin++;
        ScoreUpdate();
    }
    void ScoreUpdate()
    {
        text.text = coin.ToString()+"/"+coinMax.ToString();
    }
}
