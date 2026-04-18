using UnityEngine;

public class SkyBoxControl : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 0.6f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
