using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class FOV : MonoBehaviour
{
    public float heroVigRange;
    [Space]
    public float monsterVigRange;
    public float monsterVigVelocity;
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
        if(localPlayerTag == "Player" && m_Profile.vignette.settings.smoothness == 0.01f)
        {
            Debug.Log("Vignette changed Hero");
            var vignette = m_Profile.vignette.settings;
            vignette.smoothness = heroVigRange;
            m_Profile.vignette.settings = vignette;
        }
        else if(localPlayerTag == "Monster" && m_Profile.vignette.settings.smoothness == 0.01f)
        {
            Debug.Log("Vignette changed Monster");
            var vignette = m_Profile.vignette.settings;
            vignette.smoothness = monsterVigRange;
            m_Profile.vignette.settings = vignette;
        }
        if(localPlayerTag == "Monster" &&  m_Profile.vignette.settings.smoothness > 0.22f)
        {
            Debug.Log("Monster changing Vig");
            var monvignette = m_Profile.vignette.settings;
            monvignette.smoothness = monvignette.smoothness - monsterVigVelocity;
            m_Profile.vignette.settings = monvignette;
        }
    }
}
