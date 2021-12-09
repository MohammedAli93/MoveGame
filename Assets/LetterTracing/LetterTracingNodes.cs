using UnityEngine;
[SelectionBase]
public class LetterTracingNodes : MonoBehaviour
{
    
    [HideInInspector]
    public Vector3[] localNodes = new Vector3[1];
    public Vector3[] worldNode {  get { return m_WorldNode; } }
    protected Vector3[] m_WorldNode;

    public int Index;
   

    private void Awake()
    {
        m_WorldNode = new Vector3[localNodes.Length];
        for (int i = 0; i < m_WorldNode.Length; ++i)
        {
            m_WorldNode[i] = transform.TransformPoint(localNodes[i]);
        }
    }

    public void IncIndex()
    {
        Index++;
    }
}