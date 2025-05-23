using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f; // �������� ��������
    [SerializeField] private float topYPosition = 5f; // ������� �������
    [SerializeField] private float bottomYPosition = 0f; // ������ �������\

    public int dfs;

    private bool movingUp = true; // ����������� ��������

    private void Update()
    {
        // �������� �����
        if (movingUp)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            if (transform.position.y >= topYPosition)
                movingUp = false;
        }
        // �������� ����
        else
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= bottomYPosition)
                movingUp = true;
        }
    }
}