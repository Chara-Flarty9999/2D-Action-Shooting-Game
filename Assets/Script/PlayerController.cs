using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>�ړ����x</summary>
    [SerializeField] float _moveSpeed = 8f;
    /// <summary>�W�����v���x</summary>
    [SerializeField] float _jumpSpeed = 5f;
    /// <summary>�W�����v���ɃW�����v�{�^���𗣂������̏㏸���x������</summary>
    [SerializeField] float _gravityDrag = .2f;
    /// <summary>���˂���e�ۂ�Prefab�Ŏw�肷��B</summary>
    [SerializeField] GameObject _bullet;
    /// <summary>�v���C���[�w��</summary>
    [SerializeField] GameObject _player;
    /// <summary>�\�E����ԕύX����SE</summary>
    [SerializeField] AudioClip _changeSound;

    float h;
    float v;

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

    /// <summary>�A�C�e���擾���ɂ��o�t�̏�Ԃ��r�b�g�t���O�Ŕ���B</summary>
    Itemtype _soulState;

    public float rote;

    /// <summary>�\�E���̏�Ԃ��w��B�O������ύX���邱�Ƃɂ���ă\�E����Ԃ��ς��B
    /// �܂��A�G���A���SoulModeChanger��ݒu���邱�Ƃɂ���ăt�B�[���h��ł��\�E����Ԃ�ς����B</summary>
    [Tooltip("�\�E���̏�Ԃ�ݒ�ł���B�O������ł��ύX�\�B")]
    public SoulMode soul;
        
    Rigidbody2D _rb = default;
    SpriteRenderer _sprite = default;
    /// <summary>�ڒn�t���O</summary>
    bool _isGrounded = false;
    Vector3 _initialPosition = default;
    AudioSource _audioSource;
    //Animator _anim = default;
    /// <summary>�����Ă���A�C�e���̃��X�g</summary>
    List<ItemBase> _itemList = new List<ItemBase>();
    // Start is called before the first frame update


    private int soulMode { get; set; }
    void Start()
    {
        _player = GameObject.Find("Player");
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
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
        if(soul == SoulMode.blue) 
        {
            _sprite.color = new Color(0, 0, 1);
            _moveSpeed = 8f;

            _rb.gravityScale = 3;
            float h = Input.GetAxis("Horizontal");
            if (h > 0)
            {
                rote = 0;
            }
            else if (h < 0)
            {
                rote = 180;
            }
            Vector2 velocity = _rb.velocity;   // ���̕ϐ� velocity �ɑ��x���v�Z���āA�Ō�� Rigidbody2D.velocity �ɖ߂�

            if (h != 0)
            {
                velocity.x = h * _moveSpeed * highSpeed;
            }

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
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

        if(soul == SoulMode.red)
        {
            _sprite.color = new Color(1, 0, 0);
            _rb.gravityScale = 0;
            if (Input.GetKey(KeyCode.X) == true)
            {
                _moveSpeed = 2.5f;
            }
            else
            {
                _moveSpeed = 7f;
            }
            // ���������̓��͂����o����
            h = Input.GetAxisRaw("Horizontal");
            //Debug.Log(h);
            v = Input.GetAxisRaw("Vertical");
            //Debug.Log(v);
            // ���͂ɉ����ăp�h���𐅕������ɓ�����
            _rb.velocity = new Vector3(h * _moveSpeed, v * _moveSpeed);

            if (transform.position.x <= -11.95f)
            {
                transform.position = new Vector2(-11.95f, this.transform.position.y);
            }
            if (transform.position.x >= 11.95f)
            {
                transform.position = new Vector2(11.95f, this.transform.position.y);
            }
            if (transform.position.y <= -6.5f)
            {
                transform.position = new Vector2(this.transform.position.x, -6.5f);
            }
            if (transform.position.y >= 6.5f)
            {
                transform.position = new Vector2(this.transform.position.x, 6.5f);
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            if(!_isGrounded)
            {
                _isGrounded = true;
            }
        }

        if (collision.tag == "SoulChanger")
        {
            GameObject _soulChangerObject = collision.gameObject;
            SoulModeChanger _soulModeChanger = _soulChangerObject.GetComponent<SoulModeChanger>();
            if (_soulModeChanger._changeMode == soul)
            {

            }
            else
            {
                _audioSource.PlayOneShot(_changeSound);
            }
            soul = _soulModeChanger._changeMode;


            
        }
    }

    public enum SoulMode
    {
        red,
        blue,
    }

    public enum Itemtype
    {
        None = 0b0000,
        Highspeed = 0b0001,
        Levitation = 0b0010,
        Highjump = 0b0100,
        Invincible = 0b1000,
    }
}
