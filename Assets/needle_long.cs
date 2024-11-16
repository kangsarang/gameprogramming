using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needle_long : MonoBehaviour
{
    public bool isRunning = false;
    public bool inTargetMinuteRange = false;
    private float rotSpeed = -10f; // �⺻ ȸ�� �ӵ�
    private float targetMinuteStartAngle = 360f - 90f; // 15�� ���� ����
    private float targetMinuteEndAngle = 360f - 180f;  // 30�� �� ����
    private Quaternion initialRotation; // �ʱ� ȸ�� ����
    private bool resetComplete = true;  // �ʱ�ȭ �Ϸ� ���� �÷���

    void Start()
    {
        initialRotation = transform.rotation; // �ʱ� ȸ�� ����

        // �� �̸��� ���� �ӵ��� ��ǥ ���� ����
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame")
        {
            rotSpeed = -5f; // ��1: ���� �ӵ�
            targetMinuteStartAngle = 360f - 90f; // 15�� ����
            targetMinuteEndAngle = 360f - 180f; // 30�� ��
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2")
        {
            rotSpeed = -10f; // ��2: ���� �ӵ�
            targetMinuteStartAngle = 0f; // 45�� ����
            targetMinuteEndAngle = 90f;   // ����
        }
    }

    void Update()
    {
        if (!resetComplete) return; // �ʱ�ȭ�� �Ϸ���� �ʾҴٸ� �Է� ó�� �� ��

        if (Input.GetMouseButtonDown(0))
        {
            if (!isRunning)
            {
                isRunning = true;
                Debug.Log("�� �ٴ��� �����̱� �����߽��ϴ�.");
            }
            else
            {
                isRunning = false;
                CheckMinuteTargetRange();
            }
        }

        if (isRunning)
        {
            transform.Rotate(0, 0, rotSpeed); // �ٴ� ȸ��
        }
    }

    void CheckMinuteTargetRange()
    {
        float currentAngle = transform.eulerAngles.z;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame" && currentAngle <= targetMinuteStartAngle && currentAngle >= targetMinuteEndAngle)
        {
            inTargetMinuteRange = true;
            Debug.Log("�� �ٴ��� ��ǥ ������ �ֽ��ϴ�.");
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2" && currentAngle >= targetMinuteStartAngle && currentAngle <= targetMinuteEndAngle)
        {
            inTargetMinuteRange = true;
            Debug.Log("�� �ٴ��� ��ǥ ������ �ֽ��ϴ�.");
        }
        else
        {
            inTargetMinuteRange = false;
            Debug.Log("�� �ٴ��� ��ǥ ������ ���� �ʽ��ϴ�.");
            Debug.Log($"�� �ٴ� ���� ����: {currentAngle}, ��ǥ ���� ����: {targetMinuteStartAngle} ~ {targetMinuteEndAngle}");
        }
    }

    public void ResetNeedle()
    {
        transform.rotation = initialRotation; // �ʱ� ��ġ�� ȸ��
        isRunning = false;
        inTargetMinuteRange = false;

        // �� �̸��� ���� �ʱ�ȭ �� �ӵ� ����
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame")
        {
            rotSpeed = -5f; // ��1: ���� �ӵ�
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2")
        {
            rotSpeed = -10f; // ��2: ���� �ӵ�
        }

        StartCoroutine(EnableInputAfterReset());
    }

    private IEnumerator EnableInputAfterReset()
    {
        resetComplete = false; // �Է� ��Ȱ��ȭ
        yield return null; // �� ������ ���
        resetComplete = true; // �Է� Ȱ��ȭ
    }

    public float GetRotSpeed()
    {
        return rotSpeed;
    }
}
