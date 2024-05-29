using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Raycastshoot : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private int _damage = 40;
    [SerializeField] private Transform _spavnPoint;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _damageableLayers;

    void Update()
    {
        RotateGun();
        
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(shootSound);
            StartCoroutine(Raycast(_lineRenderer, _spavnPoint, _damage, _spavnPoint.right, _damageableLayers));
        }      
    }

    private void RotateGun()
    {
        var mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(mouseCoordinates.y, mouseCoordinates.x) * Mathf.Rad2Deg;
        
        if (angle >= -90f && angle <= 90f)
        {
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * 10);
        }    
    }


    public static IEnumerator Raycast(LineRenderer lineRenderer, Transform spavnPoint, int damage, Vector3 direction, LayerMask damageableLayers)
    {
        var hitInfo = Physics2D.Raycast(spavnPoint.position, direction, Mathf.Infinity, damageableLayers);
        if (hitInfo)
        {
            var damageableObject = hitInfo.transform.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
            }

            //Instantiate(_impactEffect, hitInfo.point, Quaternion.identity) ���������� ��� ������� �� ��������;
            lineRenderer.SetPosition(0, spavnPoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, spavnPoint.position);
            lineRenderer.SetPosition(1, spavnPoint.position + direction * 100);
        }

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
}
