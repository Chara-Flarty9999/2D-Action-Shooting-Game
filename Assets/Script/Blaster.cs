using DG.Tweening;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    SpriteRenderer renderer;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        renderer.color -= new Color(0f, 0f, 0f, 255f);
        new MoveInfo(transform.position, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BlasterCharge(StartInfo startInfo,MoveInfo moveInfo,float size,int waittime, int looplong, BlasterColor color)
    {
        this.transform.position = startInfo.vector2;
        

        

    }

    public struct MoveInfo
    {
        public Vector2 vector2;
        public int di;

        public MoveInfo(Vector2 vector2, int di)
        {
            this.vector2 = vector2;
            this.di = di;
        }
    }

    public struct StartInfo
    {
        public float x;
        public float y;
        public Vector2 vector2;
        public int di;

        public StartInfo(Vector2 vector2, int di)
        {
            this.vector2 = vector2;
            this.di = di;
            this.x = vector2.x;
            this.y = vector2.y;
        }

        public StartInfo(float x, float y, int di)
        {
            this .x = x;
            this .y = y;
            this .di = di;
            this .vector2 = new Vector2(x,y);
        }
    }

    public enum BlasterColor
    {
        white,
        orange,
        blue
        
    }
}
