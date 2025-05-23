using UnityEngine;
using UnityEngine.UI;

public class UserEntry : MonoBehaviour
{
    [SerializeField] private Text usernameText;
    [SerializeField] private Text pointsText;

    public void SetUserData(User user)
    {
        usernameText.text = user.username;
        pointsText.text = user.points.ToString();
    }
}
