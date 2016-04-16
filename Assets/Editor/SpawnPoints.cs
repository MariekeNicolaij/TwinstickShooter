using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EnemyManager))]
public class SpawnPoints : Editor
{
    EnemyManager self;
    int count;


    void Awake()
    {
        self = (EnemyManager)target;
        count = self.spawnPoints.Count;
    }

    void OnInspectorGUI()
    {
        if (count < self.spawnPoints.Count)
        {
            for (int i = count; i < self.spawnPoints.Count; i++)
                self.spawnPoints[i] = self.transform.position + new Vector3(5, 0, 5);
            count = self.spawnPoints.Count;
        }
        else if (count > self.spawnPoints.Count)
            count = self.spawnPoints.Count;
        base.OnInspectorGUI();
        EditorUtility.SetDirty(self);
    }

    void OnSceneGUI()
    {
        if (self.spawnPoints.Count != 0)
            for (int i = 0; i < self.spawnPoints.Count; i++)
                self.spawnPoints[i] = Handles.PositionHandle(self.spawnPoints[i], Quaternion.identity);
    }
}
