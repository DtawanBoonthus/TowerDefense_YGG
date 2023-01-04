using TowerDefense.Manager.MapManager.Runtime;
using TowerDefense.Manager.TowerManager.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Manager.MapManager.MonoEditor
{
    public sealed class BoxBuildInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private TurretManagerSO turretManager;
        [SerializeField] private MapDataSO mapData;
        [SerializeField] private BoxMap boxMap;
        [SerializeField] private Color colorSelectNotTower;
        [SerializeField] private Color colorSelectHaveTower;
        
        private Renderer[] renderers;
        private Color[] colorDefault;
        private GameObject turret;
        
        private void Awake()
        {
            renderers = this.GetComponentsInChildren<Renderer>();
            
            colorDefault = new Color[renderers.Length];
            
            for (int i = 0; i < renderers.Length; i++)
            {
                colorDefault[i] = renderers[i].material.color;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeColorSelect();

            mapData.BoxSelect = this.boxMap;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = colorDefault[i];
            }

            mapData.BoxSelect = null;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var boxMapHaveTower = boxMap.HaveTower;
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (boxMapHaveTower) return;

                turretManager.BuildTurret(turretManager.CurrentTurret, this.transform, out boxMapHaveTower,
                    out turret);
                boxMap.HaveTower = boxMapHaveTower;
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!boxMapHaveTower) return;
                turretManager.RemoveTurret(turret, out boxMapHaveTower);
                boxMap.HaveTower = boxMapHaveTower;
            }
            else
            {
                return;
            }
            
            ChangeColorSelect();
        }
        
        private void ChangeColorSelect()
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = boxMap.HaveTower ? colorSelectHaveTower : colorSelectNotTower;
            }
        }
    }
}