using UnityEngine;
using UnityEngine.UI;

public class StarsUpdate : MonoBehaviour
{
    [SerializeField] private Text starText;
    private int starsCount;

    private string CurrentUser => $"Stars_{APIService.Instance.UserId}";

    private void Start()
    {
        LoadStars();
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            starsCount++;
            UpdateUI();
            SaveStars();
            Destroy(collision.gameObject);
        }
    }

    // ��������� ������ �� PlayerPrefs
    private void LoadStars()
    {
        starsCount = PlayerPrefs.GetInt(CurrentUser, 0); // 0 - �������� �� ���������
    }

    // ��������� ������ � PlayerPrefs
    private void SaveStars()
    {
        PlayerPrefs.SetInt(CurrentUser, starsCount);
        PlayerPrefs.Save(); // ����� �������� Save() ��� ������������ ����������
    }

    // ��������� UI
    private void UpdateUI()
    {
        if (starText != null)
        {
            starText.text = starsCount.ToString();
        }
    }

    // ����� ��� ������ ���������� (����� ������������ ��� ������������)
    public void ResetStars()
    {
        starsCount = 0;
        PlayerPrefs.DeleteKey(CurrentUser);
        UpdateUI();
    }

    // ����� ��� ���������� ����� �� ������ ��������
    public void AddStars(int amount)
    {
        starsCount += amount;
        SaveStars();
        UpdateUI();
    }
}
