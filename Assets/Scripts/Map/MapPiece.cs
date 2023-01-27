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
        [Tooltip("Mesh Collider of the base of the map piece")]
        private MeshCollider baseCollider = null;

        public float colliderSize {
            get { return baseCollider.bounds.size.x; }
        }

        [SerializeField]
        [Tooltip("Mesh Renderer of the base of the map piece")]
        public MeshRenderer baseMeshRenderer = null;

    }

}