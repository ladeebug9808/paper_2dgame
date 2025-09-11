using UnityEngine;

public class debugVisual : MonoBehaviour
{
    private Material debugMaterial;

    public bool editorOnly = true;
    public bool forgetIfEditorOnly;

    private void Start()
    {

        if (editorOnly)
        {
            gameObject.SetActive(false);
        }


        if (editorOnly && forgetIfEditorOnly)
        {
            Destroy(gameObject);
            return;
        }

        
        if (debugMaterial == null)
        {
            debugMaterial = Resources.Load<Material>("debugVisual");
        }

        
        Renderer rend = GetComponent<Renderer>();
       

        
        if (rend.sharedMaterial != debugMaterial)
        {
            rend.sharedMaterial = debugMaterial;
        }
    }
}
