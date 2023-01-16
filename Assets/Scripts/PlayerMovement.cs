using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigid;
    private Vector2 _direction;
    private bool _jump;
    private bool _attack;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    private int _jumpAmount = 0;
    [SerializeField] private int _maxJump;
    [SerializeField] private float _fallGravity;
    [SerializeField] private Animator _animator;
    [SerializeField] private IntVariables _pHP;
    [SerializeField] private GameObject _spawn;
    [SerializeField] [Range(0,3)] private float _radius;
    [SerializeField] [Range(0,10)] private float _offsetgroundcheck;
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private LayerMask _layermask;

    private void Awake()
    {
        _playerRigid = GetComponent<Rigidbody2D>();
        _jump = false;
        _pHP._value = 3;
        _hpCount.text = "HP : " + _pHP._value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) { _attack = true; _animator.SetBool("attack", true); }
        _direction.x = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _animator.SetFloat("movespeedx",Mathf.Abs(_direction.x));
        _animator.SetFloat("movespeedy", _direction.y);
        if (Input.GetButtonDown("Jump") && _jumpAmount < _maxJump)
        {
            _jump = true;
        }

        //groundchecker
        Vector3 posoffset = new Vector3(transform.position.x, transform.position.y - _offsetgroundcheck, transform.position.z);
        Collider2D floorCollider = Physics2D.OverlapCircle(posoffset, _radius, _layermask);

        if (floorCollider != null)
        {

            _animator.SetBool("ground", true);
            //_animator.SetTrigger("Grounded");
            _jumpAmount = 0;
            if (floorCollider.tag == "movingplatform")
            {
                transform.SetParent(floorCollider.transform);
            }
        }
        else
        {
            transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        //appliquer une gravité permanente
        _direction.y = _playerRigid.velocity.y;

        _playerRigid.velocity = _direction;
        if (_attack) { _attack= false; _animator.SetBool("attack", false); }
        if (_jump && _jumpAmount<_maxJump)
        {
            _jump = false;
            /*Vector2 jumpVector = new Vector2(_direction.x, _direction.y = _jumpForce);
            _playerRigid.AddForce(jumpVector);*/
            _direction.y = _jumpForce;
            _playerRigid.velocity = _direction * Time.fixedDeltaTime;
            _jumpAmount++;
            //_animator.SetBool("ground", false);
        }

        if (_playerRigid.velocity.y <0)
        {
            _playerRigid.gravityScale = _fallGravity;
        }
        else
        {
            _playerRigid.gravityScale = 1;
        }

        if (_direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),transform.localScale.y, transform.localScale.z);
        }
        else if (_direction.x >0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 posoffset = new Vector3(transform.position.x, transform.position.y - _offsetgroundcheck, transform.position.z);
        Gizmos.DrawWireSphere(posoffset, _radius);
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("floor"))
    //    {
    //        _animator.SetBool("ground", true);
    //        //_animator.SetTrigger("Grounded");
    //        _jumpAmount = 0;
    //    }
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("floor"))
        {
            _animator.SetBool("ground", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "exit")
        {
            if (SceneManager.GetActiveScene().buildIndex<1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "death")
        {
            _pHP._value--;
            _hpCount.text = "HP : " + _pHP._value;
            if (_pHP._value >0)
            {
                transform.position = _spawn.transform.position;
            }
            if (_pHP._value <=0)
            {
                //load la scene gameover
                SceneManager.LoadScene(2);
            }
        }
    }
}
