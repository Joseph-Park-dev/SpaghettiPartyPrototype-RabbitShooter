using UnityEngine;

public class EnemyRobotMovement : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _minDist;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("PLAYER").transform;
    }

    private void Update()
    {
        if(_player != null)
        {
            RotateTowardTarget(_player);
            MoveTowardTarget(_player);
        }
    }

    private void RotateTowardTarget(Transform target)
    {
        Vector2 dir = (Vector2)target.transform.position - rb.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = -rotateAmount * _rotateSpeed;
    }

    private void MoveTowardTarget(Transform target)
    {
        if (_minDist <= Vector2.Distance((Vector2)target.transform.position, rb.position))
        {
            rb.velocity = transform.up * _speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
