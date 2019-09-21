using UnityEngine;

public class InputDetector : MonoBehaviour
{
    BlackHoleEffect effect;

    private void Start()
    {
        effect = Camera.main.GetComponent<BlackHoleEffect>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 viewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            effect.SetBlackHoleBirthPoint(viewport);
            effect.life = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            effect.life = true;
        }
    }
}