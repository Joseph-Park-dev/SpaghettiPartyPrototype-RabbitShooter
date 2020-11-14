using System.Collections;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class LouisAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _attackAim;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _shotInterval = 0.5f;
    [SerializeField] private float _meleeInterval = 0.4f;
    private bool _canShoot;
    private bool _canMelee;
    [SerializeField] private Animator _attackAnimator;
    [SerializeField] private LineRenderer _lineRenderer;
    private EnemyRobotDamage _enemyDamage;

    PlayerControls _playerControls;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.GamePlay.SwordAttack.performed += ctx => StartCoroutine(MeleeAttack(_meleeInterval));
        _playerControls.GamePlay.GunAttack.performed += ctx => StartCoroutine(GunAttack(_shotInterval));
        _lineRenderer.enabled = false;
        _canShoot = true;
        _canMelee = true;
    }

    private IEnumerator MeleeAttack(float meleeInterval)
    {
        if(_canMelee)
        {
            _canMelee = false;
            _attackAnimator.SetTrigger("MeleeAttack");
            FindObjectOfType<SoundManager>().Play("MeleeAttack");
            Collider2D[] hitList = Physics2D.OverlapCircleAll(
                _attackPoint.position,
                _attackRange
                );
            StartCoroutine(VibratePad(0.2f, 0.1f));

            foreach (Collider2D hit in hitList)
            {
                if (hit.tag == "ENEMY")
                {
                    _enemyDamage = hit.gameObject.GetComponent<EnemyRobotDamage>();
                    _enemyDamage.DestroyEnemy(_enemyDamage.gameObject);
                    StartCoroutine(VibratePad(1.0f, 0.3f));
                }
            }
            yield return new WaitForSeconds(meleeInterval);
            _canMelee = true;
        }
        
    }

    private IEnumerator GunAttack(float shotInterval)
    {
        if(_canShoot)
        {
            _canShoot = false;
            StartCoroutine(VibratePad(0.3f, 0.1f));
            StartCoroutine(Shoot());
            yield return new WaitForSeconds(shotInterval);
            _canShoot = true;
        }
    }


    private IEnumerator Shoot()
    {
        FindObjectOfType<SoundManager>().Play("GunAttack");
        RaycastHit2D hit = Physics2D.Raycast(_attackAim.position, _attackAim.transform.up);
        if(hit)
        {
            _enemyDamage = hit.transform.GetComponent<EnemyRobotDamage>();
            _enemyDamage.DestroyEnemy(_enemyDamage.gameObject);
            StartCoroutine(VibratePad(1.0f, 0.3f));
        }
        CreateGunTracer(_attackAim.localPosition, Vector2.up * 100); 
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
        _lineRenderer.enabled = false;
    }



    private void CreateGunTracer(Vector2 fromPos, Vector2 targetPos)
    {
        _lineRenderer.SetPosition(0, fromPos);
        _lineRenderer.SetPosition(1, targetPos);
    }


    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private IEnumerator VibratePad(float magnitude, float seconds)
    {
        GamePad.SetVibration(playerIndex, magnitude, magnitude);
        yield return new WaitForSeconds(seconds);
        GamePad.SetVibration(playerIndex, 0, 0);
    }
}
