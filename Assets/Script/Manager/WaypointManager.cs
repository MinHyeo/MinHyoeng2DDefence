using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    private SplineContainer _splineContainer;
    private List<Vector3> _waypoints = new List<Vector3>();

    private void Awake()
    {
        Instance = this;
    }

    public void SetWayPoint()
    {
        Debug.Log("spline 호출");
        _splineContainer = StageManager.Instance.GetTilemap().GetComponent<SplineContainer>();
        if (_splineContainer == null)
            return;

        var spline = _splineContainer.Spline;

        foreach (var knot in spline.Knots)
        {
            _waypoints.Add(knot.Position);
        }
    }

    public List<Vector3> GetWaypoints()
    {
        return _waypoints ;
    }
}
