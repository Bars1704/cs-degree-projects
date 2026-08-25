
using UnityEngine;

public class GamerScript : MonoBehaviour
{
    public float MovingSpeed = 1;
    public Rigidbody2D rb;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    void Update()
    {
        if (transform.rotation.z!=180&& transform.rotation.z != 90 && transform.rotation.z != 270 && transform.rotation.z != 0)
        {
            int x = (int)(transform.rotation.eulerAngles.z / 90);
            switch (x)
            {
                case 0:
                  transform.rotation = Quaternion.Euler(0, 0, 0);
                break; 
                case 1:
                  transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
                case 2:
                  transform.rotation = Quaternion.Euler(0, 0, 180);
                break; 
                case 3:
                  transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            }
            
        }
        if (Input.GetKey(down))
        {
            rb.MovePosition(rb.position + new Vector2(0, -1 * MovingSpeed * Time.deltaTime));
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKey(up))
        {
            rb.MovePosition(rb.position + new Vector2(0, 1 * MovingSpeed * Time.deltaTime));
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(left))
        {
            rb.MovePosition(rb.position + new Vector2(-1 * MovingSpeed * Time.deltaTime, 0));
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey(right))
        {
            rb.MovePosition(rb.position + new Vector2(1 * MovingSpeed * Time.deltaTime, 0));
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (Input.anyKey == false)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}