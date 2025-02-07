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

    // 아이템 강화용 변수
    private string defaultText;
    public Text itemGradeText;
    public Text enhanceText;
    public Button EnhanceBtn;

    // 이미지 교체용 변수
    public RawImage rawImage;
    public string defaultPath = "sword";

    // JSON다루기
    private string enhanceDataPath;
    private EnhanceData enhanceData;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 이걸로 등록하면 인스펙터 창에서 안보이니까 주의하셍
        nameInputField.onEndEdit.AddListener(ValueChanged);
        descriptionInputField.onEndEdit.AddListener(ValueChanged);

        // 저장관련 버튼 상호작용 활성/비활성 초기화하기
        saveBtn.onClick.AddListener(SaveData);
        saveBtn.interactable = true;
        loadBtn.onClick.AddListener(LoadData);
        loadBtn.interactable = false;
        deleteBtn.onClick.AddListener(DeleteData);
        deleteBtn.interactable = false;
        ItemData itemData = new ItemData();


        // 여기는 강화 기능 초기화하기
        enhanceDataPath = Path.Combine(Application.persistentDataPath, "enhanceData.json");
        enhanceData = new EnhanceData();
        
        enhanceData.itemGrade = 0;
        defaultText = "+ ";
        EnhanceBtn.onClick.AddListener(Enhance);
        EnhanceBtn.interactable = false;
        enhanceData.EnhanceSuccessRate = 100;


        SetDataInfo(itemData);
    }

    // 1.publilc 함수는 유니티 인스펙터에서 직접 연결
    // 2.public이 아닌 함수는 스크립트를 통해 직접 연결  

    public void Sample()
    {
        Debug.Log("입력발생");
    }
    // Update is called once per frame
    void Update()
    {
    }

    void SetDataInfo(ItemData itemData)
    {
        enhanceText.text = $"강화 확률 : {enhanceData.EnhanceSuccessRate}%";
        itemGradeText.text = $"{defaultText}{enhanceData.itemGrade}";
        nameText.text = $"{defaultText}{enhanceData.itemGrade} {itemData.itemName}";
        descriptionText.text = itemData.itemDescription;
    }

    void ValueChanged(string text)
    {
        Debug.Log("메세지 전송하기 : " + text);
    }

    void Enhance()
    {
        //확률 계산 연산하기
        int value = Random.Range(0, 100);
        if (enhanceData.EnhanceSuccessRate - value >= 0)
        {
            // 강화 성공시
            //// 강화 성공하고 저장 안하면 니 탓이다. 후회하지마라
            Debug.Log("강화 성공");
            enhanceData.EnhanceSuccessRate -= 2 * (enhanceData.EnhanceSuccessRate / 10);
            enhanceData.itemGrade++;
        }
        else
        {
            // 강화 실패시
            //// 실패하면 무기 터지는거야 그러니까 지운다?
            Debug.Log($"강화 실패");
            enhanceData.EnhanceSuccessRate = 100;
            enhanceData.itemGrade = 0;
            DeleteData();
        }
        // 화면 갱신하기
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
            Debug.LogError($"이미지 로드 실패: {imgPath}");
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
            Debug.Log("강화 정보 불러오기 완료: " + json);
        }
        else
        {
            Debug.Log("강화 데이터 없음, 기본값 적용");
            enhanceData.itemGrade = 0;
            enhanceData.EnhanceSuccessRate = 100;
        }

        if (itemData.itemName == "")
        {
            EnhanceBtn.interactable = false;
            deleteBtn.interactable = false;
            itemData.itemName = "가지고 있는 아이템이 없어요";
            itemData.itemDescription = "아이템 정보를 입력해주세요";
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
