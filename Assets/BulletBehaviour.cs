using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int hitter = -1;
    float distance = 0;
    Vector3 lastPos;

    void Awake()
    {
        this.lastPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.gameObject.GetComponentInParent<IHitable>();
        if(hitable!=null)
        {
            if(hitable.ID == this.hitter)
                return;
            hitable.GetHit();
        }

        Destroy(this.gameObject);
    }
}
