using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI
{
    [CustomEditor(typeof(Path))]
    public class PathEditor : Editor
    {
        private Path pathRef;

        private bool showGizmos = true;
        private Color gizmoColor = Color.blue;
        private bool moveGizmo = false;

        private void OnEnable()
        {
            pathRef = (Path)target;
        }

        private void OnSceneGUI()
        {
            if (showGizmos)
            {
                if (pathRef == null)
                {
                    pathRef = (Path)target;
                }

                Vector3[] waypoints = pathRef.waypoints;

                if(waypoints.Length >= 4)
                {
                    Vector3 position = pathRef.transform.position;
                    
                    for (int i = 0; i < waypoints.Length; i++)
                    {
                        Handles.color = gizmoColor;

                        Vector3 waypointPosition = waypoints[i] + position;
                        Vector3 nextWaypointPosition = (i < waypoints.Length - 1) ? waypoints[i + 1] + position : waypoints[0] + position;

                        Handles.DrawLine(waypointPosition, nextWaypointPosition);
                        Handles.DrawDottedLine(waypointPosition, waypointPosition + (Vector3.up * 20f), 2f);
                        Handles.DrawWireCube(waypointPosition, new Vector3(0.5f, 0.5f, 0.5f));

                        Handles.DrawWireDisc(waypointPosition, Vector3.up, pathRef.waypointRadius);

                        if (moveGizmo)
                        {
                            Vector3 newPosition = Handles.PositionHandle(waypointPosition, Quaternion.identity);
                            if (newPosition != waypointPosition)
                            {
                                Undo.RecordObject(pathRef, "Move Waypoint");
                                pathRef.MoveWaypoint(i, newPosition - position);
                            }
                        }

                        Handles.Label(waypointPosition - Vector3.up, i.ToString());
                    }
                }
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Gizmo Section", EditorStyles.boldLabel);
            showGizmos = EditorGUILayout.Toggle("Show gizmos", showGizmos);
            if (showGizmos)
            {
                gizmoColor = EditorGUILayout.ColorField("Gizmo color", gizmoColor);
                EditorGUILayout.Space();
                if (!EditorApplication.isPlaying)
                {
                    EditorGUILayout.LabelField("Move waypoints with gizmo", EditorStyles.boldLabel);
                    moveGizmo = EditorGUILayout.Toggle("Move gizmo", moveGizmo);
                }
                else
                {
                    moveGizmo = false;
                }
            }
        }
    }
}

