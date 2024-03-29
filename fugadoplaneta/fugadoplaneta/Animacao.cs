﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace fugadoplaneta
{
    class Animacao : SpriteManager
    {
        private float timeElapsed;
        public bool IsLooping = false;

		//20 frames per second
        private float timeToUpdate = 0.05f;
        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public Animacao(Texture2D Texture, int frames, int animations) : base(Texture, frames, animations)
        {
			
        }
        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)
                gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;
				if (FrameIndex <Frames - 1)
				{
                    FrameIndex++; 
                }
				else if (IsLooping)
	            {
                    FrameIndex = 0;
                }
            }
        }
    }
}
