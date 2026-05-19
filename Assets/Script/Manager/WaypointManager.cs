using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    [SerializeField] private SplineContainer _splineContainer;
    private List<Vector3> _waypoints = new List<Vector3>();

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
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
