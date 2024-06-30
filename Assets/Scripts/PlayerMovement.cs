using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = (nameof(Horizontal));
    private const string Vertical = (nameof(Vertical));

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate() 
    {
        float rotation  = Input.GetAxis(Horizontal);

        transform.Rotate(rotation * _rotateSpeed * Time.deltaTime * Vector3.up);
    }

    private void Move()
    {
        float direction = Input.GetAxis(Vertical);

        transform.Translate(direction * _moveSpeed * Time.deltaTime * Vector3.forward);
    }
}
