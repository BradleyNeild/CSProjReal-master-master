using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game3
{
    public class ParticleHandler
    {
        List<Particle> particles = new List<Particle>();
        public void CreateParticles(int amount, int force, Point position, Color color, float gravity, int yFloor)
        {
            for (int i = 0; i < amount; i++)
            {
                particles.Add(new Particle(Game1.whitePixelTexture, color, new Rectangle(position, Point.Zero), new Vector2(Game1.random.Next(-4,4),Game1.random.Next(-10,0)), new Timer((float)Game1.random.NextDouble() * 5),gravity, yFloor));
            }
            Game1.objectHandler.AddObjects(particles);
            particles.Clear();
        }
        public void CreateRainbowParticles(int amount, int force, Point position, float gravity)
        {
            for (int i = 0; i < amount; i++)
            {
                particles.Add(new Particle(Game1.whitePixelTexture, new Color(Game1.random.Next(100, 255), Game1.random.Next(100, 255), Game1.random.Next(100, 255)), new Rectangle(position, Point.Zero), new Vector2(Game1.random.Next(-4, 4), Game1.random.Next(-10, 0)), new Timer((float)Game1.random.NextDouble() * 5), gravity, 30));
            }
            Game1.objectHandler.AddObjects(particles);
            particles.Clear();
        }
    }
}
