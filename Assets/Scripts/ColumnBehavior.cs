using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnBehavior : MonoBehaviour
{
    [SerializeField] public int _hp = 3;
    // Start is called before the first frame update
    [SerializeField] public GameObject _column1;
    [SerializeField] public GameObject _column2;
    [SerializeField] public GameObject _column3;

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "weapon")
        {
            _hp--;
            ActiveLayer();
        }
    }

    private void ActiveLayer() 
    {
        if (_hp == 2)
        {
            _column1.GetComponent<SpriteRenderer>().enabled = false;
            _column2.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (_hp ==1)
        {
            _column2.GetComponent<SpriteRenderer>().enabled = false;
            _column3.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (_hp ==0)
        {
            gameObject.SetActive(false);
        }
    }
}
