using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartLabelManager : MonoBehaviour
{
    [System.Serializable]
    public class PartData
    {
        public string partName;
        public Transform partTransform;
        public string Description;
    }

    public List<PartData> parts = new List<PartData>();

    [Header("UI Elements")]
    public GameObject buttonPrefab;     // Prefab for UI button
    public Transform uiPanel;

    public GameObject DescriptionTextPanel;
    public TMP_Text Descriptiontext;
    [Header("Highlight Settings")]
    public Material highlightMaterial;
    private Dictionary<Transform, Material[]> originalMaterials =
        new Dictionary<Transform, Material[]>();

    public Color HighlightedColor;
    public Color NormalColor;

    // ----------------------------------------------------------------------    
    private void Start()
    {
        //CreatePartButtons();
    }

    // ----------------------------------------------------------------------
    // Create buttons only - NO labels in 3D
    // ----------------------------------------------------------------------
    public void CreatePartButtons()
    {
        // Clear old buttons if any
        foreach (Transform child in uiPanel)
            Destroy(child.gameObject);

        // Create buttons
        foreach (var p in parts)
        {
            GameObject btnObj = Instantiate(buttonPrefab, uiPanel);
            btnObj.name = p.partName + "_BTN";

            Button btn = btnObj.GetComponentInChildren<Button>();
            TMP_Text txt = btnObj.GetComponentInChildren<TMP_Text>();

            if (txt != null)
                txt.text = p.partName;

            btn.onClick.AddListener(() =>
            {
                HighlightPart(p.partTransform,btn,p);
            });
        }
    }

    // ----------------------------------------------------------------------
    // Highlight the selected part for 3 seconds
    // ----------------------------------------------------------------------
    void HighlightPart(Transform part, Button btn,PartData p)
    {
        if (!originalMaterials.ContainsKey(part))
        {
            MeshRenderer[] mrs = part.GetComponentsInChildren<MeshRenderer>();
            Material[] mats = new Material[mrs.Length];

            // Store original materials
            for (int i = 0; i < mrs.Length; i++)
            {
                mats[i] = mrs[i].material;
                mrs[i].material = highlightMaterial;
            }
            DescriptionTextPanel.SetActive(true);
            Descriptiontext.text = p.Description;
            originalMaterials.Add(part, mats);
            btn.GetComponent<Image>().color = HighlightedColor;
            StartCoroutine(RemoveHighlightAfterDelay(part, mrs, btn));
        }
    }

    IEnumerator RemoveHighlightAfterDelay(Transform part, MeshRenderer[] mrs, Button btn)
    {
        yield return new WaitForSeconds(1.5f);

        if (originalMaterials.ContainsKey(part))
        {
            Material[] mats = originalMaterials[part];

            for (int i = 0; i < mrs.Length; i++)
            {
                if (mrs[i] != null)
                    mrs[i].material = mats[i];
            }

            DescriptionTextPanel.SetActive(false);
            btn.GetComponent<Image>().color = NormalColor;
            originalMaterials.Remove(part);
        }
    }
}
