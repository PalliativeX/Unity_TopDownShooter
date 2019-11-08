using UnityEngine;

public class Crosshairs : MonoBehaviour
{
    public LayerMask targetMask;
    public SpriteRenderer dot;
    public Color dotHighlightColor;
    Color originalDotColor;

	Transform player;

    private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Cursor.visible = false;
        originalDotColor = dot.color;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * 40 * Time.deltaTime);

		if (Input.GetKeyDown(KeyCode.Escape) && player != null)
		{
			Cursor.visible = (!Cursor.visible ? true : false);
		}
		if (player == null)
			Cursor.visible = true;
    }

    public void DetectTargets(Ray ray)
    {
        if (Physics.Raycast(ray, 100, targetMask))
		{
            dot.color = dotHighlightColor;
        }
		else
		{
            dot.color = originalDotColor;
        }
    }

}
