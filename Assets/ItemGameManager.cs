using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ

public class ItemGameManager : MonoBehaviour
{
    public static ItemGameManager Instance; // �̱��� �ν��Ͻ�

    public Transform[] itemDisplayPositions; // ������ ������ ǥ�� ��ġ
    public Sprite[] itemIcons; // ������ ������ �̹���
    private string targetItem = ""; // ��ǥ ������
    private int collectedCount = 0; // ������ ������ ����

    void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem(string itemName)
    {
        if (targetItem == "" || targetItem == itemName)
        {
            // ������ ������ ����
            targetItem = itemName; // ��ǥ ������ ����
            collectedCount++; // ���� ���� ����
            UpdateItemDisplay(itemName); // UI ������Ʈ

            if (collectedCount >= 3)
            {
                Debug.Log("���� Ŭ����! NameScene���� ��ȯ�˴ϴ�.");
                SceneManager.LoadScene("NameScene"); // NameScene���� ��ȯ
            }
        }
        else
        {
            // �ٸ� ������ Ŭ�� �� �ʱ�ȭ
            Debug.Log($"�ٸ� ������({itemName}) Ŭ��: �ʱ�ȭ");
            collectedCount = 0; // ���� ���� �ʱ�ȭ
            targetItem = ""; // ��ǥ ������ �ʱ�ȭ
            ResetItemDisplay(); // ȭ�� �ʱ�ȭ
        }
    }

    private void UpdateItemDisplay(string itemName)
    {
        ResetItemDisplay(); // ���� ǥ�ø� �ʱ�ȭ

        for (int i = 0; i < collectedCount; i++)
        {
            // ������ �������� ǥ��
            SpriteRenderer sr = itemDisplayPositions[i].GetComponent<SpriteRenderer>();
            sr.sprite = GetItemIcon(itemName); // �ش� �������� ������ ����
            sr.color = Color.white; // ������ ǥ��
        }
    }

    private void ResetItemDisplay()
    {
        // ������ �ʱ�ȭ (�����ϰ� ����)
        foreach (Transform position in itemDisplayPositions)
        {
            SpriteRenderer sr = position.GetComponent<SpriteRenderer>();
            sr.sprite = null;
            sr.color = Color.clear;
        }
    }

    private Sprite GetItemIcon(string itemName)
    {
        // ������ �̸��� ���� ������ ��ȯ
        switch (itemName)
        {
            case "mouse": return itemIcons[0];
            case "keyboard": return itemIcons[1];
            case "pen": return itemIcons[2];
            default: return null;
        }
    }
}
