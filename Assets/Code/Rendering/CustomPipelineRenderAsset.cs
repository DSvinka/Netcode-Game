using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Rendering
{
    [CreateAssetMenu(menuName = "Rendering/CustomPipelineRenderAsset")]
    public class CustomPipelineRenderAsset: RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new CustomPipelineRender();
        }
    }
}