﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

//taken from starter code from IGME 106 taught by Erica Mesh
namespace EndGame
{
    public enum Effect
    {
        damageOverTime,
        increaseSpeed,
        increaseHealth,
        increaseDamage,
        increaseShotSize,
        increaseFireRate,
        reset
    }
    public delegate void OnButtonClickDelegate();
    class Button
    {
       
        
       
            // Button specific fields
            private SpriteFont font;
            private MouseState prevMState;
            private string text;
            private Rectangle position; // Button position and size
            private Vector2 textLoc;
            private Texture2D buttonImg;
            private Color textColor;
            private Effect effect;
            private Player player;


        public Effect Effect
        {
            get { return effect; }
        }
            /// <summary>
            /// If the client wants to be notified when a button is clicked, it must
            /// implement a method matching OnButtonClickDelegate and then tie that method to
            /// the button's "OnButtonClick" event.
            /// 
            /// The delegate will be called with a reference to the clicked button.
            /// </summary>
            public event OnButtonClickDelegate OnLeftButtonClick;
            public event OnButtonClickDelegate OnRightButtonClick;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // Add your new event here!
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            /// <summary>
            /// Create a new custom button
            /// </summary>
            /// <param name="device">The graphics device for this game - needed to create custom button textures.</param>
            /// <param name="position">Where to draw the button's top left corner</param>
            /// <param name="text">The text to draw on the button</param>
            /// <param name="font">The font to use when drawing the button text.</param>
            /// <param name="color">The color to make the button's texture.</param>
            public Button(GraphicsDevice device, Rectangle position, String text, SpriteFont font, Color color, Effect effect)
            {
                // Save copies/references to the info we'll need later
                this.font = font;
                this.position = position;
                this.text = text;
            this.effect = effect;

                // Figure out where on the button to draw it
                Vector2 textSize = font.MeasureString(text);
                textLoc = new Vector2(
                    (position.X + position.Width / 2) - textSize.X / 2,
                    (position.Y + position.Height / 2) - textSize.Y / 2
                );

                // Invert the button color for the text color (because why not)
                textColor = new Color(255 - color.R, 255 - color.G, 255 - color.B);

                // Make a custom 2d texture for the button itself
                buttonImg = new Texture2D(device, position.Width, position.Height, false, SurfaceFormat.Color);
                int[] colorData = new int[buttonImg.Width * buttonImg.Height]; // an array to hold all the pixels of the texture
                Array.Fill<int>(colorData, (int)color.PackedValue); // fill the array with all the same color
                buttonImg.SetData<Int32>(colorData, 0, colorData.Length); // update the texture's data
            }

            /// <summary>
            /// Each frame, update its status if it's been clicked.
            /// </summary>
            public void Update()
            {
                // Check/capture the mouse state regardless of whether this button
                // if active so that it's up to date next time!
                MouseState mState = Mouse.GetState();
                if (mState.LeftButton == ButtonState.Released &&
                    prevMState.LeftButton == ButtonState.Pressed &&
                    position.Contains(mState.Position))
                {

                
                if (OnLeftButtonClick != null)
                    {
                        // Call ALL methods attached to this button
                        OnLeftButtonClick();
                    }
                }




                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // Add your new click detection here!
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //if the right click button was pressed and released and the cursor is within the buttons rectangle
                if (mState.RightButton == ButtonState.Released &&
                    prevMState.RightButton == ButtonState.Pressed &&
                    position.Contains(mState.Position))
                {
                    //and a functionality has been added to OnRightButtonClick
                    if (OnRightButtonClick != null)
                    {
                        //runs whatever OnRightButtonClick does
                        OnRightButtonClick();
                    }
                }

                prevMState = mState;
            }

            /// <summary>
            /// Override the GameObject Draw() to draw the button and then
            /// overlay it with text.
            /// </summary>
            /// <param name="spriteBatch">The spriteBatch on which to draw this button. The button 
            /// assumes that Begin() has already been called and End() will be called later.</param>
            public void Draw(SpriteBatch spriteBatch)
            {
                // Draw the button itself
                spriteBatch.Draw(buttonImg, position, Color.Black);

                // Draw button text over the button
                spriteBatch.DrawString(font, text, textLoc, Color.White);
            }
    }
}
