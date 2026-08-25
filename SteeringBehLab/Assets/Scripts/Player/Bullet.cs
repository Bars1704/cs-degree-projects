using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var killable = col.gameObject.GetComponent<IKillable>();

        killable?.Kill();
        gameObject.SetActive(false);
    }
}