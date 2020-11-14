using System.Collections;
using UnityEngine;
using EZCameraShake;
using XInputDotNetPure;

public class EnemyRobotDamage : MonoBehaviour
{
    [SerializeField] private GameObject _explosionEffect;

    public void DestroyEnemy(GameObject enemy)
    {
        FindObjectOfType<SoundManager>().Play("RobotDies");
        Instantiate(_explosionEffect, enemy.transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(10f, 4f, .1f, 1f);
        Destroy(enemy.gameObject);
    }
}
