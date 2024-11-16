using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needle_short : MonoBehaviour
{
    public bool isRunning = false;
    public bool inTargetHourRange = false;
    private float rotSpeed = 0;
    private float targetHourStartAngle = 360f - 30f; // 1�� ���� ����
    private float targetHourEndAngle = 360f - 60f;   // 1�ÿ� 2�� ������ ����
    private Quaternion initialRotation; // �ʱ� ȸ�� ����
    private bool resetComplete = true;  // �ʱ�ȭ �Ϸ� ���� �÷���

    public needle_long minuteNeedle; // �� �ٴ��� ȸ�� �ӵ��� ����

    void Start()
    {
        initialRotation = transform.rotation; // �ʱ� ȸ�� ����

        // �� �̸��� ���� ��ǥ ���� ����
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame")
        {
            targetHourStartAngle = 360f - 30f; // 1�� ����
            targetHourEndAngle = 360f - 60f;  // 2�� ��
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ClockGame2")
        {
            targetHourStartAngle = 360f - 60f; // 2�� ����
            targetHourEndAngle = 360f - 90f;  // 3�� ��
        }
    }

    void Update()
    {
        if (!resetComplete) return; // �ʱ�ȭ�� �Ϸ���� �ʾҴٸ� �Է� ó�� �� ��

        if (minuteNeedle.isRunning)
        {
            if (rotSpeed != minuteNeedle.GetRotSpeed() / 12f) // ���ʿ��� ���� �ּ�ȭ
            {
                rotSpeed = minuteNeedle.GetRotSpeed() / 12f;
            }
            transform.Rotate(0, 0, rotSpeed); // �� �ٴ��� ȸ�� �ӵ��� ������� ȸ��
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isRunning)
            {
                isRunning = true;
                Debug.Log("�ð� �ٴ��� �����̱� �����߽��ϴ�.");
            }
            else
            {
                isRunning = false;
                CheckHourTargetRange();
            }
        }
    }

    void CheckHourTargetRange()
    {
        float currentAngle = transform.eulerAngles.z;
        if (currentAngle <= targetHourStartAngle && currentAngle >= targetHourEndAngle)
        {
            inTargetHourRange = true;
            Debug.Log("�ð� �ٴ��� ��ǥ ������ �ֽ��ϴ�.");
        }
        else
        {
            inTargetHourRange = false;
            Debug.Log("�ð� �ٴ��� ��ǥ ������ ���� �ʽ��ϴ�.");
            Debug.Log($"ª�� �ٴ� ���� ����: {currentAngle}, ��ǥ ���� ����: {targetHourStartAngle} ~ {targetHourEndAngle}");

        }
    }

    public void ResetNeedle()
    {
        transform.rotation = initialRotation; // �ʱ� ��ġ�� ȸ��
        isRunning = false;
        inTargetHourRange = false;
        rotSpeed = 0; // ȸ�� �ӵ� �ʱ�ȭ

        // �ʱ�ȭ �� �� ������ ���� Ŭ�� �Է��� ����
        StartCoroutine(EnableInputAfterReset());
    }

    private IEnumerator EnableInputAfterReset()
    {
        resetComplete = false; // �Է� ��Ȱ��ȭ
        yield return null; // �� ������ ���
        resetComplete = true; // �Է� Ȱ��ȭ
    }
}
