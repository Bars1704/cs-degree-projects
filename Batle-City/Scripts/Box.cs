using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Healka;
    public int HealDropRate = 3;


    private void OnDestroy()
    {
        int x = Random.Range(0, HealDropRate);
        if (x == 0)
        {
          Instantiate(Healka,gameObject.transform.position,gameObject.transform.rotation);
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Tank"))
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Tank"))
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
