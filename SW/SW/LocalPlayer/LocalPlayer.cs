using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace SW
{
    class LocalPlayer
    {
        public bool IsRunning;

        private FlameParticleEngine particleEngine;

        private SpriteFont debug;
        private Texture2D drawTexture;
        private SoundEffect effect;
        private SoundEffectInstance rocketSound;
        private Vector2 drawLocation;
        private float drawAngle;
        private KeyboardState ks;
        private KeyboardState oks;
        private MouseState ms;
        private MouseState oms;
        private GamePadState gps;
        private GamePadState ogps;
        private Vector2 velocity;
        private float forceAngle;
        private float forceMag;
        private float standardForceMag;
        private Vector2 velocityCap;
        private float movementAngle;
        private float fuel;
        private float fuelRate;

        public void Initialize()
        {

            drawLocation = new Vector2(200, 200);
            drawAngle = (float)Math.PI / -2.0f;
            velocity = new Vector2(10, 10);
            forceAngle = 0.0f;
            forceMag = 0.0f;
            standardForceMag = 100.0f;
            velocityCap = new Vector2(200, 200);
            fuel = 1.0f;
            fuelRate = 0.01f;
            IsRunning = false;
        }

        public void LoadContent(ContentManager content)
        {

            debug = content.Load<SpriteFont>("LocalPlayer\\debug");
            drawTexture = content.Load<Texture2D>("LocalPlayer\\spaceman");
            List<Texture2D> pel = new List<Texture2D>();
            pel.Add(content.Load<Texture2D>("FlameEffect\\whiteParticle"));
            particleEngine = new FlameParticleEngine(pel, new Vector2(400, 240));
            effect = content.Load<SoundEffect>("FlameEffect\\rocket");
            rocketSound = effect.CreateInstance();
            rocketSound.IsLooped = true;
            rocketSound.Pitch = 0.0f;
            rocketSound.Pan = 0.0f;
            rocketSound.Volume = 0.0f;
        }

        public void Update(float elapsed)
        {
            preUpdateInput();
            updateMovement(elapsed);
            particleEngine.Update(elapsed, forceAngle);
            postUpdateInput();
        }

        private void updateMovement(float elapsed)
        {
            if (IsRunning)
            {
                forceMag = 0.0f;
                forceAngle = 0.0f;
                particleEngine.emit = false;
                particleEngine.EmitterLocation = drawLocation;

                if (fuel > 0.0f)
                {
                    calculateForceAngleKeyboard();
                    calculateForceAngleMouse();
                    calculateForceAngleGamePad();
                }
                capVelocity();
                velocity = velocity + new Vector2(forceMag * (float)Math.Cos(forceAngle), forceMag * (float)Math.Sin(forceAngle)) * elapsed;
                drawLocation = drawLocation + velocity * elapsed;
                movementAngle = (float)Math.Atan2(velocity.Y, velocity.X);
                drawAngle = drawAngle + (movementAngle - drawAngle) * elapsed;
                handleSound(elapsed);
                handleFuel(elapsed);
            }
        }

        public void handleFuel(float elapsed)
        {
            if (particleEngine.emit)
            {
                fuel = fuel - fuelRate * elapsed;
                if (fuel < 0.0f)
                    fuel = 0.0f;
            }
        }

        private void handleSound(float elapsed)
        {
            float volume = 0.0f;
            if (particleEngine.emit )
            {
                rocketSound.Play();
                volume = rocketSound.Volume + 5.0f * elapsed;
                if (volume > 1.0f)
                    volume = 1.0f;
                rocketSound.Volume = volume;
            }
            else
            {
                volume = rocketSound.Volume - 10.0f * elapsed;
                if (volume < 0.0f)
                    volume = 0.0f;
                rocketSound.Volume = volume;
            }
        }

        private void calculateForceAngleGamePad()
        {
            if (Math.Abs(gps.ThumbSticks.Left.Y) > 0.5f || Math.Abs(gps.ThumbSticks.Left.X) > 0.5f)
            {
                forceAngle = (float)Math.Atan2(-gps.ThumbSticks.Left.Y, gps.ThumbSticks.Left.X);
                forceMag = standardForceMag;
                particleEngine.emit = true;
            }
        }

        private void calculateForceAngleMouse()
        {
            if (ms.RightButton == ButtonState.Pressed || ms.LeftButton == ButtonState.Pressed)
            {
                forceMag = standardForceMag;
                forceAngle = (float)Math.Atan2((ms.Y - drawLocation.Y * ScreenManager.S), (ms.X - drawLocation.X * ScreenManager.S));
                particleEngine.emit = true;
            }
        }

        private void calculateForceAngleKeyboard()
        {
            Vector2 keypad = Vector2.Zero;
            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.Left) ||
                ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.Up))
            {
                forceMag = standardForceMag;
                particleEngine.emit = true;
            }
            if (ks.IsKeyDown(Keys.Right) && ks.IsKeyUp(Keys.Left))
            {
                forceMag = standardForceMag;
                keypad.X = keypad.X + 1.0f;
            }
            if (ks.IsKeyDown(Keys.Left) && ks.IsKeyUp(Keys.Right))
            {
                forceMag = standardForceMag;
                keypad.X = keypad.X - 1.0f;
            }
            if (ks.IsKeyDown(Keys.Down) && ks.IsKeyUp(Keys.Up))
            {
                forceMag = standardForceMag;
                keypad.Y = keypad.Y + 1.0f;
            }
            if (ks.IsKeyDown(Keys.Up) && ks.IsKeyUp(Keys.Down))
            {
                forceMag = standardForceMag;
                keypad.Y = keypad.Y - 1.0f;
            }
            forceAngle = (float)Math.Atan2(keypad.Y, keypad.X);
        }


        private void capVelocity()
        {
            if (velocity.X > velocityCap.X)
                velocity.X = velocityCap.X;
            if (velocity.Y > velocityCap.Y)
                velocity.Y = velocityCap.Y;
            if (velocity.X < -velocityCap.X)
                velocity.X = -velocityCap.X;
            if (velocity.Y < -velocityCap.Y)
                velocity.Y = -velocityCap.Y;
        }

        private void preUpdateInput()
        {
            ms = Mouse.GetState();
            ks = Keyboard.GetState();
            gps = GamePad.GetState(0);
        }

        private void postUpdateInput()
        {
            oms = ms;
            oks = ks;
            ogps = gps;
        }

        public void Draw(SpriteBatch batch)
        {
            if (IsRunning)
            {
                batch.DrawString(debug, "Fuel: " + fuel.ToString(), Vector2.Zero, Color.White, 0.0f, Vector2.Zero, ScreenManager.S, SpriteEffects.None, 1.0f);
                batch.Draw(drawTexture, drawLocation * ScreenManager.S, null,
                           Color.White, drawAngle, new Vector2(drawTexture.Width / 2, drawTexture.Height / 2),
                           1.0f * ScreenManager.S, SpriteEffects.None, 1.0f);
                particleEngine.Draw(batch);
            }
        }

        public Vector2 ReadLocation()
        {
            return drawLocation;
        }

        public float ReadFuel()
        {
            return fuel;
        }


    }
}
