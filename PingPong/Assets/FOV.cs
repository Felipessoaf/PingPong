using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class FOV : MonoBehaviour
{
    public string localPlayerTag;
    PostProcessingProfile m_Profile;

    void OnEnable()
    {
        var behaviour = GetComponent<PostProcessingBehaviour>();

        if (behaviour.profile == null)
        {
            enabled = false;
            return;
        }

        m_Profile = Instantiate(behaviour.profile);
        behaviour.profile = m_Profile;
        

    }

    void Update()
    {
        /*
        var vignette = m_Profile.vignette.settings;
        vignette.smoothness = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup) * 0.45f) + 0.001f + 0.2f;
        m_Profile.vignette.settings = vignette;
        */
        if(localPlayerTag == "Player" && m_Profile.vignette.settings.smoothness == 1f)
        {
            Debug.Log("Vignette changed");
            var vignette = m_Profile.vignette.settings;
            vignette.smoothness = 0.6f;
            m_Profile.vignette.settings = vignette;
        }
    }
}
