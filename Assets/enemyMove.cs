using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    [SerializeField] private GameObject fireBallPrefab;
    private GameObject _fireball;
    private bool _alive;
    float wallMinDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _alive = true;

    }
    

    // Update is called once per frame
    void Update()
    {
        if (!_alive) return;
        transform.Translate(0, 0, 0.01f);
        RaycastHit hit;
        if (_fireball == null) {
            _fireball = Instantiate(fireBallPrefab) as GameObject;
            _fireball.transform.position =
            transform.TransformPoint(Vector3.forward * 1.5f);
            _fireball.transform.rotation = transform.rotation;
        }
        
        if (Physics.SphereCast(transform.position, 0.3f, transform.position+(new Vector3(0,0,1)), out hit))
        {
            
                if (hit.distance < wallMinDistance) {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }

    }
    public void SetAlive(bool a)
    {
        this._alive = a;
    }
}
