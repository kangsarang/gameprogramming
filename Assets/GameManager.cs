using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class GameManager : MonoBehaviour
{
    public needle_short hourNeedle;
    public needle_long minuteNeedle;
    public UnityEngine.UI.Text resultText; // UI �ؽ�Ʈ
    public UnityEngine.UI.Text hint;// UI �ؽ�Ʈ
    private bool resultChecked = false;
    private bool gameStarted = false;

    void Update()
    {
        if ((hourNeedle.isRunning || minuteNeedle.isRunning) && !gameStarted)
        {
            gameStarted = true;
        }

        if (gameStarted && !hourNeedle.isRunning && !minuteNeedle.isRunning && !resultChecked)
        {
            CheckIfWin();
        }
    }

    void CheckIfWin()
    {
        if (hourNeedle.inTargetHourRange && minuteNeedle.inTargetMinuteRange)
        {
            resultText.text = "�����մϴ�! ���� �ð��̿���!";
            Debug.Log("�����մϴ�! ��ǥ �ð��� ������ϴ�.");

            // �� �̸��� ���� ���� ������ �̵�
            if (SceneManager.GetActiveScene().name == "ClockGame")
            {
                Invoke("LoadNextGameScene", 2f); // ClockGameScene2�� ��ȯ
            }
            else if (SceneManager.GetActiveScene().name == "ClockGame2")
            {
                Invoke("LoadHintScene", 2f); // ClockHintScene���� ��ȯ
            }
        }
        else
        {
            resultText.text = "�ƽ����ϴ�. �ٽ� �����غ����?";
            Debug.Log("�ƽ����ϴ�. ��ǥ �ð��� ������ ���߽��ϴ�.");
            Invoke("ResetGame", 2f); // 2�� �� ���� �ʱ�ȭ
        }

        resultChecked = true;
    }

    void LoadNextGameScene()
    {
        SceneManager.LoadScene("ClockGame2");
    }

    void LoadHintScene()
    {
        SceneManager.LoadScene("ClockHintScene"); 
    }

    void ResetGame()
    {
        hourNeedle.ResetNeedle();
        minuteNeedle.ResetNeedle();
        resultChecked = false;
        gameStarted = false;
        resultText.text = "ȭ���� ���� �ð踦 ���߼���!"; // �ʱ� �޽��� ����
        
        // �� �̸��� ���� �ٸ� ��Ʈ
        if (SceneManager.GetActiveScene().name == "ClockGame")
        {
            hint.text = "hint .oO(���� �ð��� 1�� 15�к��� 1�� 30�б�����)";
        }
        else if (SceneManager.GetActiveScene().name == "ClockGame2")
        {
            hint.text = "hint .oO(���� ���� �ð��� 2�� 45�к��� 3�� 00�б�����)";
        }
    }
}
