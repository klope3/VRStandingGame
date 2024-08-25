using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SaberTrail : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private int frameCount;
    [SerializeField] private Transform saberBase;
    [SerializeField] private Transform saberTip;
    private SlidingWindow<Vector3[]> saberPositions; //a sliding window of Vector3 pairs, where each pair stores a saber base position at index 0 and a saber tip position at index 1
    private readonly int VERTS_PER_FRAME = 6;

    private void Awake()
    {
        saberPositions = new SlidingWindow<Vector3[]>(frameCount);
    }

    private void Update()
    {
        saberPositions.Push(new Vector3[] { saberBase.position, saberTip.position });
        Draw();
    }

    private void FillTriIndices(int[] tris)
    {
        for (int i = 0; i < tris.Length; i++)
        {
            tris[i] = i;
        }
    }

    public void Draw()
    {
        Mesh mesh = new Mesh();
        List<Vector3[]> positions = saberPositions.GetElements();
        if (positions.Count < 2) return;

        Vector3[] verts = new Vector3[(positions.Count - 1) * VERTS_PER_FRAME];
        int[] tris = new int[(positions.Count - 1) * VERTS_PER_FRAME];

        for (int i = 0; i < positions.Count - 1; i++)
        {
            Vector3 base1 = transform.InverseTransformPoint(positions[i][0]);
            Vector3 tip1 = transform.InverseTransformPoint(positions[i][1]);
            Vector3 base2 = transform.InverseTransformPoint(positions[i + 1][0]);
            Vector3 tip2 = transform.InverseTransformPoint(positions[i + 1][1]);

            int startIndex = i * VERTS_PER_FRAME;
            verts[startIndex] = base1;
            verts[startIndex + 1] = tip1;
            verts[startIndex + 2] = tip2;
            verts[startIndex + 3] = base1;
            verts[startIndex + 4] = tip2;
            verts[startIndex + 5] = base2;
        }

        FillTriIndices(tris);

        mesh.vertices = verts;
        mesh.triangles = tris;
        meshFilter.mesh = mesh;
    }

    [Button]
    public void DoUpdate()
    {
        saberPositions.Push(new Vector3[] { saberBase.position, saberTip.position });
        Draw();
    }
}
