using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;

    private SplineContainer _splineContainer;
    private Dictionary<int, List<Vector3>> _waypointList = new Dictionary<int, List<Vector3>>();

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

        int index = 1;
        var splineList = _splineContainer.Splines;
        foreach(var spline in splineList)
        {
            _waypointList.Add(index, new List<Vector3>());
            foreach (var knot in spline.Knots)
            {
                _waypointList[index].Add(knot.Position);
            }
            index++;
        }
    }

    public void ResetWaypoint()
    {
        _waypointList.Clear();
    }

    public List<Vector3> GetWaypoints(int waveGroup)
    {
        return _waypointList[waveGroup];
    }
}
