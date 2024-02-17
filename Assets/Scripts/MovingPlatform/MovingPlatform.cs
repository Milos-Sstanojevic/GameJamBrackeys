using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float timeOffset;
    [SerializeField] float movingSpeed;
    [SerializeField] Vector3 target1;
    [SerializeField] Vector3 target2;
    [SerializeField] float minDiff;
    [SerializeField] float getAwayDistance;

    private int currentTarget;

    private Collider2D myCollider;
    private ContactFilter2D filter;
    private List<Collider2D> result;
    void Start()
    {
        currentTarget = 1;
        myCollider = GetComponent<Collider2D>();
        filter = new ContactFilter2D().NoFilter();
        result = new();
    }
    void Update()
    {
        var targetPosition = currentTarget == 1 ? target1 : target2;
        var diff = targetPosition - transform.position;

        transform.Translate(diff.normalized * movingSpeed * Time.deltaTime);

        if ((diff.magnitude < minDiff))
        {
            SwitchDirection();
        }
        else
        {
            myCollider.OverlapCollider(filter, result);
            if (result.Any(collider => collider.CompareTag("Platform")))
            {
                SwitchDirection();
            }
            result.Clear();
        }
    }
    private void SwitchDirection()
    {
        currentTarget = currentTarget == 1 ? 2 : 1;
        // Mrdnem ga malo u novi smer jer se zaglavljuje u druge kolajdere
        var targetPosition = currentTarget == 1 ? target1 : target2;
        var diff = targetPosition - transform.position;
        transform.Translate(diff.normalized * getAwayDistance);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            /*
                * Desava se greska samo kada izadjes iz play mode-a dok stojis na moving platform
                * 
                * Greska se ovde javlja jer unity ne dozvoljava da se menja parent objektu koji
                * se trenutno aktivira ili deaktivira. Ovde konkretno se player pokusava 
                * deaktivirati i u tom trenutku moving platform skapira da je player izasao iz colizije
                * (bas zbog deaktivacije pretposavljam) i pozove ovu funkciju.
                *
            */

            collision.gameObject.transform.SetParent(null);
        }
    }
}
