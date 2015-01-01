using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    public class CRenderable
    {

        private Effect _shader;
        private VertexBuffer _vertexBuffer;
        public float aspectRatio = (float)CGraphics.GPU.Viewport.Width / (float)CGraphics.GPU.Viewport.Height;
        public VertexPositionColor[] vertices;
        public bool isOffscreen = false;
        public CRenderable(Effect shader = null, params VertexPositionColor[] vertices)
        {
            if (vertices.Count() > 0)
            {
                _shader = shader;
                this.vertices = vertices;
                _vertexBuffer = new VertexBuffer(CGraphics.GPU, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            }
        }

        public virtual bool draw(int x, int y, bool useOverlay = false)
        {
            if (isOffscreen != false)
                renderOffScreen();
            if (_shader != null)
                foreach (EffectPass pass in _shader.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    CGraphics.GPU.SetVertexBuffer(_vertexBuffer);
                    CGraphics.GPU.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, vertices.Length, 0, 1);
                }

            return true;
        }

        public void swapTechnique(string name)
        {
            _shader.CurrentTechnique = _shader.Techniques[name];
        }

        public void swapTechnique(int index)
        {
            _shader.CurrentTechnique = _shader.Techniques[index];
        }

        public void renderOffScreen()
        {
            if (CGraphics._rtar2D != null)
                CGraphics.GPU.SetRenderTarget(CGraphics._rtar2D);
            else
                throw new NullReferenceException("Off-sreen render target is null!");

            CGraphics.GPU.Clear(Color.Transparent);
        }
        
    }
}
