using System.Collections;
using UnityEngine;
using EZCameraShake;
using XInputDotNetPure;

public class LouisDamage : MonoBehaviour
{
    PlayerIndex _playerIndex;

    [SerializeField] int _maxHealth = 5;
    int _currentHealth;
    [SerializeField] HealthBar _healthBar;

    private void Start()
    {
        _healthBar = GameObject.FindGameObjectWithTag("UI_HEALTH").
            GetComponent<HealthBar>();
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    private void Update()
    {
        DestroyPlayer();
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag == "ENEMY")
        {
            --_currentHealth;
            _healthBar.SetHealth(_currentHealth);
            CameraShaker.Instance.ShakeOnce(10f, 4f, .1f, 1f);
            StartCoroutine(VibratePad(1.0f, 0.2f));
        }
    }

    private void DestroyPlayer()
    {
        if (_currentHealth <= 0)
        {
            FindObjectOfType<SoundManager>().Play("LouisDies");
            GamePad.SetVibration(_playerIndex, 0f, 0f);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator VibratePad(float magnitude, float seconds)
    {
        GamePad.SetVibration(_playerIndex, magnitude, magnitude);
        yield return new WaitForSeconds(seconds);
        GamePad.SetVibration(_playerIndex, 0, 0);
    }
}
