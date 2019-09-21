/****************************************
Author: JunCi Yu
Company: fengyuzhu
Email:kirby_yu@hotmail.com
Data: 2019/9/20 21:11:29
*****************************************/

using UnityEngine;

public class BlackHoleEffect : ImageEffectBase 
{
    public bool life = true;

    private Vector4 viewport = Vector4.zero;

    [SerializeField]
    [Range(0, 1)]
    private float scale = 1;        //黑洞缩放尺寸

    [SerializeField]
    [Range(0, 1)]
    private float width = 0.5f;     //黑洞横向宽度

    [SerializeField]
    [Range(0, 1)]
    private float height = 0.85f;   //黑洞纵向宽度

    private float elapsedTime = 0;
    private float threadholdTime = 0.01f;
    private float iterationTime = 0.05f;

    protected override void Start()
    {
        base.Start();

        material.SetFloat("_Width", width);
        material.SetFloat("_Height", height);
    }

    private void Update()
    {
        if (life)
        {
            if (scale >= 1) return;

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= threadholdTime)
            {
                scale += iterationTime;
                scale = Mathf.Min(1, scale);
                elapsedTime = 0;
            }
        }
        else
        {
            if (scale <= 0) return;

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= threadholdTime)
            {
                scale -= iterationTime;
                scale = Mathf.Max(0, scale);
                elapsedTime = 0;
            }
        }
    }

    public void SetBlackHoleBirthPoint(Vector2 viewport)
    {
        this.viewport = new Vector4(viewport.x, viewport.y, 0, 0);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            material.SetVector("_Viewport", viewport);
            material.SetFloat("_Scale", scale);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}