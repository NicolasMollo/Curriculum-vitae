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

        [Header("MAP OBJECT")]
        [SerializeField]
        [Tooltip("Empty object that will contain the map pieces in scene")]
        private GameObject map = null;

        [Header("MAP DATA")]
        [SerializeField]
        private DatabaseMapManager data = null;

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
            SetPrefabsScale(Vector3.one * data.MapPiecesScale);
            CreateMapPieces();
            SetMaterial();
            PlaceMapPieces();
        }
        private void VariablesAssignment() {
            rotZero = Quaternion.Euler(0, 0, 0);
            rotNinety = Quaternion.Euler(0, 90, 0);
            rotNinetyNeg = Quaternion.Euler(0, -90, 0);
            rotHunEighty = Quaternion.Euler(0, 180, 0);
        }


        private void CreateMapPieces() {

            for (int i = 0; i < data.MapColumn; i++) {
                for (int j = 0; j < data.MapRow; j++) {

                    if (i == 0 && j == 0) {
                        CreateMapPiece(MapPieceType.CornerPiece, rotZero);
                    }
                    else if (i == 0 && j == (data.MapRow - 1)) {
                        CreateMapPiece(MapPieceType.CornerPiece, rotNinety);
                    }
                    else if (i == (data.MapColumn - 1) && j == (data.MapRow - 1)) {
                        CreateMapPiece(MapPieceType.CornerPiece, rotHunEighty);
                    }
                    else if (i == (data.MapColumn - 1) && j == 0) {
                        CreateMapPiece(MapPieceType.CornerPiece, rotNinetyNeg);
                    }
                    else if (j == 0 || j == (data.MapRow - 1)) {
                        CreateMapPiece(MapPieceType.OneBorderPiece, j == (data.MapRow - 1) ? rotHunEighty : rotZero);
                    }
                    else if (i == 0 || i == (data.MapColumn - 1)) {
                        CreateMapPiece(MapPieceType.OneBorderPiece, i == (data.MapColumn - 1) ? rotNinetyNeg : rotNinety);
                    }
                    else {
                        CreateMapPiece(MapPieceType.CentralPiece, rotZero);
                    }

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

            mapPiece.transform.position = Vector3.zero;
            mapPiece.transform.rotation = _rotation;
            mapPiece.transform.SetParent(map.transform);
            mapPieces.Add(mapPiece);

            return mapPiece;

        }
        private void PlaceMapPieces() {

            float scaleOffset = mapPieces[0].colliderSize;
            int columnIndex = 0;

            for (int i = 0; i < mapPieces.Count; i++) {

                if (i == 0) {
                    continue;
                }

                if (i % data.MapRow == 0) {
                    mapPieces[i].transform.position = new Vector3(mapPieces[columnIndex].transform.position.x + scaleOffset,
                                                                  mapPieces[i].transform.position.y,
                                                                  mapPieces[i].transform.position.z);
                    columnIndex += data.MapRow;
                }
                else {
                    mapPieces[i].transform.position = new Vector3(mapPieces[columnIndex].transform.position.x,
                                                                  mapPieces[i].transform.position.y,
                                                                  mapPieces[i - 1].transform.position.z + scaleOffset);
                }

            }

        }


        private void SetMaterial() {

            if (data.Materials.Length == 0 || data.Materials.Length > 3) {
                SetMapPiecesMaterial(data.DefaultMaterial);
                return;
            }

            if (data.Materials.Length == 1) {
                SetMapPiecesMaterial(data.Materials[0]);
            }
            else if (data.Materials.Length == 2) {
                SetMapPiecesMaterial(data.Materials[0], data.Materials[1]);
            }
            else {
                SetMapPiecesMaterial(data.Materials[0], data.Materials[1], data.Materials[2]);
            }

        }
        private void SetMapPiecesMaterial(Material _material) {

            foreach (MapPiece mapPiece in mapPieces) {
                mapPiece.baseMeshRenderer.material = _material;
            }

        }
        private void SetMapPiecesMaterial(Material _firstMaterial, Material _secondMaterial) {

            Material temp = null;

            for (int i = 0; i < mapPieces.Count; i++) {

                if (data.MapRow % 2 == 0 && i % data.MapRow == 0) {
                    temp = _firstMaterial;
                    _firstMaterial = _secondMaterial;
                    _secondMaterial = temp;
                }

                mapPieces[i].baseMeshRenderer.material = i % 2 == 0 ? _firstMaterial : _secondMaterial;

            }

        }
        private void SetMapPiecesMaterial(Material _firstMaterial, Material _secondMaterial, Material _thirdMaterial) {

            Material temp = _firstMaterial;
            Material secondTemp = _secondMaterial;
            Material thirdTemp = _thirdMaterial;
            int nextMapRowValue = 0;

            for (int i = 0; i < mapPieces.Count; i++) {

                if (i == 0) {
                    mapPieces[i].baseMeshRenderer.material = _firstMaterial;
                    continue;
                }

                if (data.MapRow % 3 == 0 && i % data.MapRow == 0) {
                    nextMapRowValue = i + 1;
                    temp = _secondMaterial;
                    thirdTemp = _firstMaterial;
                    secondTemp = _thirdMaterial;
                }

                if (i == nextMapRowValue) {
                    temp = _firstMaterial;
                    secondTemp = _secondMaterial;
                    thirdTemp = _thirdMaterial;
                }


                mapPieces[i].baseMeshRenderer.material = mapPieces[i - 1].baseMeshRenderer.material.color == _firstMaterial.color ? secondTemp :
                                                         mapPieces[i - 1].baseMeshRenderer.material.color == _secondMaterial.color ? thirdTemp :
                                                         temp;

            }

        }


        private void OnDestroy() {
            SetPrefabsScale(Vector3.one);
        }
        private void SetPrefabsScale(Vector3 _scaleValue) {
            cornerPiecePrefab.transform.localScale = _scaleValue;
            oneBorderPiecePrefab.transform.localScale = _scaleValue;
            centralPiecePrefab.transform.localScale = _scaleValue;
        }
        #endregion

    }

}