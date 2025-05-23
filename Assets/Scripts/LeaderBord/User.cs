[System.Serializable]
public class User
{
    public string username;
    public int points;

    public User(string username, int points)
    {
        this.username = username;
        this.points = points;
    }
}
