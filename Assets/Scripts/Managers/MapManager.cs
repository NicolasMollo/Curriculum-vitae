using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Managers {

    [DisallowMultipleComponent]
    public class MapManager : MonoBehaviour {
        //[SerializeField]
        private List<MapPiece> mapPieces = new List<MapPiece>();



        private void Start() {
            ManagePieces();
        }
        private void ManagePieces() {
            foreach (var mapPiece in mapPieces) {
                switch (mapPiece.pieceType) {
                    case MapPieceType.CornerPiece:
                        mapPiece.transform.position = Vector3.one * 2;

                        break;
                    case MapPieceType.OneBorderPiece:
                        mapPiece.transform.position = Vector3.one;

                        break;
                    case MapPieceType.CentralPiece:
                        mapPiece.transform.position = Vector3.zero;
                        break;
                    case MapPieceType.Last:
                        break;
                }
            }
        }


        //For debug
        private void Update() {
            Debug.Log($"MAP PIECES SUBSCRIBED: {mapPieces.Count}");

            //ManagePieces();
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