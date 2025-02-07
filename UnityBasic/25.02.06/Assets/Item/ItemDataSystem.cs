using NUnit.Framework.Interfaces;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing.MiniJSON;
using UnityEngine.UI;

[System.Serializable]
public class EnhanceData
{
    public int itemGrade = 0;
    public int EnhanceSuccessRate = 100;
}

public class ItemDataSystem : MonoBehaviour
{

    public TMP_InputField nameInputField;
    public TMP_InputField descriptionInputField;

    public Button loadBtn;
    public Button saveBtn;
    public Button deleteBtn;

    public ItemData itemData;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    // ������ ��ȭ�� ����
    private string defaultText;
    public Text itemGradeText;
    public Text enhanceText;
    public Button EnhanceBtn;

    // �̹��� ��ü�� ����
    public RawImage rawImage;
    public string defaultPath = "sword";

    // JSON�ٷ��
    private string enhanceDataPath;
    private EnhanceData enhanceData;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �̰ɷ� ����ϸ� �ν����� â���� �Ⱥ��̴ϱ� �����ϼ�
        nameInputField.onEndEdit.AddListener(ValueChanged);
        descriptionInputField.onEndEdit.AddListener(ValueChanged);

        // ������� ��ư ��ȣ�ۿ� Ȱ��/��Ȱ�� �ʱ�ȭ�ϱ�
        saveBtn.onClick.AddListener(SaveData);
        saveBtn.interactable = true;
        loadBtn.onClick.AddListener(LoadData);
        loadBtn.interactable = false;
        deleteBtn.onClick.AddListener(DeleteData);
        deleteBtn.interactable = false;
        ItemData itemData = new ItemData();


        // ����� ��ȭ ��� �ʱ�ȭ�ϱ�
        enhanceDataPath = Path.Combine(Application.persistentDataPath, "enhanceData.json");
        enhanceData = new EnhanceData();
        
        enhanceData.itemGrade = 0;
        defaultText = "+ ";
        EnhanceBtn.onClick.AddListener(Enhance);
        EnhanceBtn.interactable = false;
        enhanceData.EnhanceSuccessRate = 100;


        SetDataInfo(itemData);
    }

    // 1.publilc �Լ��� ����Ƽ �ν����Ϳ��� ���� ����
    // 2.public�� �ƴ� �Լ��� ��ũ��Ʈ�� ���� ���� ����  

    public void Sample()
    {
        Debug.Log("�Է¹߻�");
    }
    // Update is called once per frame
    void Update()
    {
    }

    void SetDataInfo(ItemData itemData)
    {
        enhanceText.text = $"��ȭ Ȯ�� : {enhanceData.EnhanceSuccessRate}%";
        itemGradeText.text = $"{defaultText}{enhanceData.itemGrade}";
        nameText.text = $"{defaultText}{enhanceData.itemGrade} {itemData.itemName}";
        descriptionText.text = itemData.itemDescription;
    }

    void ValueChanged(string text)
    {
        Debug.Log("�޼��� �����ϱ� : " + text);
    }

    void Enhance()
    {
        //Ȯ�� ��� �����ϱ�
        int value = Random.Range(0, 100);
        if (enhanceData.EnhanceSuccessRate - value >= 0)
        {
            // ��ȭ ������
            //// ��ȭ �����ϰ� ���� ���ϸ� �� ſ�̴�. ��ȸ��������
            Debug.Log("��ȭ ����");
            enhanceData.EnhanceSuccessRate -= 2 * (enhanceData.EnhanceSuccessRate / 10);
            enhanceData.itemGrade++;
        }
        else
        {
            // ��ȭ ���н�
            //// �����ϸ� ���� �����°ž� �׷��ϱ� �����?
            Debug.Log($"��ȭ ����");
            enhanceData.EnhanceSuccessRate = 100;
            enhanceData.itemGrade = 0;
            DeleteData();
        }
        // ȭ�� �����ϱ�
        ChangeSwordImage();
        SetDataInfo(itemData);
    }

    void ChangeSwordImage()
    {
        string imgPath = defaultPath;
        if (enhanceData.itemGrade < 4)
        {
            imgPath += "_01";
        }
        else if (enhanceData.itemGrade < 8)
        {
            imgPath += "_02";
        }
        else 
        {
            imgPath += "_03";
        }
        Texture2D loadedTexture = Resources.Load<Texture2D>(imgPath);
        if (loadedTexture != null)
        {
            rawImage.texture = loadedTexture;
        }
        else
        {
            Debug.LogError($"�̹��� �ε� ����: {imgPath}");
        }
    }
    public void SaveData()
    {
        Debug.Log("SaveData");
        itemData.itemName = nameInputField.text;
        itemData.itemDescription = descriptionInputField.text;
        PlayerPrefs.SetString("ItemName", itemData.itemName);
        PlayerPrefs.SetString("ItemDescription", itemData.itemDescription);
        PlayerPrefs.Save();
        string json = JsonUtility.ToJson(enhanceData, true);
        File.WriteAllText(enhanceDataPath, JsonUtility.ToJson(json));
        EnhanceBtn.interactable = true;
        loadBtn.interactable = true;
        deleteBtn.interactable = true;

        SetDataInfo(itemData);
    }
    public void LoadData()
    {
        Debug.Log("LoadData");
        itemData.itemName = PlayerPrefs.GetString("ItemName");
        itemData.itemDescription = PlayerPrefs.GetString("ItemDescription");
        if (File.Exists(enhanceDataPath))
        {
            string json = File.ReadAllText(enhanceDataPath);
            enhanceData = JsonUtility.FromJson<EnhanceData>(json);
            Debug.Log("��ȭ ���� �ҷ����� �Ϸ�: " + json);
        }
        else
        {
            Debug.Log("��ȭ ������ ����, �⺻�� ����");
            enhanceData.itemGrade = 0;
            enhanceData.EnhanceSuccessRate = 100;
        }

        if (itemData.itemName == "")
        {
            EnhanceBtn.interactable = false;
            deleteBtn.interactable = false;
            itemData.itemName = "������ �ִ� �������� �����";
            itemData.itemDescription = "������ ������ �Է����ּ���";
        }
        else
        {
            EnhanceBtn.interactable = true;
            deleteBtn.interactable = true;
        }
        SetDataInfo(itemData);
    }
    public void DeleteData()
    {
        Debug.Log("DeleteData");
        PlayerPrefs.DeleteKey("ItemName");
        PlayerPrefs.DeleteKey("ItemDescription");
        File.Delete(enhanceDataPath);
        EnhanceBtn.interactable = false;
        deleteBtn.interactable = false;
        SetDataInfo(itemData);
    }
}
