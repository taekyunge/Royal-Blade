using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameMgr : Singleton<GameMgr>
{
    [HideInInspector]
    public bool gameStart = false;

    // 총 점수
    public int totalPoint = 0;

    private int totalCount = 0;
    public int attackCount = 0;

    [SerializeField]
    private Wave[] waves;

    [SerializeField]
    private Image[] heartImages;

    [SerializeField]
    private Text pointText;

    [SerializeField]
    private Slider waveSlider;

    [SerializeField]
    private Text waveText;

    [SerializeField]
    private GameObject startPop;

    [HideInInspector]
    public int currentWave = 0;

    // 캐릭터
    public Character character;

    private void Update()
    {
        if (!gameStart)
            return;

        pointText.text = totalPoint.ToString("#,##0");

        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].color = (i < character.hp) ? Color.white : Color.gray;
        }

        waveText.text = string.Format("Wave {0}", currentWave + 1);
        waveSlider.value = attackCount == 0 ? 0 : (float)attackCount / (float)totalCount;

        if(character.hp == 0)
        {
            gameStart = false;
            startPop.SetActive(true);
        }

        if (waves[currentWave].isClear)
        {
            currentWave++;

            if (currentWave < waves.Length)
            {
                WaveStart();
            }
            else
            {
                gameStart = false;
                startPop.SetActive(true);
            }
        }
    }

    public void GameStart()
    {
        totalPoint = 0;
        totalCount = 0;
        currentWave = 0;
        attackCount = 0;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].Initialize();

            totalCount += waves[i].count;
        }

        character.Initialize();

        gameStart = true;

        WaveStart();
    }

    private void WaveStart()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].gameObject.SetActive(i == currentWave);
        }
    }

    public void OnClickStart()
    {
        GameStart();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
