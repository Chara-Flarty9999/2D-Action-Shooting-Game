using UnityEngine;

public class Blaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        new MoveInfo(transform.position, 5);
    }

    // Update is called once per frame
    void Update()
    {

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
}
