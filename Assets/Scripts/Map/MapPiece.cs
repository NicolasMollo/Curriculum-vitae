using UnityEngine;

namespace Map {

    public enum MapPieceType : byte { 
        CornerPiece,
        OneBorderPiece,
        CentralPiece,
        Last
    }

    [DisallowMultipleComponent]
    public class MapPiece : MonoBehaviour {

        [SerializeField]
        private DatabaseMapPiece database = null;
        [SerializeField]
        [Tooltip("Meshrenderer of the base of the map piece")]
        private MeshRenderer baseMeshRenderer = null;
        [HideInInspector]
        public MapPieceType pieceType = MapPieceType.Last;


        private void Start() {
            VariablesAssignment();
        }
        private void VariablesAssignment() {
            pieceType = database.PieceType;
            baseMeshRenderer.material.color = database.GetRandomColor();
        }

    }

}