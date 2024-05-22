using Photon.Pun;
using UnityEngine;

public class ObstaculoVision : MonoBehaviour
{
    public Color miColor;
    [Range(0f, 1f)]
    public float alpha = 1.0f;
    public float opaco = 1.0f;
    public float transparente = 0.3f;
    public Camera cam;

    public Material[] materiales;
    public Color[] colores;
    public GameObject objetoDetectado;  // Almacena el objeto actualmente detectado por el raycast

    private void Start()
    {
        // Inicializa los arreglos para colores originales y colores modificados
        materiales = GetComponent<MeshRenderer>().materials;
        colores = new Color[materiales.Length];


    }

    private void Update()
    {
        
        // Realiza el raycast desde la cámara al jugador
        Ray ray = new Ray(cam.transform.position, this.transform.position - cam.transform.position);
        Debug.DrawRay(cam.transform.position, this.transform.position - cam.transform.position, Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Verifica si el raycast ha golpeado un objeto con MeshRenderer
            MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                // Si el objeto detectado cambia, llama a la función para restaurar el modo de renderizado
                if (objetoDetectado != null && objetoDetectado != hit.collider.gameObject)
                {

                    RestaurarModoRenderizado(objetoDetectado);
                }

                // Guarda el nuevo objeto detectado
                objetoDetectado = hit.collider.gameObject;

                // Obtiene todos los materiales del objeto impactado por el raycast
                materiales = meshRenderer.materials;

                // Ajusta la transparencia de todos los materiales en el bucle for
                for (int i = 0; i < materiales.Length; i++)
                {
                    // Lee el modo de renderizado del material y cambia a transparente si es opaco
                    if (materiales[i].renderQueue == (int)UnityEngine.Rendering.RenderQueue.Geometry)
                    {
                        materiales[i].SetOverrideTag("RenderType", "Transparent");
                        materiales[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        materiales[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        materiales[i].SetInt("_ZWrite", 0);
                        materiales[i].DisableKeyword("_ALPHATEST_ON");
                        materiales[i].EnableKeyword("_ALPHABLEND_ON");
                        materiales[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        materiales[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                    }
                    alpha = transparente;



                }
                for (int i = 0; i < materiales.Length; i++)
                {

                    colores[i] = materiales[i].GetColor("_Color");
                    colores[i].a = alpha;
                    Debug.Log(colores[i]);
                }


            }
        }
    }

    private void RestaurarModoRenderizado(GameObject objeto)
    {
        Debug.Log("ENtro al metodo");
        // Restaura el modo de renderizado opaco y el valor de alpha a 1
        MeshRenderer meshRenderer = objeto.GetComponent<MeshRenderer>();
        Material[] originales = meshRenderer.materials;

        for (int i = 0; i < originales.Length; i++)
        {
            Debug.Log("Entro al foooor");
            originales[i].SetOverrideTag("RenderType", "");
            originales[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            originales[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            originales[i].SetInt("_ZWrite", 1);
            originales[i].DisableKeyword("_ALPHATEST_ON");
            originales[i].DisableKeyword("_ALPHABLEND_ON");
            originales[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            originales[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;




        }
        Debug.Log("Salgooooooo");
        objetoDetectado = null;
        alpha = transparente;
        meshRenderer.materials = originales;
    }
}

