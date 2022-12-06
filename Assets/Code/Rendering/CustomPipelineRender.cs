using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Rendering
{
    public class CustomPipelineRender : RenderPipeline
    {
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            CamerasRender(context, cameras);
        }
        
        private void CamerasRender(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var camera in cameras)
            {
                var cameraRenderer = camera.GetComponent<CameraRenderer>();
                cameraRenderer.Render(context, camera);
            }
        }

    }

}
