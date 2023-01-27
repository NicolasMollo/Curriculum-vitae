using UnityEngine;

[CreateAssetMenu(menuName = "Create ScriptableObject/DatabaseMapManager")]
public class DatabaseMapManager : ScriptableObject {

    [Header("MAP SIZE")]
    [SerializeField]
    [Tooltip("Number of map pieces in one line (z-axis)")]
    [Range(2, 10)]
    private int mapRow = 0;
    public int MapRow {
        get { return mapRow; }
    }

    [SerializeField]
    [Tooltip("Number of map pieces in one line (x-axis)")]
    [Range(2, 10)]
    private int mapColumn = 0;
    public int MapColumn {
        get { return mapColumn; }
    }

    [SerializeField]
    [Tooltip("Value that will be applied to the scale of the map pieces (all three axes)")]
    [Range(0.1f, 10f)]
    private float mapPiecesScale = 1f;
    public float MapPiecesScale {
        get { return mapPiecesScale; }
    }



    [Header("MAP MATERIALS")]
    [SerializeField]
    [Tooltip("Materials of map pieces (MAX 3 MATERIALS)")]
    private Material[] materials = new Material[3];
    public Material[] Materials {
        get { return materials; }
    }

    [SerializeField]
    private Material defaultMaterial = null;
    public Material DefaultMaterial {
        get { return defaultMaterial; }
    }

}