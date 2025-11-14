using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class TodoManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField taskInput;
    public Button addButton;
    public Transform contentPanel;
    public GameObject taskPrefab;

    void Start()
    {
        addButton.onClick.AddListener(AddTask); // Add butonuna tıklama eventi
    }

    void AddTask()
    {
        string taskText = taskInput.text;
        if (string.IsNullOrEmpty(taskText)) return;

        GameObject newTask = Instantiate(taskPrefab, contentPanel);
        newTask.transform.localScale = Vector3.one;

        // Text (Hiyerarşi yolu düzeltildi)
        Transform taskTextTransform = newTask.transform.Find("Hori/TaskText");
        if (taskTextTransform == null) { Debug.LogError("Hata: 'Hori/TaskText' bulunamadı!"); Destroy(newTask); return; }
        TextMeshProUGUI taskTextComponent = taskTextTransform.GetComponent<TextMeshProUGUI>();
        taskTextComponent.text = taskText;

        // Delete button (Hiyerarşi yolu düzeltildi)
        Transform deleteButtonTransform = newTask.transform.Find("Hori/DeleteButton");
        if (deleteButtonTransform == null) { Debug.LogError("Hata: 'Hori/DeleteButton' bulunamadı!"); Destroy(newTask); return; }
        Button deleteButton = deleteButtonTransform.GetComponent<Button>();
        deleteButton.onClick.AddListener(() => Destroy(newTask));

        // ✅ Toggle (tamamlandı) (Hiyerarşi yolu düzeltildi)
        Transform taskToggleTransform = newTask.transform.Find("Hori/TaskToggle");
        if (taskToggleTransform == null) { Debug.LogError("Hata: 'Hori/TaskToggle' bulunamadı!"); Destroy(newTask); return; }
        Toggle taskToggle = taskToggleTransform.GetComponent<Toggle>();
        
        taskToggle.onValueChanged.AddListener((isOn) =>
        {
            // Tik atıldığında texti çizgili yap, tik kaldırıldığında normale döndür
            if (isOn)
                taskTextComponent.fontStyle = FontStyles.Strikethrough;
            else
                taskTextComponent.fontStyle = FontStyles.Normal;
        });

        taskInput.text = "";
        taskInput.ActivateInputField(); // input field aktif kalsın
    }
}