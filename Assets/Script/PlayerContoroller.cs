using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour
{
    /// <summary>�ړ����x</summary>
    [SerializeField] float _moveSpeed = 3f;
    /// <summary>�W�����v���x</summary>
    [SerializeField] float _jumpSpeed = 5f;
    /// <summary>�W�����v���ɃW�����v�{�^���𗣂������̏㏸���x������</summary>
    [SerializeField] float _gravityDrag = .2f;
    /// <summary>���˂���e�ۂ�Prefab�Ŏw�肷��B</summary>
    [SerializeField] GameObject _bullet;
    /// <summary>�v���C���[�w��</summary>
    [SerializeField] GameObject _player;

    /// <summary>
    /// [�O���ύX�p]�W�����v�͏㏸�̃��[�g�B�{���Őݒ肷��B
    /// </summary>
    public static float highJumpRate = 1f;
    /// <summary>
    /// [�O���ύX�p]�ᑬ�����p�̃��[�g�B��{�I��3��1��OK�B
    /// </summary>
    public static float levitation = 3;
    /// <summary>
    /// [�O���ύX�p]�ړ����x�̃��[�g�B�{���Őݒ�B
    /// </summary>
    public static float highSpeed = 1;

    public float Rote;
        
    Rigidbody2D _rb = default;
    /// <summary>�ڒn�t���O</summary>
    bool _isGrounded = false;
    Vector3 _initialPosition = default;
    //Animator _anim = default;
    /// <summary>�����Ă���A�C�e���̃��X�g</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody2D>();
        _initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        // �A�C�e�����g��
        if (Input.GetButtonDown("Fire1"))
        {
            if (_itemList.Count > 0)
            {
                // ���X�g�̐擪�ɂ���A�C�e�����g���āA�j������
                ItemBase item = _itemList[0];
                _itemList.RemoveAt(0);
                item.Activate();
                Destroy(item.gameObject);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(_bullet, _player.transform.position,Quaternion.identity);
        }

        // ��ʊO�ɗ������珉���ʒu�ɖ߂�
        if (this.transform.position.y < -15)
        {
            this.transform.position = _initialPosition;
        }
    }

    public void GetItem(ItemBase item)
    {
        _itemList.Add(item);
    }

    void Movement()
    {
        _rb.gravityScale = 3;
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            Rote = 0;
        }
        else if (h < 0)
        {
            Rote = 180;
        }
        Vector2 velocity = _rb.velocity;   // ���̕ϐ� velocity �ɑ��x���v�Z���āA�Ō�� Rigidbody2D.velocity �ɖ߂�

        if (h != 0)
        {
            velocity.x = h * _moveSpeed * highSpeed;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            velocity.y = _jumpSpeed * highJumpRate;
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0)
        {
            // �㏸���ɃW�����v�{�^���𗣂�����㏸����������
            velocity.y *= _gravityDrag;
        }

        if (velocity.y < -1)
        {
            _rb.gravityScale = levitation;
        }

        _rb.velocity = velocity;
        //Debug.Log(_rb.velocity);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
}
