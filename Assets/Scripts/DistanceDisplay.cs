using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class DistanceDisplay : MonoBehaviour
{
    [SerializeField] Text goalDistance;
    [SerializeField] Text subGoalDistance;

    [SerializeField] Transform player;

    Goal goal;
    Station[] stations;

    private void Start()
    {
        goal = FindObjectOfType<Goal>();
        stations = FindObjectsOfType<Station>();
    }

    void UpdateStations()
    {
        stations = FindObjectsOfType<Station>();
    }

    private void FixedUpdate()
    {
        var distanceToGoal = Vector3.Distance(player.position, goal.transform.position);
        goalDistance.text = $"Distance: {Mathf.CeilToInt(distanceToGoal)}";

        var closestStationDistance = float.MaxValue;

        foreach (var station in stations)
        {
            if (!station)
            {
                UpdateStations();
            }

            if (!station.HasPower)
                continue;
            closestStationDistance = Mathf.Min(closestStationDistance, Vector3.Distance(player.position, station.transform.position));
        }
        subGoalDistance.text = $"Distance: {Mathf.CeilToInt(closestStationDistance)}";
    }
}
