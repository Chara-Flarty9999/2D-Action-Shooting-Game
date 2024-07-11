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
    [SerializeField] float _gravityDrag = .8f;

    public static float highJump = 1;
    Rigidbody2D _rb = default;
    /// <summary>�ڒn�t���O</summary>
    bool _isGrounded = false;
    Vector3 _initialPosition = default;
    Animator _anim = default;
    /// <summary>�����Ă���A�C�e���̃��X�g</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    // Start is called before the first frame update
    void Start()
    {
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
        float h = Input.GetAxis("Horizontal");
        Vector2 velocity = _rb.velocity;   // ���̕ϐ� velocity �ɑ��x���v�Z���āA�Ō�� Rigidbody2D.velocity �ɖ߂�

        if (h != 0)
        {
            velocity.x = h * _moveSpeed;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isGrounded = false;
            velocity.y = _jumpSpeed * highJump;
        }
        else if (!Input.GetButton("Jump") && velocity.y > 0)
        {
            // �㏸���ɃW�����v�{�^���𗣂�����㏸����������
            velocity.y *= _gravityDrag;
        }

        _rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGrounded = false;
    }
}
