using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ObjectInteractive : MonoBehaviour
{/// <summary>
 /// The material to use when this object is inactive (not being gazed at).
 /// </summary>
    public Material inactiveMaterial;

    /// <summary>The material to use when this object is active (gazed at).</summary>
    public Material gazedAtMaterial;

    private Vector3 startingPosition;
    private Renderer myRenderer;

    /// <summary>Sets this instance's GazedAt state.</summary>
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    public void SetGazedAt(bool gazedAt)
    {
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            myRenderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
            return;
        }
    }
    IEnumerator WaitForLandscape()
    {
        while (Screen.orientation != ScreenOrientation.LandscapeLeft)
        {
            yield return null;
        }
        XRSettings.LoadDeviceByName("cardboard");
    }
    /// <summary>Resets this instance and its siblings to their starting positions.</summary>
    public void Reset()
    {
        int sibIdx = transform.GetSiblingIndex();
        int numSibs = transform.parent.childCount;
        for (int i = 0; i < numSibs; i++)
        {
            GameObject sib = transform.parent.GetChild(i).gameObject;
            sib.transform.localPosition = startingPosition;
            sib.SetActive(i == sibIdx);
        }
    }

    /// <summary>Calls the Recenter event.</summary>
//    public void Recenter()
//    {
//#if !UNITY_EDITOR
//            GvrCardboardHelpers.Recenter();
//#else
//        if (GvrEditorEmulator.Instance != null)
//        {
//            GvrEditorEmulator.Instance.Recenter();
//        }
//#endif  // !UNITY_EDITOR
//    }

    /// <summary>Teleport this instance randomly when triggered by a pointer click.</summary>
    /// <param name="eventData">The pointer click event which triggered this call.</param>
    public void TeleportRandomly(BaseEventData eventData)
    {
        // Only trigger on left input button, which maps to
        // Daydream controller TouchPadButton and Trigger buttons.
        PointerEventData ped = eventData as PointerEventData;
        if (ped != null)
        {
            if (ped.button != PointerEventData.InputButton.Left)
            {
                return;
            }
        }
        bool oldVal = XRSettings.enabled;
        XRSettings.enabled = !oldVal;
        if (!oldVal)
        {
            StartCoroutine(WaitForLandscape());
        }
        // Pick a random sibling, move them somewhere random, activate them,
        //// deactivate ourself.
        //int sibIdx = transform.GetSiblingIndex();
        //int numSibs = transform.parent.childCount;
        //sibIdx = (sibIdx + Random.Range(1, numSibs)) % numSibs;
        //GameObject randomSib = transform.parent.GetChild(sibIdx).gameObject;

        //// Move to random new location ±90˚ horzontal.
        //Vector3 direction = Quaternion.Euler(
        //    0,
        //    Random.Range(-90, 90),
        //    0) * Vector3.forward;

        //// New location between 1.5m and 3.5m.
        //float distance = (2 * Random.value) + 1.5f;
        //Vector3 newPos = direction * distance;

        //// Limit vertical position to be fully in the room.
        //newPos.y = Mathf.Clamp(newPos.y, -1.2f, 4f);
        //randomSib.transform.localPosition = newPos;

        //randomSib.SetActive(true);
        //gameObject.SetActive(false);
        //SetGazedAt(false);
    }
    IEnumerator SwitchToVr()
    {
        XRSettings.LoadDeviceByName("cardboard");
        yield return null;
        XRSettings.enabled = true;
    }
    IEnumerator SwitchToNormal()
    {
        XRSettings.LoadDeviceByName("None");
        yield return null;
        XRSettings.enabled = false;
    }
    
    public void SetVR()
    {
        StartCoroutine(SwitchToVr());
    }
    public void SetNormal(int NumberScene)
    {
        //StartCoroutine(SwitchToNormal());
        SceneManager.LoadScene(NumberScene);
    }
    private void Start()
    {
        startingPosition = transform.localPosition;
        myRenderer = GetComponent<Renderer>();
        SetGazedAt(false);
        SetVR();
    }
}
