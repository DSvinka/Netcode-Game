using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Code.Rendering
{
    public partial class CameraRenderer
    {
        private CommandBuffer _commandBuffer = new CommandBuffer();
        private string _bufferName;
        
        private CullingResults _cullingResult;

        private ScriptableRenderContext _context;
        private Camera _camera;
        
        private static readonly List<ShaderTagId> DrawingShaderTagIds =
            new List<ShaderTagId>
            {
                new ShaderTagId("SRPDefaultUnlit"),
            };

        
        public void Render(ScriptableRenderContext context, Camera camera)
        {
            _camera = camera;
            _context = context;

            _bufferName = camera.gameObject.name;
            _commandBuffer.name = _bufferName;
            if (Cull(out var parameters))
                return;
            
            Settings(parameters);
            DrawUnsupportedShaders();
            DrawGizmos();
            DrawVisible();
            Submit();
        }
        
        private DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTags, SortingCriteria
            sortingCriteria, out SortingSettings sortingSettings)
        {
            sortingSettings = new SortingSettings(_camera)
            {
                criteria = sortingCriteria,
            };
            var drawingSettings = new DrawingSettings(shaderTags[0], sortingSettings);
            for (var i = 1; i < shaderTags.Count; i++)
            {
                drawingSettings.SetShaderPassName(i, shaderTags[i]);
            }
            return drawingSettings;
        }

        
        private void Settings(ScriptableCullingParameters parameters)
        {
            _cullingResult = _context.Cull(ref parameters);
            
            _context.SetupCameraProperties(_camera);
            _commandBuffer.ClearRenderTarget(true, true, Color.clear);
            _commandBuffer.BeginSample(_bufferName);
            ExecuteCommandBuffer();
        }

        private void Submit()
        {
            _commandBuffer.EndSample(_bufferName);
            ExecuteCommandBuffer();
            _context.Submit();
        }
        private void ExecuteCommandBuffer()
        {
            _context.ExecuteCommandBuffer(_commandBuffer);
            _commandBuffer.Clear();
        }

        private void DrawVisible()
        {
            var drawingSettings = CreateDrawingSettings(DrawingShaderTagIds, SortingCriteria.CommonOpaque, out var sortingSettings);
            var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
            _context.DrawRenderers(_cullingResult, ref drawingSettings, ref filteringSettings);
            _context.DrawSkybox(_camera);
            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings;
            filteringSettings.renderQueueRange = RenderQueueRange.transparent;
            _context.DrawRenderers(_cullingResult, ref drawingSettings, ref filteringSettings);

        }

        private bool Cull(out ScriptableCullingParameters parameters)
        {
            return _camera.TryGetCullingParameters(out parameters);
        }
    }
}