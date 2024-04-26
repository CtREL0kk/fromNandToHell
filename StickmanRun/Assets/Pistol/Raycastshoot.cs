using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycastshoot : MonoBehaviour
{
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private int _damage = 40;
    [SerializeField] private Transform _spavnPoint;
    [SerializeField] private float _delayBtwShoots;
    [SerializeField] private LineRenderer _lineRenderer;
    private float _localDelayBtwShoots;

    void Update()
    {
        RotateGun();
        
        if (Input.GetMouseButton(0))
            StartCoroutine(Raycast());
    }

    private void RotateGun()
    {
        var mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var targetRotation = Mathf.Atan2(mouseCoordinates.y, mouseCoordinates.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, Quaternion.Euler(0, 0, targetRotation), Time.deltaTime * 10);
    }

    IEnumerator Raycast()
    {
        
        var hitInfo = Physics2D.Raycast(_spavnPoint.position, _spavnPoint.right);
        if (hitInfo)
        {
            var enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }

            //Instantiate(_impactEffect, hitInfo.point, Quaternion.identity);
            _lineRenderer.SetPosition(0, _spavnPoint.position);
            _lineRenderer.SetPosition(1, hitInfo.point);
        }
        else 
        {
            _lineRenderer.SetPosition(0, _spavnPoint.position);
            _lineRenderer.SetPosition(1, _spavnPoint.position + _spavnPoint.right * 100);
        }
    
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _lineRenderer.enabled = false;
    }
}
