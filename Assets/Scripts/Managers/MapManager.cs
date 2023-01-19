using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Managers {

    [DisallowMultipleComponent]
    public class MapManager : MonoBehaviour {

        private List<MapPiece> mapPieces = new List<MapPiece>();
        private Quaternion rotZero;
        private Quaternion rotNinety;
        private Quaternion rotNinetyNeg;
        private Quaternion rotHunEighty;

        [Header("MAP REFERENCES AND ATTRIBUTES")]
        [SerializeField]
        [Tooltip("Empty object that will contain the map pieces in scene")]
        private GameObject map = null;
        [SerializeField]
        [Tooltip("Number of map pieces in one line (z-axis)")]
        [Range(2, 10)]
        private int mapRow = 0;
        [SerializeField]
        [Tooltip("Number of map pieces in one line (x-axis)")]
        [Range(2, 10)]
        private int mapColumn = 0;

        [Header("MAP PREFABS")]
        [SerializeField]
        [Tooltip("Edgeless piece prefab")]
        private MapPiece centralPiecePrefab = null;
        [SerializeField]
        [Tooltip("Prefab of the piece with an edge")]
        private MapPiece oneBorderPiecePrefab = null;
        [SerializeField]
        [Tooltip("Prefab of the piece with two edges")]
        private MapPiece cornerPiecePrefab = null;


        #region Life cycle
        private void Start() {
            VariablesAssignment();
            CreateMap();
            PlaceMapPieces();
        }
        private void VariablesAssignment() {
            rotZero = Quaternion.Euler(0, 0, 0);
            rotNinety = Quaternion.Euler(0, 90, 0);
            rotNinetyNeg = Quaternion.Euler(0, -90, 0);
            rotHunEighty = Quaternion.Euler(0, 180, 0);
        }
        private void CreateMap() {

            MapPiece temporary = null;

            for (int i = 0; i < mapColumn; i++) {
                for (int j = 0; j < mapRow; j++) {
                    if (i == 0 && j == 0) {
                        temporary = CreateMapPiece(MapPieceType.CornerPiece, rotZero);
                    }
                    else if (i == 0 && j == (mapRow - 1)) {
                        temporary = CreateMapPiece(MapPieceType.CornerPiece, rotNinety);
                    }
                    else if (i == (mapColumn - 1) && j == (mapRow - 1)) {
                        temporary = CreateMapPiece(MapPieceType.CornerPiece, rotHunEighty);
                    }
                    else if (i == (mapColumn - 1) && j == 0) {
                        temporary = CreateMapPiece(MapPieceType.CornerPiece, rotNinetyNeg);
                    }
                    else if (j == 0 || j == (mapRow - 1)) {
                        temporary = CreateMapPiece(MapPieceType.OneBorderPiece, j == (mapRow - 1) ? rotHunEighty : rotZero);
                    }
                    else if (i == 0 || i == (mapColumn - 1)) {
                        temporary = CreateMapPiece(MapPieceType.OneBorderPiece, i == (mapColumn - 1) ? rotNinetyNeg : rotNinety);
                    }
                    else {
                        temporary = CreateMapPiece(MapPieceType.CentralPiece, rotZero);
                    }
                }
            }

        }
        private void PlaceMapPieces() {
            float xHalfScale = mapPieces[0].transform.localScale.x;
            int lastIndexZ = 0;

            for (int i = 0; i < mapPieces.Count; i++) {
                if (i == 0) {
                    continue;
                }

                if (i % mapRow == 0) {
                    mapPieces[i].transform.position = new Vector3(mapPieces[lastIndexZ].transform.position.x + (xHalfScale * 5), mapPieces[i].transform.position.y, mapPieces[i].transform.position.z);
                    lastIndexZ += mapRow;
                }
                else {
                    mapPieces[i].transform.position = new Vector3(mapPieces[lastIndexZ].transform.position.x, mapPieces[i].transform.position.y, mapPieces[i - 1].transform.position.z + (xHalfScale * 5));
                }

            }
        }
        private MapPiece CreateMapPiece(MapPieceType _pieceType, Quaternion _rotation) {

            MapPiece mapPiece = null;

            switch (_pieceType) {
                case MapPieceType.CornerPiece:
                    mapPiece = Instantiate(cornerPiecePrefab);
                    break;
                case MapPieceType.OneBorderPiece:
                    mapPiece = Instantiate(oneBorderPiecePrefab);
                    break;
                case MapPieceType.CentralPiece:
                    mapPiece = Instantiate(centralPiecePrefab);
                    break;
            }

            mapPiece.transform.SetParent(map.transform);
            mapPieces.Add(mapPiece);
            mapPiece.transform.position = Vector3.zero;
            mapPiece.transform.rotation = _rotation;

            return mapPiece;

        }
        #endregion




        //For debug
        private void Update() {
            Debug.Log($"MAP PIECES SUBSCRIBED: {mapPieces.Count}");
        }



        #region API
        public void SubscribeToTheList(MapPiece _piece) {
            mapPieces.Add(_piece);
        }


        public void UnsubscribeFromTheList(MapPiece _piece) {
            mapPieces.Remove(_piece);
        }


        public void RemoveAllPiecesFromTheList() {
            foreach (var mapPiece in mapPieces) {
                mapPieces.Remove(mapPiece);
            }

        }
        #endregion

    }

}