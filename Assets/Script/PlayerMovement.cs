using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    float invTime = 3f;
    [SerializeField] float m_speed = 5f;
    Rigidbody2D m_rb2d;
    float h;
    float v;
    // Start is called before the first frame update
    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X) == true)
        {
            m_speed = 2.5f;
        }
        else
        {
            m_speed = 7f;
        }
            // …•½•ûŒü‚Ì“ü—Í‚ğŒŸo‚·‚é
            h = Input.GetAxisRaw("Horizontal");
            //Debug.Log(h);
            v = Input.GetAxisRaw("Vertical");
            //Debug.Log(v);
        // “ü—Í‚É‰‚¶‚Äƒpƒhƒ‹‚ğ…•½•ûŒü‚É“®‚©‚·
        m_rb2d.velocity = new Vector3(h * m_speed, v * m_speed);

        if (transform.position.x <= -11.95f) 
        {
            transform.position = new Vector2(-11.95f,this.transform.position.y);
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
