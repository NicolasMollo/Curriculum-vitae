using UnityEngine;
using Map;

[CreateAssetMenu(menuName = "Create scriptable object/Map piece")]
public class DatabaseMapPiece : ScriptableObject {

    [Header("MAP PIECE DATA")]
    [SerializeField]
    [Tooltip("Type of map piece")]
    private MapPieceType pieceType = MapPieceType.Last;
    public MapPieceType PieceType {
        get { return pieceType; }
    }

    [SerializeField]
    [Tooltip("Color of map piece base")]
    private Color[] colors = null;



    #region API
    public Color GetColorAtIndex(int _index) {
        if (_index < 0 || _index > colors.Length - 1) {
            return Color.white;
        }

        return colors[_index];
    }


    public Color GetRandomColor() {
        return colors[Random.Range(0, colors.Length - 1)];
    }
    #endregion
}