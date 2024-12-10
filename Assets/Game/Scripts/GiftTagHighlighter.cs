using UnityEngine;

public class GiftTagHighlighter : MonoBehaviour
{
    public Color gizmoColor = Color.yellow; // Color for the gizmos
    public float gizmoSize = 0.5f; // Size of the gizmo spheres

    private void OnDrawGizmos()
    {
        // Find all objects with the tag "GiftTag"
        GameObject[] gifts = GameObject.FindGameObjectsWithTag("GiftTag");

        // Draw a sphere around each gift
        Gizmos.color = gizmoColor;
        foreach (GameObject gift in gifts)
        {
            if (gift != null && gift.activeInHierarchy)
            {
                Gizmos.DrawSphere(gift.transform.position, gizmoSize);
            }
        }
    }
}
