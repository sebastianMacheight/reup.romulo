using ReUpVirtualTwin.Behaviours;
using ReUpVirtualTwin.Helpers;
using ReUpVirtualTwin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ReUpVirtualTwin
{
    public class MaterialSelectorManager : MonoBehaviour, IListItemHandler
    {
        public TMP_InputField nameInputField;
        public Button addMaterialToReplaceButton;
        public Button addMaterialToSelectablesButton;
        public Button triggerButton;
        public GameObject materialSelectPanelPrefab;
        public GameObject pointSelectPanelPrefab;
        public TMP_Text materialToReplaceField;
        public GameObject selectableMaterialList;
        public GameObject materialItemPrefab;
        public TMP_Text triggerTextField;
        public GameObject triggerObjectPrefab;

        GameObject _materialSelectorPanel;
        SelectMaterial _materialSelector;
        Button _materialSelectPanelOkButton;

        GameObject _pointSelectorPanel;
        SelectPointPanel _pointSelector;
        Button _pointSelectorOkButton;

        string _materialSelectorName;
        GameObject _objectToReplaceMaterial;
        List<MaterialItem> _selectableMaterials;
        Trigger _trigger;
        IObjectPool _objectPool;
        Button _okButton;
        ExtensionsManager _extensionManager;
        GameObject _extensionsTriggersVessel;

        delegate void OkButtonOnClick();

        private void Start()
        {
            _selectableMaterials = new List<MaterialItem>();
            _objectPool = ObjectFinder.FindObjectPool();
            _extensionManager = ObjectFinder.FindExtensionManager();
            _extensionsTriggersVessel = ObjectFinder.FindExtensionsTrigger();
        }

        private void OnEnable()
        {
            addMaterialToReplaceButton.onClick.AddListener(() => DisplayMaterialSelector(DefineMaterialToReplace));
            addMaterialToSelectablesButton.onClick.AddListener(() => DisplayMaterialSelector(GetMaterialFromSelectMaterialPanel));
            triggerButton.onClick.AddListener(DisplayPointSelector);
            _okButton = ObjectFinder.FindOkButton(gameObject);
            _okButton.onClick.AddListener(CreateExtension);

            if (_trigger != null)
            {
                _trigger.triggerObject.SetActive(true);
            }
        }


        private void OnDisable()
        {
            addMaterialToReplaceButton.onClick.RemoveAllListeners();
            addMaterialToSelectablesButton.onClick.RemoveAllListeners();
            triggerButton.onClick.RemoveAllListeners();
            _okButton.onClick.RemoveAllListeners();

            if (_trigger != null)
            {
                _trigger.triggerObject.SetActive(false);
            }
        }

        void DisplayMaterialSelector(OkButtonOnClick DoSelectMaterial)
        {
            _materialSelectorPanel =  _objectPool.GetObjectFromPool(materialSelectPanelPrefab.name, transform);
            _materialSelector = _materialSelectorPanel.GetComponent<SelectMaterial>();
            _materialSelectPanelOkButton = ObjectFinder.FindOkButton(_materialSelectorPanel);

            _materialSelectPanelOkButton.onClick.AddListener(() => OnMaterialSelectOkButton(DoSelectMaterial));
        }

        void OnMaterialSelectOkButton(OkButtonOnClick DoSelectMaterial)
        {
            if (_materialSelector.selectedMaterialObject != null)
            {
                DoSelectMaterial();
            }
            _materialSelectPanelOkButton.onClick?.RemoveAllListeners();
            _objectPool.PoolObject(_materialSelectorPanel);
        }

        void DefineMaterialToReplace()
        {
            _objectToReplaceMaterial = _materialSelector.selectedMaterialObject;
            materialToReplaceField.text = _objectToReplaceMaterial.name;
            var material = _objectToReplaceMaterial.GetComponent<Renderer>()?.material;
            if (material != null )
            {
                AddToSelectablesMaterials(material);
            }
        }
        void ClearMaterialToReplace()
        {
            _objectToReplaceMaterial = null;
            materialToReplaceField.text = "";
        }

        void GetMaterialFromSelectMaterialPanel()
        {
            var material = _materialSelector.selectedMaterialObject.GetComponent<Renderer>().material;
            AddToSelectablesMaterials(material);
        }
        void AddToSelectablesMaterials(Material material)
        {
            if (material != null) 
            {
                var materialPanelItem = _objectPool.GetObjectFromPool(materialItemPrefab.name, selectableMaterialList.transform);
                // change name of materialItemInstance to diferentiate it in the object pool;
                materialPanelItem.name = material.name;
                var listItemInstance = materialPanelItem.GetComponent<IListItemInstance>();
                listItemInstance.itemHandler = this;
                listItemInstance.labelText = material.name;
                var materialInfo = new MaterialItem
                {
                    materialName = material.name,
                    objectWithMaterial = _materialSelector.selectedMaterialObject,
                    material = material,
                    materialItemInstance = materialPanelItem
                };
                AddItemToList<MaterialItem>(materialInfo);
            }
        }
        public void AddItemToList<T>(T item)
        {
            if (typeof(T) != typeof(MaterialItem))
            {
                throw new ArgumentException("Invalid argument of type " + typeof(T));
            }
                _selectableMaterials.Add(item as MaterialItem);
        }

        public void RemoveItemFromList(string  materialName)
        {
            var materialInfo = _selectableMaterials.FirstOrDefault(material => material.materialName == materialName);
            _selectableMaterials.Remove(materialInfo);
            Destroy(materialInfo.materialItemInstance);
        }
        public void ClearList()
        {
            foreach(var item in _selectableMaterials)
            {
                Destroy(item.materialItemInstance);
            }
            _selectableMaterials.Clear();
        }

        void DisplayPointSelector()
        {
            _pointSelectorPanel = _objectPool.GetObjectFromPool(pointSelectPanelPrefab.name, transform);
            _pointSelector = _pointSelectorPanel.GetComponent<SelectPointPanel>();
            _pointSelectorOkButton = ObjectFinder.FindOkButton(_pointSelectorPanel);
            _pointSelectorOkButton.onClick.AddListener(OnSelectPoint);
        }

        void OnSelectPoint()
        {
            if (_trigger == null)
            {
                var triggerObject = _objectPool.GetObjectFromPool(triggerObjectPrefab.name, _extensionsTriggersVessel.transform);
                _trigger = new Trigger(triggerObject, _pointSelector.selectedPoint);
                triggerTextField.text = _trigger.triggerPosition.ToString();
            }
            else
            {
                _trigger.triggerPosition = _pointSelector.selectedPoint;
            }
            _pointSelectorOkButton.onClick?.RemoveAllListeners();
            _objectPool.PoolObject(_pointSelectorPanel);
        }

        void CreateExtension()
        {
            string extensionName = nameInputField.text;
            if (extensionName == "")
            {
                Debug.LogWarning("Please write the extension name");
                return;
            }
            if (_objectToReplaceMaterial == null)
            {
                Debug.LogWarning("Please select the material to change");
                return;
            }
            if (_trigger == null)
            {
                Debug.LogWarning("Please set up the trigger");
                return;
            }
            MaterialSelectorExtension extension = new MaterialSelectorExtension
            {
                objectToReplaceMaterial = _objectToReplaceMaterial,
                selectableMaterials = getMaterialsList(),
                trigger = _trigger,
                extensionName = extensionName
            };
            extension.trigger.triggerObject.GetComponent<TriggerInstance>().extension = extension;
            _extensionManager.AddItemToList<Extension>(extension);
            ClearPanel();
            _objectPool.PoolObject(this.gameObject);
        }
        void ClearPanel()
        {
            nameInputField.text = "";
            ClearMaterialToReplace();
            ClearList();
            _trigger = null;
            triggerTextField.text = "";
        }
        Material[] getMaterialsList()
        {
            var materialList = new Material[_selectableMaterials.Count];
            for(int i = 0; i < materialList.Length; i++)
            {
                materialList[i] = _selectableMaterials[i].material;
            }
            return materialList;
        }
    }
}
