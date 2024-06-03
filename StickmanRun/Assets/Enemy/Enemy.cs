using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Характеристики")]
    [SerializeField] protected int hp;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fireRateConstant;
    [SerializeField] private int damage;
    
    [Header("Оружие")]
    [SerializeField] private Transform gun;
    [SerializeField] private LayerMask _damageableLayers;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private Transform _spavnPoint;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int startWeaponAngle;
    
    [Header("Звуки")]
    [SerializeField] private AudioClip audioShoot;
    
    private Animator _animator;
    private Transform _player;
    private float _fireRate;
    private bool _isDead;
    private Transform _mainCharacter;
    private AudioSource audio;
    
    protected virtual void Start()
    {
        audio = GetComponent<AudioSource>();
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) _mainCharacter = playerObject.transform;
        _animator = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (_isDead) return;
        RotateWeapon();
        if (FindDistance() <= attackRange && _fireRate <= 0)
        {
            Attack();
            _fireRate = fireRateConstant;
        }
        else _fireRate -= Time.deltaTime;
    }

    private void Attack()
    {
        fireEffect.SetActive(false);
        StartCoroutine(ShowFireEffect());
    }

    IEnumerator ShowFireEffect()
    {
        fireEffect.SetActive(true);
        var hitInfo = Physics2D.Raycast(_spavnPoint.position, -_spavnPoint.right, Mathf.Infinity, _damageableLayers);
        if (hitInfo)
        {
            var damageableObject = hitInfo.transform.GetComponent<IDamageable>();
            damageableObject?.TakeDamage(damage);
            if (audioShoot) audio.PlayOneShot(audioShoot);

            _lineRenderer.SetPosition(0, _spavnPoint.position);
            _lineRenderer.SetPosition(1, _mainCharacter.position);
        }
        
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        
        _lineRenderer.enabled = false;
        fireEffect.SetActive(false);
    }
    
    private float FindDistance() => Vector3.Distance(transform.position, _player.position);

    private void RotateWeapon()
    {
        if (_mainCharacter == null) return;
        var direction = _mainCharacter.Find("Aim").position - gun.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!(angle is > -90 and < 90))
        {
            gun.rotation = Quaternion.Slerp(gun.rotation,
                Quaternion.Euler(0, 0, angle + startWeaponAngle), 5f * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        _animator.SetTrigger("Death");
    }
}