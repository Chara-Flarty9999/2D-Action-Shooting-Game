using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManagerVanish : MonoBehaviour
{
    /// <summary>���������ݒ�ł���B</summary>
    [SerializeField] liftmode liftmoving = default;
    int _liftmode;
    [SerializeField] bool playOnCollision = default;
    [SerializeField] float m_animSpeed = default;
    Animator m_anim = default;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        _liftmode = (int)liftmoving;
        Debug.Log(_liftmode);
        m_anim.SetInteger("Liftmoving", _liftmode);
        
    }

    // Update is called once per frame
    void Update()
    {
        m_anim.SetBool("PlayOnCollision", playOnCollision);
        m_anim.speed = m_animSpeed;
/*        if (!playOnCollision)
        {
            playOnCollision = true;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playOnCollision == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                playOnCollision = false;
            }
        }
    }

    private void Reseter()
    {
        playOnCollision = true;
        m_anim.SetBool("Reset?", true);
    }

    private void Starter()
    {
        m_anim.SetBool("Reset?", false);
    }

    enum liftmode
    {
        /// <summary>
        /// ���ɓ����ď�����B
        /// </summary>
        Left,
        /// <summary>
        /// ����ɓ����B
        /// </summary>
        LeftUp,
        /// <summary>
        /// ��ɓ����B
        /// </summary>
        Up,
        /// <summary>
        /// �E��ɓ����B
        /// </summary>
        RightUp,
        /// <summary>
        /// �E�ɓ����B
        /// </summary>
        Right,
    }
}
