using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkDescriptor : MonoBehaviour
{

    public Transform Fork;
    public Transform[] Rails;
    private Bounds[] railBounds;

    private void Awake()
    {
        railBounds = new Bounds[Rails.Length];

        for (int i = 0; i < Rails.Length; i++)
        {
            Mesh mesh = Rails[i].GetComponent<MeshFilter>().mesh;

            railBounds[i] = mesh.bounds;
        }

    }

    public float GetRailHeight()
    {
        float sum = 0;
        for (int i = 0; i < railBounds.Length; i++)
        {
            sum += railBounds[i].extents.y * 2;
        }
        return sum;
    }

}
