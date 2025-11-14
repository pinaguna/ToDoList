using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskItemUI : MonoBehaviour
{
    // Task Item prefab'ında bu referansları bağlayın
    [Header("Item Components")]
    public TextMeshProUGUI taskText;
    public Button deleteButton;
    public Toggle taskToggle;

    private TodoManager manager; // Ana yöneticiye referans

    public void Setup(string text, TodoManager todoManager)
    {
        taskText.text = text;
        manager = todoManager;

        // Buton ve Toggle eventlerini burada bağlayın
        deleteButton.onClick.AddListener(DeleteTask);
        taskToggle.onValueChanged.AddListener(ToggleTaskCompletion);
    }

    void ToggleTaskCompletion(bool isCompleted)
    {
        // Tik atıldığında metni çizgili yap
        if (isCompleted)
            taskText.fontStyle = FontStyles.Strikethrough;
        else
            taskText.fontStyle = FontStyles.Normal;
    }

    void DeleteTask()
    {
        // Objenin kendisini hiyerarşiden sil
        Destroy(gameObject);
        // İhtiyaç duyarsanız, TodoManager üzerinden ana listeden de silebilirsiniz.
        // manager.RemoveTaskFromList(this); 
    }
}