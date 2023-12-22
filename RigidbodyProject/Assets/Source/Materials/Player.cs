using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButton(0))
            {
                if (hit.transform.TryGetComponent(out Door door))
                {
                    door.Open();
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (hit.transform.TryGetComponent(out Door door))
                {
                    door.Close();
                }
            }

        }
    }
}
