using UnityEngine;

[System.Serializable]
public class Path : MonoBehaviour {

    public bool debug = true;
    public float radius;

    [SerializeField]
    private Vector3[] waypoints;
    public float PathLength { get { return waypoints.Length; } }

    public Vector3 GetPoint(int index)
    {
        return waypoints[index];
    }

    private void OnDrawGizmos()
    {
        if (!debug)
            return;

        for(int i = 0; i < waypoints.Length; i++)
        {
            if (i + 1 < waypoints.Length)
                Debug.DrawLine(waypoints[i], waypoints[i + 1], Color.red);
        }
    }
}
