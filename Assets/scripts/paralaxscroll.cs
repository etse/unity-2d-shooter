using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxscroll : MonoBehaviour
{
    private GameObject nebula1;
    private GameObject nebula2;
    private GameObject stars1_1;
    private GameObject stars1_2;
    private GameObject stars2_1;
    private GameObject stars2_2;

    private float nabula_speed = 0.05f;
    private float stars1_speed = 0.20f;
    private float stars2_speed = 0.30f;

    // Start is called before the first frame update
    void Start()
    {
        nebula1 = transform.Find("nebula_1").gameObject;
        nebula2 = transform.Find("nebula_2").gameObject;
        stars1_1 = transform.Find("stars_1_1").gameObject;
        stars1_2 = transform.Find("stars_1_2").gameObject;
        stars2_1 = transform.Find("stars_2_1").gameObject;
        stars2_2 = transform.Find("stars_2_2").gameObject;

        var nebulaSize = nebula1.GetComponent<SpriteRenderer>().bounds.size.y;
        var stars1Size = stars1_1.GetComponent<SpriteRenderer>().bounds.size.y;
        var stars2Size = stars2_1.GetComponent<SpriteRenderer>().bounds.size.y;

        setYPosition(nebula1, 0);
        setYPosition(nebula2, nebulaSize);
        setYPosition(stars1_1, 0);
        setYPosition(stars1_2, stars1Size);
        setYPosition(stars2_1, 0);
        setYPosition(stars2_2, stars2Size);
    }

    // Update is called once per frame
    void Update()
    {
        scrollBackgrounds(nebula1, nebula2, nabula_speed);
        scrollBackgrounds(stars1_1, stars1_2, stars1_speed);
        scrollBackgrounds(stars2_1, stars2_2, stars2_speed);
    }

    private void setYPosition(GameObject gameObject, float Y)
    {
        var vector = gameObject.transform.position;
        vector.y = Y;
        gameObject.transform.position = vector;
    }

    private void scrollBackgrounds(GameObject gb1, GameObject gb2, float speed)
    {
        var gb1IsAbove = gb1.transform.position.y > gb2.transform.position.y;
        var bgAbove = gb1IsAbove ? gb1 : gb2;
        var bgBelow = gb1IsAbove ? gb2 : gb1;

        bgAbove.transform.Translate(new Vector2(0, -speed * Time.deltaTime * 6));
        var height = bgAbove.GetComponent<SpriteRenderer>().bounds.size.y;
        setYPosition(bgBelow, bgAbove.transform.position.y - height);
        moveAboveIfOutsideCamera(bgBelow, bgAbove);
    }

    private void moveAboveIfOutsideCamera(GameObject background, GameObject other)
    {
        var bottomOfScreen = Camera.main.ViewportToWorldPoint(-Vector3.one).y;
        var height = other.GetComponent<SpriteRenderer>().bounds.size.y;
        var position = background.transform.position.y;

        if (position+(height/2) < bottomOfScreen)
        {
            setYPosition(background, other.transform.position.y + height);
        }
    }
}
