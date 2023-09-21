using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLine : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    private float timeLeft;

    [SerializeField]
    private LineRenderer line;

    private void Update()
    {
        if (timeLeft > 0)
            timeLeft -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    public void Enable(Vector2 point)
    {
        timeLeft = lifeTime;
        gameObject.SetActive(true);

        line.SetPosition(1, (Vector3)point);
    }
}
