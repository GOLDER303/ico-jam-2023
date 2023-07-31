using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ObstaclePartSO : ScriptableObject
{
    public Mesh mesh;
    public Material material;
    public PlayerShapeSO fittingPlayerShapeSO;
}
