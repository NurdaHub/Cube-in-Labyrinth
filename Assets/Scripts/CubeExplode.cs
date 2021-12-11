using UnityEngine;

public class CubeExplode : MonoBehaviour
{
    [SerializeField] private Material cubeMaterial;
    
    private Vector3 cubePosition;
    private Vector3 cubesPivot;
    private int cubesInRow = 4;
    private float cubesPivotDistance;
    private float cubePiecesSize = 0.2f;
    private float explosionForce = 40f;
    private float explosionRadius = 4f;
    private float explosionUpward = 0.4f;
    
    private void Start() 
    {
        cubesPivotDistance = cubePiecesSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    public void Explode(Transform cubeTransform)
    {
        cubePosition = cubeTransform.position;
        
        for (int x = 0; x < cubesInRow; x++) 
        {
            for (int y = 0; y < cubesInRow; y++) 
            {
                for (int z = 0; z < cubesInRow; z++) 
                {
                    CreateSmallPieces(x, y, z);
                }
            }
        }
        
        Collider[] colliders = Physics.OverlapSphere(cubePosition, explosionRadius);
        
        foreach (Collider hit in colliders) 
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
            if (rb != null)
                rb.AddExplosionForce(explosionForce, cubePosition, explosionRadius, explosionUpward);
        }
    }

    private void CreateSmallPieces(int x, int y, int z) 
    {
        var smallPiece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        smallPiece.transform.position = cubePosition + new Vector3(cubePiecesSize * x, cubePiecesSize * y, cubePiecesSize * z) - cubesPivot;
        smallPiece.transform.localScale = new Vector3(cubePiecesSize, cubePiecesSize, cubePiecesSize);
        smallPiece.transform.parent = transform;

        smallPiece.GetComponent<Renderer>().material = cubeMaterial;
        smallPiece.AddComponent<Rigidbody>();
        smallPiece.GetComponent<Rigidbody>().mass = cubePiecesSize;
    }
}