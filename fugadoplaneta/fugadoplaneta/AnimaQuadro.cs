
using Microsoft.Xna.Framework.Graphics;
namespace fugadoplaneta
{ 
    class AnimaQuadro : SpriteManager
    {
        public AnimaQuadro(Texture2D Texture, int frames, int animations)
            : base(Texture, frames, animations)
        {
        }

        public void SetFrame(int frame)
        {
            if (frame < Frames)
                FrameIndex = frame;
        }
    }
}
